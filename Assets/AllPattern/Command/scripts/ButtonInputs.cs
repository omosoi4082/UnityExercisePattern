using UnityEngine;
using UnityEngine.UI;

public class ButtonInputs : MonoBehaviour
{
    [Header("key controls")]
    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode undoKey = KeyCode.U;
    [SerializeField] private KeyCode redoKey = KeyCode.R;

    [Header("button controls")]
    [SerializeField] private Button forwardBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button undoBtn;
    [SerializeField] private Button redoBtn;

    [SerializeField] private PlayerMover playerMover;

    void Start()
    {
        forwardBtn.onClick.AddListener(OnForwardInput);
        backBtn.onClick.AddListener(OnBackInput);
        leftBtn.onClick.AddListener(OnRightInput);
        rightBtn.onClick.AddListener(OnLeftInput);
        undoBtn.onClick.AddListener(OnUndoInput);
        redoBtn.onClick.AddListener(OnRedoInput);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(forwardKey))
        {
            forwardBtn.onClick.Invoke();//코드로 강제로 클릭시키는 것 등록된 함수들을 전부 실행
        }
        if (Input.GetKeyDown(backKey))
        {
            backBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(leftKey))
        {
            leftBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(rightKey))
        {
            rightBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(undoKey))
        {
            undoBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(redoKey))
        {
            redoBtn.onClick.Invoke();
        }
    }

    private void RunPlayerCommand(PlayerMover player, Vector3 movemant)
    {
        if (player == null) { return; }
        if (player.IsVaildMove(movemant))
        {
            ICommand command = new MoveCommand(player, movemant);
            CommandInvoker.ExecuteCommand(command);
        }
    }

    private void OnLeftInput()
    {
        RunPlayerCommand(playerMover, Vector3.left);
    }
    private void OnRightInput()
    {
        RunPlayerCommand(playerMover, Vector3.right);
    }
    private void OnForwardInput()
    {
        RunPlayerCommand(playerMover, Vector3.forward);
    }
    private void OnBackInput()
    {
        RunPlayerCommand(playerMover, Vector3.back);
    }

    private void OnUndoInput()
    {
        CommandInvoker.Undocommand();
    }
    private void OnRedoInput()
    {
        CommandInvoker.Redocommand();
    }
}

