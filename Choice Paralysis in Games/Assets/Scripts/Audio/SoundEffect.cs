using UnityEngine;

namespace Scripts.Audio
{
    [CreateAssetMenu(menuName = "Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        [Header("General settings")]
        [SerializeField] internal AudioClip Clip;
        [SerializeField] internal bool ShouldLoop;

        [Header("Volume")]
        [SerializeField] [Tooltip("Defaults to MinVolume if false")] internal bool RandomizeVolume = true;
        [SerializeField] internal float MinVolume = 0.4f;
        [SerializeField] internal float MaxVolume = 0.6f;

        [Header("Pitch")]
        [SerializeField] [Tooltip("Defaults to 1 if false")] internal bool RandomizePitch = true;
        [SerializeField] internal float MinPitch = 0.95f;
        [SerializeField] internal float MaxPitch = 1.05f;
    }
}
