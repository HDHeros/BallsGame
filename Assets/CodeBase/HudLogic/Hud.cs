using CodeBase.GameProcess;
using UnityEngine;

namespace CodeBase.HudLogic
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private RoundTimer _roundTimer;
        [SerializeField] private StartGamePopUp _startGamePopUp;
        private RoundController _roundController;
        private IScore _playerScore;

        public void Setup(RoundController roundController, IScore playerScore)
        {
            _roundController = roundController;
            _playerScore = playerScore;
            _scoreCounter.Setup(_playerScore);
            _roundTimer.Setup(_roundController);
            _roundController.OnRoundStarted += RoundControllerOnRoundStarted;
            _roundController.OnRoundEnded += RoundControllerOnRoundEnded;
        }

        private void RoundControllerOnRoundStarted()
        {
            _scoreCounter.Show();
            _roundTimer.Show();
            _startGamePopUp.Hide();
        }

        private void RoundControllerOnRoundEnded()
        {
            _scoreCounter.Hide();
            _roundTimer.Hide();
            _startGamePopUp.Show(_playerScore, _roundController.Start);
        }

        private void OnDestroy()
        {
            _roundController.OnRoundStarted -= RoundControllerOnRoundStarted;
            _roundController.OnRoundEnded -= RoundControllerOnRoundEnded;
        }
    }
}