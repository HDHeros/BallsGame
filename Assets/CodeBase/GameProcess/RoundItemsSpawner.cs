using System;
using CodeBase.ObjectsPull;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.GameProcess
{
    public class RoundItemsSpawner
    {
        public event Action<RoundItem> OnItemClick;
        
        private readonly IObjectsPool<RoundItem> _itemsPool;
        private readonly IRoundTimer _roundTimer;
        private readonly float _cameraTopBound;
        private Vector3 _leftSpawnBorder;
        private Vector3 _rightSpawnBorder;

        public RoundItemsSpawner(IObjectsPool<RoundItem> itemsPoolPool, Camera camera, IRoundTimer roundTimer)
        {
            _itemsPool = itemsPoolPool;
            _roundTimer = roundTimer;
            _cameraTopBound = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight)).y;
            InitializeSpawnRange(camera);
        }

        public void SpawnItem(RoundItemConfig itemConfig)
        {
            RoundItem item = _itemsPool.Get();
            item.Setup(itemConfig, _cameraTopBound, _roundTimer);
            item.Transform.position = GetItemPosition(item);
            item.OnOutOfBounds += ItemOnOutOfBounds;
            item.OnClick += ItemOnClick;
            item.gameObject.SetActive(true);
        }

        private void ItemOnClick(RoundItem item)
        {
            HideItem(item);
            item.OnClick -= ItemOnClick;
            OnItemClick?.Invoke(item);
        }

        private void ItemOnOutOfBounds(RoundItem item) => 
            HideItem(item);

        private void HideItem(RoundItem item)
        {
            item.OnOutOfBounds -= ItemOnOutOfBounds;
            item.OnClick -= ItemOnClick;
            item.gameObject.SetActive(false);
            _itemsPool.Return(item);
        }

        private void InitializeSpawnRange(Camera camera)
        {
            _leftSpawnBorder = camera.ScreenToWorldPoint(Vector3.zero);
            _rightSpawnBorder = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0f));
            _leftSpawnBorder.z = 0;
            _leftSpawnBorder.z = 0;
        }

        private Vector3 GetItemPosition(RoundItem item)
        {
            Vector3 offset = item.SpriteRenderer.bounds.size / 2;
            var leftBorder = _leftSpawnBorder;
            var rightBorder = _rightSpawnBorder;
            leftBorder.y -= offset.y;
            leftBorder.x += offset.x;
            rightBorder -= offset;
            
            return Vector3.Lerp(leftBorder, rightBorder, Random.Range(0f, 1f));
        }
    }
}