using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace CodeBase.GameProcess
{
    public abstract class RoundItem : MonoBehaviour, IPointerClickHandler
    {
        public event Action<RoundItem> OnOutOfBounds;
        public event Action<RoundItem> OnClick;

        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public Transform Transform => _transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public float Score => _config.BaseScoreAmount + _normalizedScaleValue * _config.ScoreCoefficientPerScale;
        protected float Speed => _config.BaseSpeed + 
                                 + _normalizedScaleValue * _config.ScaleSpeedCoefficient
                                 + _roundTimer.CurrentNormalizedRoundDuration * _config.RoundTimeSpeedCoefficient;
        
        private RoundItemConfig _config;
        private float _topCameraBoundValue;
        private IRoundTimer _roundTimer;
        private float _normalizedScaleValue;

        public virtual void Setup(RoundItemConfig config, float topCameraBoundValue, IRoundTimer roundTimer)
        {
            _topCameraBoundValue = topCameraBoundValue;
            _config = config;
            _roundTimer = roundTimer;
            SetRandomScale(config);
            SetRandomColor();
        }

        protected virtual void Update()
        {
            if(IsOutOfTopBound()) OnOutOfBounds?.Invoke(this);
        }

        private bool IsOutOfTopBound() => 
            _topCameraBoundValue < _spriteRenderer.bounds.min.y;

        public void OnPointerClick(PointerEventData eventData) => 
            OnClick?.Invoke(this);

        private void SetRandomScale(RoundItemConfig config)
        {
            Transform.localScale = GetRandomScale(config);
            _normalizedScaleValue = Mathf.InverseLerp(_config.MaxScaleMultiplier, _config.MinScaleMultiplier,
                Transform.localScale.x);
        }

        private static Vector3 GetRandomScale(RoundItemConfig config) => 
            Vector3.one * Mathf.Lerp(config.MinScaleMultiplier, config.MaxScaleMultiplier, Random.Range(0f, 1f));

        private void SetRandomColor()
        {
            _spriteRenderer.color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        }
    }
}