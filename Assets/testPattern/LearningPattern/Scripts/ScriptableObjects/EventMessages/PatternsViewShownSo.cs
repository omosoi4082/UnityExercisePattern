using UnityEngine;
[CreateAssetMenu(fileName = "DisignPatterns/PatternsViewShownSo")]

public class PatternsViewShownSo : BaseEventSo
{
    public override void OnEventRaised()
    {
        base.OnEventRaised();
        UIEvents.DesingMenuShown?.Invoke();
    }
}
