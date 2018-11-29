using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "AvailableSprites")]
    public class AvailableSprites : ScriptableObject
    {
        [SerializeField] internal List<Sprite> HeadSprites;
        [SerializeField] internal List<Sprite> BodySprites;
    }
}
