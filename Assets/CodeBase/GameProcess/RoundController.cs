using System;
using System.Collections;
using CodeBase.ObjectsPull;
using UnityEngine;

namespace CodeBase.GameProcess
{
    public class RoundController : IRoundTimer
    {
        public event Action OnRoundStarted;
        public event Action OnRoundTimeChanged;
        public event Action OnRoundEnded;
        
        public float CurrentNormalizedRoundDuration => _normalizedRoundDuration;
        public float CurrentRoundDuration => _normalizedRoundDuration * _roundConfig.RoundDuration; 
        public float TotalRoundDuration => _roundConfig.RoundDuration;
        
        private readonly IScore _playerScore;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly RoundConfig _roundConfig;
        private readonly RoundItemsSpawner _roundItemsSpawner;
        private Coroutine _roundLoopCoroutine;
        private float _normalizedRoundDuration;

        public RoundController(IScore playerScore, IObjectsPool<RoundItem> roundItemsPool, 
            ICoroutineRunner coroutineRunner, RoundConfig roundConfig, Camera camera)
        {
            _playerScore = playerScore;
            _coroutineRunner = coroutineRunner;
            _roundConfig = roundConfig;
            _roundItemsSpawner = new RoundItemsSpawner(roundItemsPool, camera, this);
        }

        public void Start()
        {
            _playerScore.ResetScore();
            _roundLoopCoroutine = _coroutineRunner.StartCoroutine(StartRoundLoop());
        }

        private void RoundItemsSpawnerOnItemClick(RoundItem item)
        {
            _playerScore.AppendScore(item.Score);
        }

        private IEnumerator StartRoundLoop()
        {
            float currentRoundDuration = 0;
            float timeBeforeSpawn = 0;
            _normalizedRoundDuration = 0;
            OnRoundStarted?.Invoke();
            _roundItemsSpawner.OnItemClick += RoundItemsSpawnerOnItemClick;
            while (currentRoundDuration < _roundConfig.RoundDuration)
            {
                currentRoundDuration += Time.deltaTime;
                timeBeforeSpawn -= Time.deltaTime;
                _normalizedRoundDuration = currentRoundDuration / _roundConfig.RoundDuration;
                
                if (timeBeforeSpawn <= 0)
                {
                    timeBeforeSpawn = _roundConfig.ItemsSpawnDelay;
                    _roundItemsSpawner.SpawnItem(_roundConfig.RoundItemConfig);
                }
                
                OnRoundTimeChanged?.Invoke();
                yield return null;
            }
            _roundItemsSpawner.OnItemClick -= RoundItemsSpawnerOnItemClick;
            OnRoundEnded?.Invoke();
        }
    }
}