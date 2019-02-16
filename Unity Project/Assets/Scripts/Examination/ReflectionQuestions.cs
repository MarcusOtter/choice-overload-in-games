namespace Scripts.Examination
{
    [System.Serializable]
    public struct ReflectionQuestions
    {
        public int EntertainmentValue;
        public string PleasedWithPerformance;
        public string FinalCharacterSatisfaction;
        
        public ReflectionQuestions(int entertainmentValue, string pleasedWithPerformance, string finalCharacterSatisfaction)
        {
            EntertainmentValue = entertainmentValue;
            PleasedWithPerformance = pleasedWithPerformance;
            FinalCharacterSatisfaction = finalCharacterSatisfaction;
        }
    }
}
