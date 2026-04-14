using UnityEngine;

public class JumpState : IPlayerState
{
    public Color meshColor { get; set; } = Color.red;

    public void Enter(StatePlayer player)
    {
        Debug.Log("jump");
        player.OnJump();
    }

    public void Execte(StatePlayer player)
    {
        player.OnMove();//중력 적용
        player.material.material.color = meshColor;
        if (player.IsGrounded())
        {
            if (player.isMove)
            {
                player.ChangeState(new MoveState());
            }
            else
            {
                player.ChangeState(new IdleState());
            }
        }
    }


    public void Exit(StatePlayer player)
    {

    }
}
