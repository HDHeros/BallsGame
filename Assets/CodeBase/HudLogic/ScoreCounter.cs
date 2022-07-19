using System.Globalization;
using TMPro;
using UnityEngine;

namespace CodeBase.HudLogic
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreAmountText;
        [SerializeField] private CanvasGroup _canvasGroup;
        private IScore _playerScore;

        public void Setup(IScore playerScore) => 
            _playerScore = playerScore;

        public void Show()
        {
            PlayerScoreOnScoreChanged(_playerScore.GetScore());
            _playerScore.OnScoreChanged += PlayerScoreOnScoreChanged;
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _playerScore.OnScoreChanged -= PlayerScoreOnScoreChanged;
            _canvasGroup.alpha = 0;
        }

        private void PlayerScoreOnScoreChanged(float scoreAmount) => 
            _scoreAmountText.SetText(Mathf.Round(scoreAmount).ToString(CultureInfo.InvariantCulture));
    }
}