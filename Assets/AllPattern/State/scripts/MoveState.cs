using UnityEngine;

public class MoveState : IPlayerState
{
    public Color meshColor { get; set; } = Color.blue;

    public void Enter(StatePlayer player)
    {
        Debug.Log("move");
    }

    public void Execte(StatePlayer player)
    {
        player.OnMove();//중력 적용
        player.material.material.color = meshColor;
        if (player.isJump)
        {
            player.ChangeState(new JumpState());
            return;
        }
        if (!player.isMove)
        {
            player.ChangeState(new IdleState());

        }
    }


    public void Exit(StatePlayer player)
    {

    }
}
