using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LayerMask obstacle;
    private const float boardSpacing = 1f;

    private PlayerPath playerPath;
    public PlayerPath PlayerPath=>playerPath;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPath = GetComponent<PlayerPath>();    
    }

    public void Move(Vector3 movement)
    {
        Vector3 destination=transform.position+movement;
        transform.position = destination;
    }
    public bool IsVaildMove(Vector3 movement)//플레이어 위치에서 특정 방향레이 쏴서 충돌 검사
    {  //시작점, 방향, 거리, 레이어
        return !Physics.Raycast(transform.position, movement, boardSpacing, obstacle);
    }
}
