using System.Collections.Generic;
using UnityEngine;

public enum HandlerType//키 안정
{
    Pose,
    Sensor,
    Has
}

public class HandlerRegistry :MonoBehaviour
{
    [SerializeField] private List<APIConfig> aPIConfigs;
    private Dictionary<HandlerType, APIConfig> handlerMap;
    private Dictionary<HandlerType, IDataHandlerStrategy> dataMap;   

    private void Awake()
    {
        handlerMap = new Dictionary<HandlerType, APIConfig>();
        dataMap = new Dictionary<HandlerType, IDataHandlerStrategy>();
        foreach (var item in aPIConfigs)
        {
            handlerMap[item.type]= item;
            var strategy=StrategyFactory.Create(item.type); 
            if(strategy!=null)
            {
                dataMap[item.type]= strategy;
            }
        }
    }

    private void Update()
    {
        GetkeybyObj();
    }

    void GetkeybyObj()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Execute(HandlerType.Pose);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Execute(HandlerType.Sensor);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Execute(HandlerType.Has);
        }
    }

    void Execute(HandlerType type)
    {
        if(handlerMap.TryGetValue(type, out APIConfig value) && dataMap.TryGetValue(type,out var strategy))
        {
            strategy.Handle(value);
        }
        else
        {
            Debug.LogWarning("없음");
        }
    }
}
