namespace Scripts.Examination
{
    [System.Serializable]
    public struct ExaminationEntry
    {
        public CharacterData CharacterData;
        public CharacterQuestions CharacterQuestions;
        public ReflectionQuestions ReflectionQuestions;
        public GameStats GameStats;

        public ExaminationEntry(CharacterData characterData, CharacterQuestions characterQuestions, ReflectionQuestions reflectionQuestions, GameStats gameStats)
        {
            CharacterData = characterData;
            CharacterQuestions = characterQuestions;
            ReflectionQuestions = reflectionQuestions;
            GameStats = gameStats;
        }
    }
}
