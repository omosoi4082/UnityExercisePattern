using UnityEngine;

public interface IPlayerState : IColorable
{
    void Enter(StatePlayer player);
    void Execte(StatePlayer player);
    void Exit(StatePlayer player);
}
