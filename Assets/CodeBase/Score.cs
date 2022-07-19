using System;

namespace CodeBase
{
    public class Score : IScore
    {
        public event Action<float> OnScoreChanged;
        public event Action OnScoreReset;

        private float _currentScore;
        
        public void AppendScore(float amount)
        {
            if (amount < 0) throw new ArgumentException();
            _currentScore += amount;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public float GetScore() => 
            _currentScore;

        public void ResetScore()
        {
            _currentScore = 0;
            OnScoreReset?.Invoke();
        }
    }
}