namespace Scripts.Examination
{
    [System.Serializable]
    public struct CharacterData
    {
        public bool HasChangedName;
        public string CharacterName;
        public double TimeSpentOnName;

        public int SpriteOptionAmount; // Per section (30 heads & 30 body choices = 30)

        public double VisitedHeadsPercentage;
        public double VisitedBodiesPercentage;

        public int SelectedHead;
        public int SelectedBody;

        public bool IsMatchingSet;
        public double TimeSpentOnCharacter;
       
        public CharacterData(bool hasChangedName, string characterName, double timeSpentOnName, int spriteOptionAmount, double visitedHeadsPercentage, double visitedBodiesPercentage, int selectedHead, int selectedBody, bool isMatchingSet, double timeSpentOnCharacter)
        {
            HasChangedName = hasChangedName;
            CharacterName = characterName;
            TimeSpentOnName = timeSpentOnName;
            SpriteOptionAmount = spriteOptionAmount;
            VisitedHeadsPercentage = visitedHeadsPercentage;
            VisitedBodiesPercentage = visitedBodiesPercentage;
            SelectedHead = selectedHead;
            SelectedBody = selectedBody;
            IsMatchingSet = isMatchingSet;
            TimeSpentOnCharacter = timeSpentOnCharacter;
        }
    }
}
