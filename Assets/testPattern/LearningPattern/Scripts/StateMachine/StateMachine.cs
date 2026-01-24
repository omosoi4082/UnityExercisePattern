using System;
using System.Collections;
using UnityEngine;

public class StateMachine
{
    public IState currentState { get; private set; }
    Coroutine _cerrentPlayCoroutine;
    bool _playLock;

    Coroutine _loopCoroutine;
    //이전상태 중단후 새상태 실행
    public virtual void SetCurrentState(IState state)
    {
        if (state == null)
            throw new ArgumentNullException(nameof(state));//throw다음 넘어가지않고 오류 반환,null(ArgumentNullException)일시
        if (currentState != null && _cerrentPlayCoroutine != null)
        {
            Skip();//중단
        }
        currentState = state;
        Coroutines.StartCoroutine(Play());
    }
    //시작 
    IEnumerator Play()
    {
        if (!_playLock)
        {
            _playLock = true;
            currentState.Enter();
            _cerrentPlayCoroutine = Coroutines.StartCoroutine(currentState.Execute());
            yield return _cerrentPlayCoroutine;
            _cerrentPlayCoroutine = null;
        }
    }
    //중단
    void Skip()
    {
        if (currentState == null)
            throw new Exception($"{nameof(currentState)} is null.");
        if (_cerrentPlayCoroutine != null)
        {
            Coroutines.StopCoroutine(ref _cerrentPlayCoroutine);
            currentState.Exit();
            _cerrentPlayCoroutine = null;
            _playLock = false;
        }
    }

    public virtual void Run(IState state)
    {
        SetCurrentState(state);
        Run();
    }
    //루프켬
    public virtual void Run()
    {
        if (_loopCoroutine != null) return;
        _loopCoroutine = Coroutines.StartCoroutine(Loop());
    }
    //현재 상태체크 다음 상태 링크
    protected virtual IEnumerator Loop()
    {
        while (true)
        {
            if (currentState != null && _cerrentPlayCoroutine == null)
            {
                if (currentState.Validateinks(out var nextState))
                {
                    if (_playLock)
                    {
                        currentState.Exit();
                        _playLock = false;
                    }
                    currentState.DisableLinks();
                    SetCurrentState(nextState);
                    currentState.EnableLinks();
                }
            }
            yield return null;
        }
    }
    public bool isRunning=>_loopCoroutine != null;  
    //메인루프 중단
    public void Stop()
    {
        if(_loopCoroutine==null) return;    //이미중단됨
        if(currentState!=null&&_cerrentPlayCoroutine!=null) {
            Skip();
        }
        Coroutines.StopCoroutine(ref _loopCoroutine);
        currentState = null;

    }
}
