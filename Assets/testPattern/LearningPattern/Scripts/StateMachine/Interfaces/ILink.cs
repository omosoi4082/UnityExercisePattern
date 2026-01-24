using UnityEngine;

public interface ILink
{
    bool validate(out IState nextState);
    void Enable() { }

    void Disable() { }  
}