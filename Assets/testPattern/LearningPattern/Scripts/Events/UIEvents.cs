using System;
using UnityEngine;

public static class UIEvents
{
    public static Action ScreenClosed;

    public static Action MainMenuShown;
    public static Action SolidMenuShown;
    public static Action DesingMenuShown;
    public static Action ResourceMenuShown;

    public static Action<string> URLOpened;
    public static Action DemoScreenShown;
}
