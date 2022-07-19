using CodeBase.Factory;
using CodeBase.GameProcess;
using CodeBase.HudLogic;
using CodeBase.ObjectsPull;
using UnityEngine;

namespace CodeBase
{
    public class Game : MonoBehaviour, ICoroutineRunner
    {
        private const string BallPrefabPath = "GameObjects/Ball";
        private const string HudPrefabPath = "Hud";

        [SerializeField] private RoundConfig _roundConfig; 
        private IScore _playerScore;
        private IPrefabFactory<RoundItem> _ballsFactory;
        private IObjectsPool<RoundItem> _ballsPool;
        private RoundController _roundController;
        private Hud _hud;

        private void Awake()
        {
            _playerScore = new Score();
            _ballsFactory = new PrefabsFactory<RoundItem>(BallPrefabPath);
            _ballsPool = new ObjectsPool<RoundItem>(_ballsFactory);
            _roundController = new RoundController(_playerScore, _ballsPool, this, _roundConfig, Camera.main);
            InstantiateHud();
        }

        private void Start()
        {
            _roundController.Start();
        }

        private void InstantiateHud()
        {
            var hud = Instantiate(Resources.Load<Hud>(HudPrefabPath));
            hud.Setup(_roundController, _playerScore);
            
        }
    }
}