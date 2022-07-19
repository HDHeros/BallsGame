using UnityEngine;

namespace CodeBase.ObjectsPull
{
    public interface IObjectsPool<T> where T : Object
    {
        public T Get();
        public void Return(T obj);
    }
}