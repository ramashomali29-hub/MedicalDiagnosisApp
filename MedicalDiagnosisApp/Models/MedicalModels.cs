using System.ComponentModel.DataAnnotations;

namespace MedicalDiagnosisApp.Models
{
    public class Disease
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public string Recommendation { get; set; } = "";

        public List<DiseaseSymptom> DiseaseSymptoms { get; set; }
            = new();
    }

    public class Symptom
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        // القسم الذي سيظهر تحته العرض
        // مثال: Head & Neurological
        [Required]
        public string Category { get; set; } = "";

        // هل يظهر في الصفحة الأولى؟
        public bool IsGeneral { get; set; }

        // هل يحتاج إلى تنبيه طبي عاجل؟
        public bool IsEmergency { get; set; }

        public List<DiseaseSymptom> DiseaseSymptoms { get; set; }
            = new();
    }

    public class DiseaseSymptom
    {
        public int DiseaseId { get; set; }

        public Disease Disease { get; set; } = null!;

        public int SymptomId { get; set; }

        public Symptom Symptom { get; set; } = null!;

        // أهمية العرض للمرض من 1 إلى 5
        public int Weight { get; set; }
    }
}