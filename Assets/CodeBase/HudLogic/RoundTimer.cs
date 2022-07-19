using System;
using System.Collections;
using CodeBase.GameProcess;
using TMPro;
using UnityEngine;

namespace CodeBase.HudLogic
{
    public class RoundTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private CanvasGroup _canvasGroup;
        private IRoundTimer _roundController;

        public void Setup(IRoundTimer roundController) => 
            _roundController = roundController;

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _roundController.OnRoundTimeChanged += RoundControllerOnRoundTimeChanged;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _roundController.OnRoundTimeChanged -= RoundControllerOnRoundTimeChanged;
        }
        
        private void RoundControllerOnRoundTimeChanged()
        {
            TimeSpan timeUntilEndRound =
                TimeSpan.FromSeconds(_roundController.TotalRoundDuration - _roundController.CurrentRoundDuration);
            _timerText.SetText(timeUntilEndRound.ToString(@"mm\:ss"));
        }
    }
}