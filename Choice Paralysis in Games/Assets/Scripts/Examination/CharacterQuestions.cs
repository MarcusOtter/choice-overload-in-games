namespace Scripts.Examination
{
    [System.Serializable]
    public struct CharacterQuestions
    {
        public string InitialCharacterSatisfaction;
        public string OptionAmount;
        public string EnjoyedCustomization;

        public CharacterQuestions(string initialCharacterSatisfaction, string optionAmount, string enjoyedCustomization)
        {
            InitialCharacterSatisfaction = initialCharacterSatisfaction;
            OptionAmount = optionAmount;
            EnjoyedCustomization = enjoyedCustomization;
        }
    }
}
