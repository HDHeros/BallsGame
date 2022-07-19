using UnityEngine;

namespace CodeBase.Factory
{
    public class PrefabsFactory<T> : IPrefabFactory<T> where T : Object
    {
        private T _prefab;

        public PrefabsFactory(string prefabPath) => 
            _prefab = Resources.Load<T>(prefabPath);

        public T Create() => 
            Object.Instantiate(_prefab);
    }
}