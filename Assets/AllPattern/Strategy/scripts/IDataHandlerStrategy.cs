using UnityEngine;
//행동
public interface IDataHandlerStrategy
{
    HandlerType type { get; }//타입지정
    void Handle(APIConfig data);
}
