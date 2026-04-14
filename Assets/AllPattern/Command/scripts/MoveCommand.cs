using UnityEngine;

public class MoveCommand : ICommand
{
    private PlayerMover _playerMover;
    private Vector3 _moveVector3;

    public MoveCommand(PlayerMover playerMover, Vector3 moveVector3)
    {
        _playerMover = playerMover;
       _moveVector3 = moveVector3;
    }

    public void Execute()//ICommand Execute진짜 동작
    {
        _playerMover.PlayerPath.AddToPath(_playerMover.transform.position + _moveVector3);
        _playerMover.Move(_moveVector3);
    }

    public void Undo()
    {
        _playerMover.Move(-_moveVector3);
        _playerMover.PlayerPath.ReMoveFromPath();
    }
}
