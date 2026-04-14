using UnityEngine;

public class HasHandler : IDataHandlerStrategy
{
    public HandlerType type => HandlerType.Has;

    public async void Handle(APIConfig data)
    {
        var handle = data.prefab.InstantiateAsync();
        var obj = await handle.Task;
        if (obj.TryGetComponent<MeshRenderer>(out var rend))
        {
            rend.material.color = data.color;
            obj.transform.position += Vector3.right * 2f;
        }
    }
}
