namespace Scripts.Examination
{
    [System.Serializable]
    public struct CharacterData
    {
        internal bool HasChangedName { get; set; }
        internal string CharacterName { get; set; }

        internal int SpriteChoices { get; set; } // Per area (30 heads & 30 body choices = 30)
        internal int VisitedHeads { get; set; }
        internal int VisitedBodies { get; set; }

        internal bool IsMatchingSet { get; set; }
        internal float TimeSpent { get; set; }

        internal int SatisfactionScore { get; set; }

    }
}
