using System;
using System.Collections;
using UnityEngine;

public class LoadSceneState : AbstractState
{
    public Action LoadCompleted;
    public Action StateExited;

    string _scenePath;
    SceneLoader _sceneLoader;

    public override string debugName => $"{nameof(LoadSceneState)}:{_scenePath}";
    public string ScenePath
    {
        get=> _scenePath;
        set => _scenePath = value;
    }
    public LoadSceneState(SceneLoader sceneLoader,Action loadCompleted=null)
    {
        _sceneLoader = sceneLoader;
        LoadCompleted = loadCompleted;
    }
    public override IEnumerator Execute()
    {
        if(_sceneLoader != null) {
            yield return _sceneLoader.LoadScene(_scenePath);
        }
        LoadCompleted?.Invoke();
        if (_debug)
            base.LogCurrentState();
    }
    public override void Exit()
    {
        base.Exit();
        StateExited?.Invoke();  
        if(_debug) Debug.Log("Exiting state: " + this.debugName);
    }
}
