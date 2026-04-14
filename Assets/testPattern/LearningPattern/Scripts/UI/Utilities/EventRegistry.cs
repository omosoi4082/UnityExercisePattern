using System;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// UI Toolkit에서 RegisterCallback만 쓰고 UnregisterCallback을 안 하면 비활성화 후에도 이벤트가 살아있는 문제가 발생
///모든 이벤트 해제 로직을 하나의 델리게이트 체인으로 관리
///Dispose() 한 번으로 전부 정리
///EventRegistry를 한 UI 단위로 관리해야 함
/// </summary>
public class EventRegistry : IDisposable
{
    Action _unregisterAction;

    //기본 형태 (이벤트 데이터 필요할 때)
    public void RegisterCallback<TEvnet>(VisualElement visualElement,Action<TEvnet> callback)where TEvnet : EventBase<TEvnet>,new()
    {
        EventCallback<TEvnet> eventCallback=new EventCallback<TEvnet>(callback);   
        visualElement.RegisterCallback(eventCallback);
        _unregisterAction += () => visualElement.UnregisterCallback(eventCallback);
    }
    //단순 콜백 버전 (이벤트 데이터 필요 없을 때)
    public void RegisterCallback<TEvnet>(VisualElement visualElement, Action callback) where TEvnet : EventBase<TEvnet>, new()
    {
        EventCallback<TEvnet> eventCallback = new EventCallback<TEvnet>((evt)=>callback());
        visualElement.RegisterCallback(eventCallback);
        _unregisterAction += () => visualElement.UnregisterCallback(eventCallback);
    }
    //값 변경 이벤트 (Slider, TextField 등)
    public void RegisterValueChangedCallback<T>(BindableElement bindableElement, Action<T> callback) where T : struct
    {
        EventCallback<ChangeEvent<T>> eventCallback = new EventCallback<ChangeEvent<T>>((evt) => callback(evt.newValue));
        bindableElement.RegisterCallback(eventCallback);
        _unregisterAction += () => bindableElement.UnregisterCallback(eventCallback);
    }
    public void Dispose()
    {
       _unregisterAction?.Invoke(); 
        _unregisterAction = null;
    }

}
