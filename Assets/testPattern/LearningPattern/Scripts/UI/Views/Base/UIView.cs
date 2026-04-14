using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIView
{
    public const string _visibleClass = "screen-visible";
    public const string _hiddenClass = "screen-hidden";

    protected bool _hideOnAwake = true;

    protected bool _isTransparent;

    protected bool _useTransition = true;
    protected float _transitionDelay = 0.15f;

    protected VisualElement _rootElement;
    protected EventRegistry _eventRegistry;

    protected Coroutine _displayRoutine;

}
