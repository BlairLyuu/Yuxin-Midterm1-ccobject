using UnityEditor.XR;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MinePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;    



    //cam
    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float maxLookX = 60f;
    public float minLookX = -60f;

    public float bobSpeed = 6f;
    public float bobAmount = 0.05f;
    private bool mouseLocked = true;




    //jump
    [Header("Jump Settings")]
    private float coyoteTime = 1f;
    private float coyoteCounter;
    
    public Camera playerCam;

    public float zoomFOV = 30f;
    public float normalFOV = 80f;
    public float zoomSpeed = 10f;


    
    private bool isZoomed = false;

    private float finalMoveSpeed;


    private CharacterController controller;
    private Vector3 velocity;
    private float rotX;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private Vector3 basePos;

    //bob
    private Vector3 currentOffset = Vector3.zero;
    private Vector3 lastPos;
    private Vector2 lookInput;
    private bool canMove = true;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        basePos = cameraTransform.localPosition;

        lookInput = Vector2.zero;

    }

    void Update()
    {
        
        Move();
        if (mouseLocked)
            Look();

            
        
        ZoomIn();
        Jump();

    }

    public void BanPlayerMoving(bool _canMove)
    {
        canMove = _canMove;
        mouseLocked = _canMove;
    }
    void Move()
    {
        Vector2 moveInput;
        if (canMove)
            moveInput = moveAction.ReadValue<Vector2>();
        else
                    moveInput = Vector2.zero;


        if (Keyboard.current.shiftKey.isPressed)
        {
            finalMoveSpeed = 2 * moveSpeed;
        }
        else
        {
            finalMoveSpeed = moveSpeed;
        }


        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * finalMoveSpeed * Time.deltaTime);


        controller.Move(velocity * Time.deltaTime);


        //Camera bob starts HEREEEEEE
        cameraTransform.localPosition = basePos + CameraBob();

    }

    public Vector3 CameraBob()
    {
        Vector3 targetOffset;
        float returnSpeed = 5f;

        float actualSpeed;

        Vector3 displacement = transform.position - lastPos;
        displacement.y = 0; //JUMP DOESNT COUNT
        actualSpeed = displacement.magnitude / Time.deltaTime;
        lastPos = transform.position;



        actualSpeed = Mathf.Clamp(actualSpeed, 0f, 5f);
        float speedBobStrength = actualSpeed * 3;
        bool isWalking = actualSpeed > 2;
        //Debug.Log(actualSpeed);

        float frequency = bobSpeed * (1f + speedBobStrength * 0.2f);
        if (!isWalking)
        {
            targetOffset = Vector3.zero;
        }
        else
        {
            float x = Mathf.Sin(Time.time * frequency) * bobAmount * speedBobStrength;
            float y = Mathf.Cos(Time.time * frequency * 2) * bobAmount * speedBobStrength;
            targetOffset = new Vector3(x, y, 0);
        }

        currentOffset = Vector3.Lerp(currentOffset, targetOffset, Time.deltaTime * returnSpeed);

        return currentOffset;
    }


    void Look()
    {
        lookInput = lookAction.ReadValue<Vector2>();


        float mouseX = lookInput.x * lookSensitivity ;
        float mouseY = lookInput.y * lookSensitivity ;

        transform.Rotate(Vector3.up * mouseX);

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
    //    Debug.Log("rotX:" + rotX);
        cameraTransform.localRotation = Quaternion.Euler(rotX, 0, 0);
    }

    public void SetMouseLock(bool isLocked)
    {
        mouseLocked = isLocked;
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }



    public void Jump()
    {
        if (controller.isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;
       // Debug.Log("Jumpeddfnaksblkabdj");



        if (Keyboard.current.spaceKey.isPressed)
        {
            if (controller.isGrounded && coyoteCounter > 0)
            {
                velocity.y = jumpForce;
            }
            else
            {
                Debug.Log("CannotJump");
            }
        }
        else
        {

        }
    }
    private void ZoomIn()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            isZoomed = !isZoomed;
        }

        float targetFOV = isZoomed ? zoomFOV : normalFOV;
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

}