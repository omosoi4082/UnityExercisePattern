using UnityEngine;

public class SceneEventSOLink : EventSOLink
{
    private LoadSceneState _loadSceneState;
    public SceneEventSOLink(SceneEventSo eventSo, LoadSceneState loadSceneState) : base(eventSo, loadSceneState)
    {
        _loadSceneState = loadSceneState;
    }
    public override void GameEvent_EventRaised()
    {
        base.GameEvent_EventRaised();
        if(_gameEvent is SceneEventSo so)
        {
            _loadSceneState.ScenePath = so.ScenePath;   
        }
    }
}
