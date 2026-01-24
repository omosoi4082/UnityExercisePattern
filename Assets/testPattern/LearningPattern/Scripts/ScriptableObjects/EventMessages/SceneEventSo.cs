using UnityEngine;

[CreateAssetMenu(fileName ="SceneEventSO",menuName ="DesignPatterns/SceneEventSO)")]
public class SceneEventSo : BaseEventSo
{
    [SerializeField] private string _scenePath;//_멤버 변수 구분 

    //프로퍼티property
    public string ScenePath { get; private set; }//{ get => _scenePath; set => _scenePath = value; }
    public override void OnEventRaised()
    {
        base.OnEventRaised();

        SceneEvents.SceneLoadedByPath?.Invoke(_scenePath);
    }

}
