using UnityEngine;

namespace CodeBase.GameProcess
{
    [CreateAssetMenu(menuName = "Configs/Round Item Config", fileName = "New Item Config")]
    public class RoundItemConfig : ScriptableObject
    {
        public float MinScaleMultiplier;
        public float MaxScaleMultiplier;
        public int BaseScoreAmount;
        public float ScoreCoefficientPerScale;
        public int BaseSpeed;
        public float RoundTimeSpeedCoefficient;
        public float ScaleSpeedCoefficient;
    }
}