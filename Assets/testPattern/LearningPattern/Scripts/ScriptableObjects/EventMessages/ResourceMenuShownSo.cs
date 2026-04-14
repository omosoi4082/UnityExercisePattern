using UnityEngine;
[CreateAssetMenu(fileName = "DisignPatterns/ResourceMenuShownSo")]

public class ResourceMenuShownSo : BaseEventSo
{
    public override void OnEventRaised()
    {
        base.OnEventRaised();
        UIEvents.ResourceMenuShown?.Invoke();
    }
}

