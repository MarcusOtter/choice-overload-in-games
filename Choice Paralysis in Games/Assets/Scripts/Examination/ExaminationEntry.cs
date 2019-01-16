namespace Scripts.Examination
{
    [System.Serializable]
    public struct ExaminationEntry
    {
        public SubjectQuestions SubjectQuestions;
        public CharacterData CharacterData;
        public CharacterQuestions CharacterQuestions;
        public ReflectionQuestions ReflectionQuestions;
        public GameStats GameStats;

        public ExaminationEntry(SubjectQuestions subjectQuestions, CharacterData characterData, CharacterQuestions characterQuestions, ReflectionQuestions reflectionQuestions, GameStats gameStats)
        {
            SubjectQuestions = subjectQuestions;
            CharacterData = characterData;
            CharacterQuestions = characterQuestions;
            ReflectionQuestions = reflectionQuestions;
            GameStats = gameStats;
        }
    }
}
