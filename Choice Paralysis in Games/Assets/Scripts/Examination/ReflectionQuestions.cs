namespace Scripts.Examination
{
    [System.Serializable]
    public struct ReflectionQuestions
    {
        public string HappyWithPerformance;
        public string FinalCharacterSatisfaction;
        public string EnjoyedGameplay;
        
        public ReflectionQuestions(string happyWithPerformance, string finalCharacterSatisfaction, string enjoyedGameplay)
        {
            HappyWithPerformance = happyWithPerformance;
            FinalCharacterSatisfaction = finalCharacterSatisfaction;
            EnjoyedGameplay = enjoyedGameplay;
        }
    }
}
