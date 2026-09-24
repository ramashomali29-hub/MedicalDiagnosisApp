using MedicalDiagnosisApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalDiagnosisApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            AppDbContext context)
        {
            await context.Database.MigrateAsync();

            var diseaseData = new[]
            {
                new
                {
                    Name = "Iron-Deficiency Anemia",
                    Description =
                        "A possible shortage of healthy red blood cells or iron.",
                    Recommendation =
                        "Consult a doctor. CBC, ferritin and iron tests may be required."
                },
                new
                {
                    Name = "Hypothyroidism",
                    Description =
                        "The thyroid gland may not be producing enough thyroid hormone.",
                    Recommendation =
                        "Consult a doctor. TSH and T4 blood tests may be required."
                },
                new
                {
                    Name = "Migraine",
                    Description =
                        "A headache condition that may cause throbbing pain and sensitivity to light or sound.",
                    Recommendation =
                        "Consult a healthcare professional for severe, new or frequent headaches."
                },
                new
                {
                    Name = "Type 2 Diabetes",
                    Description =
                        "Blood glucose levels may be higher than normal.",
                    Recommendation =
                        "Consult a doctor. Fasting glucose or HbA1c testing may be required."
                },
                new
                {
                    Name = "Asthma",
                    Description =
                        "Inflammation and narrowing of the airways may cause breathing symptoms.",
                    Recommendation =
                        "Consult a doctor. A breathing assessment or spirometry may be required."
                },
                new
                {
                    Name = "GERD / Acid Reflux",
                    Description =
                        "Stomach acid may be moving back into the esophagus.",
                    Recommendation =
                        "Consult a doctor if symptoms are frequent, severe or affect swallowing."
                },
                new
                {
                    Name = "Dehydration",
                    Description =
                        "The body may not have enough fluid for normal function.",
                    Recommendation =
                        "Drink appropriate fluids and seek medical advice if symptoms are severe or persistent."
                }
            };

            foreach (var data in diseaseData)
            {
                var disease = await context.Diseases
                    .FirstOrDefaultAsync(x => x.Name == data.Name);

                if (disease == null)
                {
                    disease = new Disease
                    {
                        Name = data.Name
                    };

                    context.Diseases.Add(disease);
                }

                disease.Description = data.Description;
                disease.Recommendation = data.Recommendation;
            }

            await context.SaveChangesAsync();

            var symptomData = new[]
            {
                // الصفحة الأولى: Head
                new { Name = "Headache", Category = "Head & Neurological", General = true, Emergency = false },
                new { Name = "Dizziness", Category = "Head & Neurological", General = true, Emergency = false },
                new { Name = "Difficulty concentrating", Category = "Head & Neurological", General = true, Emergency = false },
                new { Name = "Blurred vision", Category = "Head & Neurological", General = true, Emergency = false },
                new { Name = "Loss of consciousness", Category = "Head & Neurological", General = true, Emergency = true },
                new { Name = "Sudden weakness on one side", Category = "Head & Neurological", General = true, Emergency = true },

                // الصفحة الأولى: Chest
                new { Name = "Rapid heartbeat", Category = "Chest & Heart", General = true, Emergency = false },
                new { Name = "Chest tightness", Category = "Chest & Heart", General = true, Emergency = false },
                new { Name = "Heartburn", Category = "Chest & Heart", General = true, Emergency = false },
                new { Name = "Severe chest pain", Category = "Chest & Heart", General = true, Emergency = true },

                // الصفحة الأولى: Breathing
                new { Name = "Shortness of breath", Category = "Breathing", General = true, Emergency = false },
                new { Name = "Wheezing", Category = "Breathing", General = true, Emergency = false },
                new { Name = "Persistent cough", Category = "Breathing", General = true, Emergency = false },
                new { Name = "Rapid breathing", Category = "Breathing", General = true, Emergency = false },
                new { Name = "Severe difficulty breathing", Category = "Breathing", General = true, Emergency = true },

                // الصفحة الأولى: Energy
                new { Name = "Fatigue", Category = "Energy & General", General = true, Emergency = false },
                new { Name = "General weakness", Category = "Energy & General", General = true, Emergency = false },
                new { Name = "Excessive sleepiness", Category = "Energy & General", General = true, Emergency = false },
                new { Name = "Tiredness after light activity", Category = "Energy & General", General = true, Emergency = false },

                // الصفحة الأولى: Weight
                new { Name = "Increased thirst", Category = "Weight, Hunger & Thirst", General = true, Emergency = false },
                new { Name = "Increased hunger", Category = "Weight, Hunger & Thirst", General = true, Emergency = false },
                new { Name = "Unexplained weight gain", Category = "Weight, Hunger & Thirst", General = true, Emergency = false },
                new { Name = "Unexplained weight loss", Category = "Weight, Hunger & Thirst", General = true, Emergency = false },
                new { Name = "Frequent urination", Category = "Weight, Hunger & Thirst", General = true, Emergency = false },

                // الصفحة الأولى: Digestive
                new { Name = "Nausea", Category = "Digestive", General = true, Emergency = false },
                new { Name = "Vomiting", Category = "Digestive", General = true, Emergency = false },
                new { Name = "Constipation", Category = "Digestive", General = true, Emergency = false },
                new { Name = "Abdominal discomfort", Category = "Digestive", General = true, Emergency = false },
                new { Name = "Loss of appetite", Category = "Digestive", General = true, Emergency = false },

                // الصفحة الأولى: Skin
                new { Name = "Pale skin", Category = "Skin, Hair & Temperature", General = true, Emergency = false },
                new { Name = "Dry skin", Category = "Skin, Hair & Temperature", General = true, Emergency = false },
                new { Name = "Thinning hair", Category = "Skin, Hair & Temperature", General = true, Emergency = false },
                new { Name = "Cold hands or feet", Category = "Skin, Hair & Temperature", General = true, Emergency = false },
                new { Name = "Sensitivity to cold", Category = "Skin, Hair & Temperature", General = true, Emergency = false },
                new { Name = "Slow-healing wounds", Category = "Skin, Hair & Temperature", General = true, Emergency = false },
                new { Name = "Dry mouth", Category = "Skin, Hair & Temperature", General = true, Emergency = false },

                // المرحلة الثانية: Anemia
                new { Name = "Feeling faint when standing", Category = "Anemia Details", General = false, Emergency = false },
                new { Name = "Breathlessness after light activity", Category = "Anemia Details", General = false, Emergency = false },

                // المرحلة الثانية: Thyroid
                new { Name = "Symptoms developed gradually", Category = "Thyroid Details", General = false, Emergency = false },
                new { Name = "Slower than usual heart rate", Category = "Thyroid Details", General = false, Emergency = false },

                // المرحلة الثانية: Migraine
                new { Name = "Throbbing headache", Category = "Migraine Details", General = false, Emergency = false },
                new { Name = "Pain on one side of the head", Category = "Migraine Details", General = false, Emergency = false },
                new { Name = "Sensitivity to light or sound", Category = "Migraine Details", General = false, Emergency = false },
                new { Name = "Headache worsens with movement", Category = "Migraine Details", General = false, Emergency = false },

                // المرحلة الثانية: Diabetes
                new { Name = "Waking at night to urinate", Category = "Diabetes Details", General = false, Emergency = false },
                new { Name = "Tingling in hands or feet", Category = "Diabetes Details", General = false, Emergency = false },

                // المرحلة الثانية: Asthma
                new { Name = "Breathing symptoms worsen with exercise", Category = "Asthma Details", General = false, Emergency = false },
                new { Name = "Coughing or wheezing at night", Category = "Asthma Details", General = false, Emergency = false },
                new { Name = "Breathing triggered by dust or smoke", Category = "Asthma Details", General = false, Emergency = false },

                // المرحلة الثانية: GERD
                new { Name = "Acid taste in the mouth", Category = "GERD Details", General = false, Emergency = false },
                new { Name = "Symptoms worsen after eating", Category = "GERD Details", General = false, Emergency = false },
                new { Name = "Symptoms worsen when lying down", Category = "GERD Details", General = false, Emergency = false },

                // المرحلة الثانية: Dehydration
                new { Name = "Dark-colored urine", Category = "Dehydration Details", General = false, Emergency = false },
                new { Name = "Urinating less than usual", Category = "Dehydration Details", General = false, Emergency = false },
                new { Name = "Recent vomiting or diarrhea", Category = "Dehydration Details", General = false, Emergency = false }
            };

            foreach (var data in symptomData)
            {
                var symptom = await context.Symptoms
                    .FirstOrDefaultAsync(x => x.Name == data.Name);

                if (symptom == null)
                {
                    symptom = new Symptom
                    {
                        Name = data.Name
                    };

                    context.Symptoms.Add(symptom);
                }

                symptom.Category = data.Category;
                symptom.IsGeneral = data.General;
                symptom.IsEmergency = data.Emergency;
            }

            await context.SaveChangesAsync();

            var links = new (string Disease, string Symptom, int Weight)[]
            {
                // Anemia
                ("Iron-Deficiency Anemia", "Fatigue", 3),
                ("Iron-Deficiency Anemia", "General weakness", 3),
                ("Iron-Deficiency Anemia", "Dizziness", 3),
                ("Iron-Deficiency Anemia", "Shortness of breath", 4),
                ("Iron-Deficiency Anemia", "Rapid heartbeat", 3),
                ("Iron-Deficiency Anemia", "Pale skin", 5),
                ("Iron-Deficiency Anemia", "Cold hands or feet", 2),
                ("Iron-Deficiency Anemia", "Tiredness after light activity", 4),
                ("Iron-Deficiency Anemia", "Feeling faint when standing", 4),
                ("Iron-Deficiency Anemia", "Breathlessness after light activity", 5),

                // Hypothyroidism
                ("Hypothyroidism", "Fatigue", 3),
                ("Hypothyroidism", "Excessive sleepiness", 2),
                ("Hypothyroidism", "Unexplained weight gain", 5),
                ("Hypothyroidism", "Constipation", 4),
                ("Hypothyroidism", "Dry skin", 4),
                ("Hypothyroidism", "Thinning hair", 4),
                ("Hypothyroidism", "Sensitivity to cold", 5),
                ("Hypothyroidism", "Difficulty concentrating", 2),
                ("Hypothyroidism", "Symptoms developed gradually", 3),
                ("Hypothyroidism", "Slower than usual heart rate", 3),

                // Migraine
                ("Migraine", "Headache", 4),
                ("Migraine", "Dizziness", 2),
                ("Migraine", "Blurred vision", 2),
                ("Migraine", "Nausea", 3),
                ("Migraine", "Vomiting", 2),
                ("Migraine", "Throbbing headache", 5),
                ("Migraine", "Pain on one side of the head", 5),
                ("Migraine", "Sensitivity to light or sound", 5),
                ("Migraine", "Headache worsens with movement", 4),

                // Diabetes
                ("Type 2 Diabetes", "Fatigue", 2),
                ("Type 2 Diabetes", "Increased thirst", 5),
                ("Type 2 Diabetes", "Increased hunger", 3),
                ("Type 2 Diabetes", "Frequent urination", 5),
                ("Type 2 Diabetes", "Unexplained weight loss", 4),
                ("Type 2 Diabetes", "Blurred vision", 3),
                ("Type 2 Diabetes", "Slow-healing wounds", 4),
                ("Type 2 Diabetes", "Waking at night to urinate", 4),
                ("Type 2 Diabetes", "Tingling in hands or feet", 3),

                // Asthma
                ("Asthma", "Shortness of breath", 5),
                ("Asthma", "Wheezing", 5),
                ("Asthma", "Persistent cough", 4),
                ("Asthma", "Rapid breathing", 3),
                ("Asthma", "Chest tightness", 4),
                ("Asthma", "Breathing symptoms worsen with exercise", 5),
                ("Asthma", "Coughing or wheezing at night", 5),
                ("Asthma", "Breathing triggered by dust or smoke", 5),

                // GERD
                ("GERD / Acid Reflux", "Heartburn", 5),
                ("GERD / Acid Reflux", "Nausea", 2),
                ("GERD / Acid Reflux", "Abdominal discomfort", 2),
                ("GERD / Acid Reflux", "Persistent cough", 2),
                ("GERD / Acid Reflux", "Acid taste in the mouth", 5),
                ("GERD / Acid Reflux", "Symptoms worsen after eating", 5),
                ("GERD / Acid Reflux", "Symptoms worsen when lying down", 5),

                // Dehydration
                ("Dehydration", "Increased thirst", 4),
                ("Dehydration", "Dry mouth", 5),
                ("Dehydration", "Dizziness", 3),
                ("Dehydration", "Fatigue", 2),
                ("Dehydration", "General weakness", 2),
                ("Dehydration", "Headache", 2),
                ("Dehydration", "Loss of appetite", 1),
                ("Dehydration", "Dark-colored urine", 5),
                ("Dehydration", "Urinating less than usual", 5),
                ("Dehydration", "Recent vomiting or diarrhea", 4)
            };

            var diseases = await context.Diseases.ToListAsync();
            var symptoms = await context.Symptoms.ToListAsync();

            foreach (var link in links)
            {
                var disease = diseases
                    .First(x => x.Name == link.Disease);

                var symptom = symptoms
                    .First(x => x.Name == link.Symptom);

                var existing = await context.DiseaseSymptoms
                    .FirstOrDefaultAsync(x =>
                        x.DiseaseId == disease.Id &&
                        x.SymptomId == symptom.Id);

                if (existing == null)
                {
                    context.DiseaseSymptoms.Add(
                        new DiseaseSymptom
                        {
                            DiseaseId = disease.Id,
                            SymptomId = symptom.Id,
                            Weight = link.Weight
                        });
                }
                else
                {
                    existing.Weight = link.Weight;
                }
            }

            await context.SaveChangesAsync();
        }
    }
}