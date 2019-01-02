using UnityEngine;

namespace Scripts.Audio
{
    [CreateAssetMenu(menuName = "Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        [Header("General")]
        [SerializeField] internal bool ShouldLoop;

        [Header("Clips")]
        [Tooltip("Defaults to the first clip if false")]
        [SerializeField] internal bool RandomizeClip = false;
        [SerializeField] internal AudioClip[] Clips;

        [Header("Volume")]
        [Tooltip("Defaults to MinVolume if false")]
        [SerializeField] internal bool RandomizeVolume = true;
        [SerializeField] internal float MinVolume = 0.4f;
        [SerializeField] internal float MaxVolume = 0.6f;

        [Header("Pitch")]
        [Tooltip("Defaults to 1 if false")]
        [SerializeField] internal bool RandomizePitch = true;
        [SerializeField] internal float MinPitch = 0.95f;
        [SerializeField] internal float MaxPitch = 1.05f;
    }
}
