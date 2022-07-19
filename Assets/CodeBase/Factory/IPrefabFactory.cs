using UnityEngine;

namespace CodeBase.Factory
{
    public interface IPrefabFactory<out T> where T : Object
    {
        public T Create();
    }
}