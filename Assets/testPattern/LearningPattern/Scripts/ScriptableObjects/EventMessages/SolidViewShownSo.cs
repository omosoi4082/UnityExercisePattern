using UnityEngine;

[CreateAssetMenu(fileName="DisignPatterns/SolidviewSo")]
public class SolidViewShownSo : BaseEventSo
{
    public override void OnEventRaised()
    {
        base.OnEventRaised();
        UIEvents.SolidMenuShown?.Invoke();
    }
}
