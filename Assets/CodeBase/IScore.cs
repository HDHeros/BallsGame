using System;

namespace CodeBase
{
    public interface IScore
    {
        public event Action<float> OnScoreChanged;
        public event Action OnScoreReset;
        
        public void AppendScore(float amount);
        public float GetScore();
        public void ResetScore();
    }
}