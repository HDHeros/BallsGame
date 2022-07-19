using System.Collections.Generic;
using CodeBase.Factory;
using UnityEngine;

namespace CodeBase.ObjectsPull
{
    public class ObjectsPool<T> : IObjectsPool<T> where T : Object
    {
        private readonly IPrefabFactory<T> _factory;
        private readonly List<T> _pool;
        public ObjectsPool(IPrefabFactory<T> factory)
        {
            _factory = factory;
            _pool = new List<T>();
        }

        public T Get()
        {
            T instance = _pool.Count > 0 ? _pool[0] : _factory.Create();
            _pool.Remove(instance);
            return instance;
        }

        public void Return(T obj)
        {
            _pool.Add(obj);
        }
    }
}