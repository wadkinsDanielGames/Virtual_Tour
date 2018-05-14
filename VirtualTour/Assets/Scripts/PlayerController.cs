using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState { IDLE, WALKING, SNEAK, RUNNING, HIDE, CARRYING, WALKING_TO_SNEAK, SNEAK_TO_WALKING };
public enum PlayerVisibility { NOTVISIBLE, VISIBLE };
public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float walkSpeed;
    public float sneakSpeed;
    public float runSpeed;

    public CharacterController controller;
    public Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    private GameState gameState = GameState.PLAYING;

    public PlayerState _playerState;

    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;
    public bool isInteracting = false;

    public bool inMenu;

    public GameObject dialogue;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _playerState = PlayerState.IDLE;
    }


    void OnEnable()
    {
        GameController.changeGameState += updateGameState;
        anim.SetBool("Walk", true);
        _playerState = PlayerState.IDLE;
        DialogueSystem.inMenu += changeMenu;
    }

    void OnDisable()
    {
        GameController.changeGameState -= updateGameState;
        DialogueSystem.inMenu -= changeMenu;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.PAUSED || gameState == GameState.WIN || isInteracting || inMenu)
        {
            return;
        }
        ChangeState();
    }

    void updateGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    void playerHide()
    {
        _playerState = PlayerState.HIDE;
    }

    void ChangeState()
    {
        switch (_playerState)
        {
            case PlayerState.IDLE:
                Movement();
                if (Input.GetAxis("Run") > 0) { _playerState = PlayerState.RUNNING; }
                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) { _playerState = PlayerState.WALKING; }
                anim.SetBool("Walk", true);
                break;

            case PlayerState.WALKING:
                Movement();
                if (Input.GetAxis("Run") > 0) { _playerState = PlayerState.RUNNING; }
                if (getMoveDir() == 0) { _playerState = PlayerState.IDLE; }
                moveSpeed = walkSpeed;
                if (getMoveDir() == 0) { _playerState = PlayerState.IDLE; }
                break;

            case PlayerState.RUNNING:
                Movement();
                if (Input.GetAxis("Run") == 0) { _playerState = PlayerState.IDLE; }
                moveSpeed = runSpeed;
                if (Input.GetAxis("Run") == 0) { _playerState = PlayerState.IDLE; }
                break;

            default:
                break;
        }
    }

    //Helper function to get the movement direction
    //Ignores Y
    float getMoveDir()
    {
        float temp1 = moveDirection.x;
        float temp2 = moveDirection.z;
        return temp1 + temp2;
    }

    void Movement()
    {
        float y = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = y;
        anim.SetFloat("BlendX", controller.velocity.x);
        anim.SetFloat("BlendY", controller.velocity.z);
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        //moveDirection.y = Mathf.Clamp(moveDirection.y, 0, gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

        //move player in different directions based on camera direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }
    }

    public void changeMenu(bool status) {

        inMenu = status;
        dialogue.SetActive(status);

    }


}
