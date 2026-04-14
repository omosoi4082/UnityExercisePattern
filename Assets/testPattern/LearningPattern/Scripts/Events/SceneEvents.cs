using System;
using UnityEngine;

public static class SceneEvents
{
    public static Action ExitingApplication;
    public static Action PreloadCompoleted;
    public static Action<float>LoadProgressUpdated;
    public static Action SceneReloaded;
    public static Action NextSceneReloaded;
    public static Action LastSceneUnloaded;
    //경로로 장면 추가
    public static Action<string> SceneLoadedByPath;    
    public static Action<string> SceneUnloadedByPath;
    public static Action<int> SceneLoadedByIndex;
}