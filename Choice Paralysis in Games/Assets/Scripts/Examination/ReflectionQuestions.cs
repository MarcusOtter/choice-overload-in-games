namespace Scripts.Examination
{
    [System.Serializable]
    public struct ReflectionQuestions
    {
        public string EntertainmentValue;
        public string PleasedWithPerformance;
        public string FinalCharacterSatisfaction;
        
        public ReflectionQuestions(string entertainmentValue, string pleasedWithPerformance, string finalCharacterSatisfaction)
        {
            EntertainmentValue = entertainmentValue;
            PleasedWithPerformance = pleasedWithPerformance;
            FinalCharacterSatisfaction = finalCharacterSatisfaction;
        }
    }
}
