namespace Scripts.Audio
{
    // While this isn't the best implementation it does make it easier for other scripts to play sound effects.
    // The limitations of this is that the volume of two clips cannot be *exactly* the same
    /// <summary>The integer value of the sound effects represent the volume. 100 = full volume</summary>
    public enum SoundEffect
    {
        PlayerSkip = 30,
        PlayerWeaponShot = 20,
        BulletImpact = 100
    }
}
