using UnityEngine;
using System.Collections.Generic;

public class LearGameStateManager : MonoBehaviour
{
    [Header("preload(splash screen)")]
    //먼저 로드되야 하는프리팹 텍스처,사운드등 
    [SerializeField] GameObject[] _preloadedAssers;

    [Header("Menu Events")]
    [Tooltip("notify listeners that the menu is active")]//구독자에 SLOID활성화 알림 
    [SerializeField] BaseEventSo _solidMenuShown;
    [SerializeField] BaseEventSo _designPatternMenuShown;
    [SerializeField] BaseEventSo _resourcesMenuShown;
    [Tooltip("Backs out of the currently active UI screen")]
    [SerializeField] BaseEventSo _screenClosed;

    [Header("Scene Events")]
    [SerializeField] List<SceneEventSo> _solidSceneLoadEvents;
    [SerializeField] List<SceneEventSo> _designSceneLoadEvents;

    [SerializeField] bool _debug;//상태 로그를 보이게 할것인가

    StateMachine _statemachine = new StateMachine();

    IState _mainMenu;
    IState _solidMenu;
    IState _designMenu;
    IState _resourceMenu;

    LoadSceneState _solidState;
    LoadSceneState _designState;

    SceneLoader _sceneLoader;
    private void Start()
    {
        Initialze();
    }
    private void Initialze()
    {
        //처음 코루틴 할당
        Coroutines.Initialize(this);
        //처음 설치로드 (리소스)
        InstantiatePreloadedAssets();
        //정의
        SetStates();//상태 
        AddLinks();

        RunStateMachin();
        if (_solidState != null)
            _solidState.StateExited += SolidSceneState_StateExited;
        if (_designState != null)
            _designState.StateExited += DesignSceneState_StateExited;
    }
    private void OnDisable()
    {
        if (_solidState != null)
            _solidState.StateExited -= SolidSceneState_StateExited;
        if (_designState != null)
            _designState.StateExited -= DesignSceneState_StateExited;
    }
    private void InstantiatePreloadedAssets()
    {
        foreach (var item in _preloadedAssers)
        {
            if (item != null)
                Instantiate(item);
        }
    }
    private void SetStates()
    {
        //메뉴 상태
        _mainMenu = new State(null, "MainMenuState", _debug);
        _solidMenu = new State(null, "SolidMenuState", _debug);
        _designMenu = new State(null, "DesignMenuState", _debug);
        _resourceMenu = new State(null, "ResourceMenuState", _debug);
        //씬 상태
        _solidState = new LoadSceneState(_sceneLoader, null);
        _designState = new LoadSceneState(_sceneLoader, null);
    }
    private void AddLinks()
    {
        //메뉴 들어가기
        _mainMenu.AddLink(new EventSOLink(_solidMenuShown, _solidMenu));
        _mainMenu.AddLink(new EventSOLink(_designPatternMenuShown, _designMenu));
        _mainMenu.AddLink(new EventSOLink(_resourcesMenuShown, _resourceMenu));

        //뒤로가기 버튼(메인으로 나가기)
        _solidMenu.AddLink(new EventSOLink(_screenClosed, _mainMenu));
        _designMenu.AddLink(new EventSOLink(_screenClosed, _mainMenu));
        _resourceMenu.AddLink(new EventSOLink(_screenClosed, _mainMenu));

        //solid메뉴 선택 화면 상태(들어가기)
        foreach (SceneEventSo so in _solidSceneLoadEvents)
        {
            _solidMenu.AddLink(new SceneEventSOLink(so, _solidState));
        }
        //디자인패턴 선택 화면 상태 (들어가기)
        foreach (SceneEventSo so in _designSceneLoadEvents)
        {
            _designMenu.AddLink(new SceneEventSOLink(so, _designState));
        }
        //뒤로가기 솔리드,패턴 메뉴 화면
        _solidState.AddLink(new EventSOLink(_screenClosed, _solidMenu));
        _designState.AddLink(new EventSOLink(_screenClosed, _designMenu));
    }

    private void RunStateMachin()
    {
        _statemachine.Run(_mainMenu);
        UIEvents.MainMenuShown?.Invoke();
    }

    private void SolidSceneState_StateExited()
    {
        SceneEvents.LastSceneUnloaded?.Invoke();
    }
    private void DesignSceneState_StateExited()
    {
        SceneEvents.LastSceneUnloaded?.Invoke();
    }
}
