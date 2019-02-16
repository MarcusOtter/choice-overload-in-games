namespace Scripts.Examination
{
    [System.Serializable]
    public struct SubjectQuestions
    {
        public string Gender;
        public bool KnowsExperimentPurpose;

        public SubjectQuestions(string gender, bool knowsExperimentPurpose)
        {
            Gender = gender;
            KnowsExperimentPurpose = knowsExperimentPurpose;
        }
    }
}
