using UnityEngine;
[CreateAssetMenu(fileName = "DisignPatterns/HomeViewShownSo")]

public class HomeViewShownSo : BaseEventSo
{
    public override void OnEventRaised()
    {
        base.OnEventRaised();
        UIEvents.MainMenuShown?.Invoke();
    }
}

