using UnityEngine;

public class StatePlayer : MonoBehaviour
{
    private IPlayerState currentState;
    private CharacterController characterController;
    public MeshRenderer material;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    public float gravity = -9.8f;// 중력
    public bool isMove;
    public bool isJump;

    public Vector2 moveVle;

    private float yVel;//y속도
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController =GetComponent<CharacterController>();
        material = GetComponent<MeshRenderer>();  
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        AppltGravity();
        currentState.Execte(this);
    }

    void GetInput()
    {
        float h = Input.GetAxis("Horizontal");//A,D
        float v = Input.GetAxis("Vertical");//W S
       
        moveVle = new Vector2(h, v);    
        isMove=moveVle.magnitude > 0.1;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isJump=true; 
        }
    }

    public void OnMove()
    {
       Vector3 dir=new Vector3(moveVle.x,0,moveVle.y);  
        dir*=speed;
        dir.y=yVel;

        characterController.Move(dir * Time.deltaTime);
    }
    public void OnJump() { 
        if(characterController.isGrounded)
        {
            yVel = Mathf.Sqrt(jumpForce * -2f * gravity);
            isJump = false;
        }
    }
    void AppltGravity()
    {
        if (characterController.isGrounded && yVel < 0)
            yVel = -2f;// 바닥 붙이기
        yVel += gravity * Time.deltaTime;
    }

    public void ChangeState(IPlayerState state)
    {
        currentState?.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }

    public bool IsGrounded ()
    {
        return characterController.isGrounded;  
    }
}
