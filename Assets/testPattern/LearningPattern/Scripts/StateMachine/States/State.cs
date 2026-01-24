using System;
using System.Collections;
using UnityEngine;

public class State : AbstractState
{
    readonly Action _OnExecute;
    public State(Action onExecute,string stateName=nameof(State),bool enableDebug=false)
    {
        _OnExecute = onExecute;
        debugName = stateName;   
        debugEnabled = enableDebug;
    }

    public override IEnumerator Execute()
    {
        yield return null;
        if(_debug)
            base.LogCurrentState();
        _OnExecute?.Invoke();
    }

}
