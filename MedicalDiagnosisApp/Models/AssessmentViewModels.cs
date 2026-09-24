using System.ComponentModel.DataAnnotations;

namespace MedicalDiagnosisApp.Models
{
    public class AssessmentViewModel
    {
        public int Stage { get; set; } = 1;

        public List<int> SelectedSymptomIds { get; set; } = new();

        public List<SymptomOptionViewModel> Symptoms { get; set; }
            = new();

        public string Duration { get; set; } = "";

        public string Severity { get; set; } = "";

        public string Frequency { get; set; } = "";

        public string Onset { get; set; } = "";
    }

    public class SymptomOptionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Category { get; set; } = "";

        public bool IsSelected { get; set; }
    }

    public class AssessmentResultViewModel
    {
        public string PossibleCondition { get; set; } = "";

        public string Description { get; set; } = "";

        public string Recommendation { get; set; } = "";

        public double MatchPercentage { get; set; }

        public bool IsEmergency { get; set; }

        public List<string> SelectedSymptoms { get; set; } = new();

        public List<DiseaseScoreViewModel> Scores { get; set; } = new();

        public string Duration { get; set; } = "";

        public string Severity { get; set; } = "";

        public string Frequency { get; set; } = "";

        public string Onset { get; set; } = "";
    }

    public class DiseaseScoreViewModel
    {
        public string DiseaseName { get; set; } = "";

        public double Score { get; set; }
    }
}