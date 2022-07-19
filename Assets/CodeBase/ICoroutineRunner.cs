using System.Collections;
using UnityEngine;

namespace CodeBase
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}