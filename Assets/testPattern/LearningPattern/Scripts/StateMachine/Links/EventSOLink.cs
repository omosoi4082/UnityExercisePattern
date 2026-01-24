using UnityEngine;

public class EventSOLink : ILink
{
    private IState _nextState;
    private bool _eventRaised;
    protected BaseEventSo _gameEvent;
    //이벤트 등록과 다음 상태
    public EventSOLink(BaseEventSo eventSo, IState nextState)
    {
        _gameEvent = eventSo;
        _nextState = nextState;
    }
    //이벤트가 있으면 참
    public bool validate(out IState nextState)
    {
        nextState = _eventRaised ? _nextState : null;
        return _eventRaised;
    }
    public void Enable()
    {
        _gameEvent.EventRaised += GameEvent_EventRaised;
        _eventRaised = false;
    }
    public void Disable()
    {
        _gameEvent.EventRaised -= GameEvent_EventRaised;
        _eventRaised = false;
    }

    public virtual void GameEvent_EventRaised()
    {
        _eventRaised = true;
    }

}
