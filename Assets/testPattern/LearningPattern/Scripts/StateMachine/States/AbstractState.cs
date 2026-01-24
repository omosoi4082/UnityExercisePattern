using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//StateMachine에 상태 공통 기능 
public abstract class AbstractState : IState
{
    public virtual string debugName { get; set; }  //디버깅시 사용
    protected bool _debug = false;
    readonly List<ILink> _links = new List<ILink>();
    public bool debugEnabled { get => _debug; set => _debug = value; }
    public void Enter()
    {
        EnableLinks();
    }
    public void AddLink(ILink link)
    {
        if (!_links.Contains(link))
        {
            _links.Add(link);
        }
    }

    public void DisableLinks()
    {
        foreach (var item in _links)
        {
            item.Disable();
        }
    }

    public void EnableLinks()
    {
        foreach (var item in _links)
        {
            item.Enable();
        }
    }

    public abstract IEnumerator Execute();

    public virtual void Exit()
    {

    }

    public void RemoveAllLinks()
    {
        _links.Clear();
    }

    public void RemoveLink(ILink link)
    {
        if (_links.Contains(link))
        {
            _links.Remove(link);
        }
    }

    public bool Validateinks(out IState nextState)
    {
        if (_links != null && _links.Count > 0)
        {
            foreach (var link in _links)
            {
                var result = link.validate(out nextState);
                if (result)
                {
                    return true;
                }
            }
        }
        nextState=null; return false;
    }

    public virtual void LogCurrentState()
    {
        if(_debug)
        {
            string message= "[AbstractState] Current state: " + debugName + "(" + this.GetType().Name + ") -------";
            message = message.Substring(0, 100);
            Debug.Log(message);
        }
    }

}
