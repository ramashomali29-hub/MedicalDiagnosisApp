using MedicalDiagnosisApp.Data;
using MedicalDiagnosisApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalDiagnosisApp.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly AppDbContext _context;

        public AssessmentController(AppDbContext context)
        {
            _context = context;
        }

        // المرحلة الأولى: عرض الأعراض العامة
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var symptoms = await _context.Symptoms
                .Where(x => x.IsGeneral)
                .OrderBy(x => x.Category)
                .ThenBy(x => x.Name)
                .ToListAsync();

            var model = new AssessmentViewModel
            {
                Stage = 1,

                Symptoms = symptoms.Select(x =>
                    new SymptomOptionViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Category = x.Category
                    })
                    .ToList()
            };

            return View(model);
        }

        // الانتقال إلى المرحلة الثانية
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(
            List<int>? selectedSymptomIds)
        {
            selectedSymptomIds ??= new List<int>();

            if (selectedSymptomIds.Count == 0)
            {
                TempData["Error"] =
                    "Please select at least one symptom.";

                return RedirectToAction(nameof(Index));
            }

            // التحقق من أعراض الطوارئ
            bool emergency = await _context.Symptoms
                .AnyAsync(x =>
                    selectedSymptomIds.Contains(x.Id) &&
                    x.IsEmergency);

            if (emergency)
            {
                return RedirectToAction(
                    nameof(Result),
                    new
                    {
                        ids = string.Join(
                            ",",
                            selectedSymptomIds)
                    });
            }

            // إيجاد الأمراض التي تتوافق مع الاختيارات
            var possibleDiseaseIds =
                await _context.DiseaseSymptoms
                    .Where(x =>
                        selectedSymptomIds.Contains(
                            x.SymptomId))
                    .Select(x => x.DiseaseId)
                    .Distinct()
                    .ToListAsync();

            // جلب الأسئلة التفصيلية المرتبطة
            // بالأمراض المحتملة فقط
            var detailedSymptoms =
                await _context.DiseaseSymptoms
                    .Where(x =>
                        possibleDiseaseIds.Contains(
                            x.DiseaseId) &&
                        !x.Symptom.IsGeneral)
                    .Select(x => x.Symptom)
                    .Distinct()
                    .OrderBy(x => x.Category)
                    .ThenBy(x => x.Name)
                    .ToListAsync();

            var model = new AssessmentViewModel
            {
                Stage = 2,

                SelectedSymptomIds =
                    selectedSymptomIds,

                Symptoms = detailedSymptoms.Select(x =>
                    new SymptomOptionViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Category = x.Category
                    })
                    .ToList()
            };

            return View("Index", model);
        }

        // استلام اختيارات المرحلة الثانية
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complete(
            List<int>? generalSymptomIds,
            List<int>? detailedSymptomIds)
        {
            generalSymptomIds ??= new List<int>();
            detailedSymptomIds ??= new List<int>();

            var allIds = generalSymptomIds
                .Concat(detailedSymptomIds)
                .Distinct()
                .ToList();

            if (allIds.Count == 0)
            {
                TempData["Error"] =
                    "Please select at least one symptom.";

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(
                nameof(Result),
                new
                {
                    ids = string.Join(",", allIds)
                });
        }

        // حساب النتيجة
        [HttpGet]
        public async Task<IActionResult> Result(string? ids)
        {
            var selectedIds = (ids ?? "")
                .Split(
                    ',',
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(value =>
                    int.TryParse(value, out int id)
                        ? id
                        : 0)
                .Where(id => id > 0)
                .Distinct()
                .ToList();

            if (selectedIds.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var selectedSymptoms =
                await _context.Symptoms
                    .Where(x =>
                        selectedIds.Contains(x.Id))
                    .ToListAsync();

            // عرض تنبيه مباشر عند وجود عرض خطر
            if (selectedSymptoms.Any(x => x.IsEmergency))
            {
                var emergencyModel =
                    new AssessmentResultViewModel
                    {
                        IsEmergency = true,

                        PossibleCondition =
                            "Urgent Medical Attention Recommended",

                        Description =
                            "You selected a symptom that may require urgent medical evaluation.",

                        Recommendation =
                            "Contact emergency services or seek immediate medical assistance.",

                        SelectedSymptoms =
                            selectedSymptoms
                                .Select(x => x.Name)
                                .ToList()
                    };

                return View(emergencyModel);
            }

            var diseases = await _context.Diseases
                .Include(x => x.DiseaseSymptoms)
                .ToListAsync();

            var scores = diseases
                .Select(disease =>
                {
                    int selectedWeight =
                        disease.DiseaseSymptoms
                            .Where(x =>
                                selectedIds.Contains(
                                    x.SymptomId))
                            .Sum(x => x.Weight);

                    int totalWeight =
                        disease.DiseaseSymptoms
                            .Sum(x => x.Weight);

                    double percentage =
                        totalWeight == 0
                            ? 0
                            : selectedWeight * 100.0 /
                              totalWeight;

                    return new
                    {
                        Disease = disease,

                        Percentage =
                            Math.Round(percentage, 1)
                    };
                })
                .OrderByDescending(x => x.Percentage)
                .ToList();

            if (scores.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var highest = scores.First();

            var model = new AssessmentResultViewModel
            {
                PossibleCondition =
                    highest.Disease.Name,

                Description =
                    highest.Disease.Description,

                Recommendation =
                    highest.Disease.Recommendation,

                MatchPercentage =
                    highest.Percentage,

                SelectedSymptoms =
                    selectedSymptoms
                        .Select(x => x.Name)
                        .ToList(),

                Scores = scores.Select(x =>
                    new DiseaseScoreViewModel
                    {
                        DiseaseName =
                            x.Disease.Name,

                        Score =
                            x.Percentage
                    })
                    .ToList()
            };

            return View(model);
        }
    }
}