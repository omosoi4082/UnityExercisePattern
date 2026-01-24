using System;
using System.Collections;
using UnityEngine;

public static class Coroutines
{
    private static MonoBehaviour _coroutineRunner;
    public static bool isInitialized=> _coroutineRunner != null;//없으면 false

    public static void Initialize(MonoBehaviour runner)
    {
        _coroutineRunner = runner;  
    }

    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        if (coroutine == null)
        {
            throw new InvalidOperationException("coroutineRunner is not initialized");
        }
        return _coroutineRunner.StartCoroutine(coroutine);  
    }
    public static void StopCoroutine(Coroutine coroutine) { 
        if(_coroutineRunner != null)
        {
            _coroutineRunner.StopCoroutine(coroutine);  
        }
    }
    public static void StopCoroutine(ref Coroutine coroutine)//받은 coroutine까지 null 만듬
    {
        if(_coroutineRunner != null&&coroutine!=null)
        {
            _coroutineRunner.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}