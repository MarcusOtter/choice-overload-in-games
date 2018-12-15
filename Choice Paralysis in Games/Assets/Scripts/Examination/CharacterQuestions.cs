namespace Scripts.Examination
{
    [System.Serializable]
    public struct CharacterQuestions
    {
        public string Satisfaction;
        public string Options;
        public string EnjoyedCustomization;

        public CharacterQuestions(string satisfaction, string options, string enjoyedCustomization)
        {
            Satisfaction = satisfaction;
            Options = options;
            EnjoyedCustomization = enjoyedCustomization;
        }
    }
}
