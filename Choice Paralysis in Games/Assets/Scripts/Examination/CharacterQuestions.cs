namespace Scripts.Examination
{
    [System.Serializable]
    public struct CharacterQuestions
    {
        public string InitialCharacterSatisfaction;
        public string OptionAmount;
        public bool EnjoyedCustomization;

        public CharacterQuestions(string initialCharacterSatisfaction, string optionAmount, bool enjoyedCustomization)
        {
            InitialCharacterSatisfaction = initialCharacterSatisfaction;
            OptionAmount = optionAmount;
            EnjoyedCustomization = enjoyedCustomization;
        }
    }
}
