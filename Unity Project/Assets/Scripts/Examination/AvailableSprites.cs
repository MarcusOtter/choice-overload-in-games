using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Examination
{
    [CreateAssetMenu(menuName = "AvailableSprites")]
    public class AvailableSprites : ScriptableObject
    {
        [SerializeField] internal int OptionAmount;
        [SerializeField] internal List<Sprite> HeadSprites;
        [SerializeField] internal List<Sprite> BodySprites;
    }
}
