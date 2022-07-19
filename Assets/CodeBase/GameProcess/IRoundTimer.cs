using System;

namespace CodeBase.GameProcess
{
    public interface IRoundTimer
    {
        public event Action OnRoundTimeChanged;
        
        public float CurrentNormalizedRoundDuration { get; }
        public float CurrentRoundDuration { get; }
        public float TotalRoundDuration { get; }
    }
}