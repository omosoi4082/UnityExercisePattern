using UnityEngine;

public class IdleState : IPlayerState
{
    public Color meshColor { get; set; } = Color.white;

    public void Enter(StatePlayer player)
    {
        Debug.Log("idleEnter");
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
        if (player.isMove)
        {
            player.ChangeState(new MoveState());

        }


    }


    public void Exit(StatePlayer player)
    {
       
    }
}
