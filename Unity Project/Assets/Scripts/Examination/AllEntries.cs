namespace Scripts.Examination
{
    [System.Serializable]
    public struct AllEntries
    {
        // ReSharper disable once InconsistentNaming - needs to be lowercase for json deserializing to work
        public ExaminationEntry[] examinations;
    }
}
