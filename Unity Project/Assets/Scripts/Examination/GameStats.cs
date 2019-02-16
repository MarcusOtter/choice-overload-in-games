namespace Scripts.Examination
{
    [System.Serializable]
    public struct GameStats
    {
        public int ShotsFired;
        public int ShotsHit;
        public int ShotsMissed;
        public double Accuracy;
        public int KillCount;

        public double FirstAttemptTime;
        public double SecondAttemptTime;
    }
}
