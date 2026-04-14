using UnityEngine;

public class PoseHandler : IDataHandlerStrategy
{

   public HandlerType type => HandlerType.Pose;

    public async void Handle(APIConfig data)
    {
        var handle = data.prefab.InstantiateAsync();//생산 주문
        var obj = await handle.Task;//기라렸다가 오브젝트 받음
        if (obj.TryGetComponent<MeshRenderer>(out var rend))
        {
            rend.material.color = data.color;
        }
    }
}
