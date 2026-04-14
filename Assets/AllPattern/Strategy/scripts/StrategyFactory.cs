using UnityEngine;

public static class StrategyFactory
{
    public static IDataHandlerStrategy Create(HandlerType type)
    {
        return type switch
        {
            HandlerType.Pose => new PoseHandler(),
            HandlerType.Sensor => new SensorHandler(),
            HandlerType.Has => new HasHandler(),
            _ => null
        };
    }
}
