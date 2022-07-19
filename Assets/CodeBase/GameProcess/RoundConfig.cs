using UnityEngine;

namespace CodeBase.GameProcess
{
    [CreateAssetMenu(menuName = "Configs/Round Config", fileName = "New Round Config")]
    public class RoundConfig : ScriptableObject
    {
        public float RoundDuration;
        public float ItemsSpawnDelay;

        public RoundItemConfig RoundItemConfig;
    }
}