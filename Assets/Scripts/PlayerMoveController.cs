using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    CharacterController playerController;
    AudioSource audioSource;
    Vector3 direction;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float runMultiplier = 2f;
    public float jumpPower = 5f;
    public float gravity = 9.81f;

    [Header("Audio Settings")]
    public AudioClip walkSound;
    public AudioClip runSound;
    public float walkStepInterval = 0.5f;
    public float runStepInterval = 0.3f;
    private float stepTimer;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 5f;
    public float minMouseY = -45f;
    public float maxmouseY = 45f;

    private float RotationY = 0f;
    private float RotationX = 0f;
    private bool isRunning;

    public Transform affectCamera;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(_horizontal) > 0.1f || Mathf.Abs(_vertical) > 0.1f;

        // 跑步状态检测
        isRunning = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isMoving;

        float currentSpeed = isRunning ? speed * runMultiplier : speed;

        if (playerController.isGrounded)
        {
            direction = new Vector3(_horizontal, 0, _vertical);
            if (Input.GetKeyDown(KeyCode.Space))
                direction.y = jumpPower;
        }

        direction.y -= gravity * Time.deltaTime;
        playerController.Move(transform.TransformDirection(direction) * currentSpeed * Time.deltaTime);

        // 脚步声处理
        if (playerController.isGrounded && isMoving)
        {
            stepTimer += Time.deltaTime;
            float interval = isRunning ? runStepInterval : walkStepInterval;

            if (stepTimer >= interval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void HandleMouseLook()
    {
        RotationX += affectCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        RotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        RotationY = Mathf.Clamp(RotationY, minMouseY, maxmouseY);

        transform.eulerAngles = new Vector3(0, RotationX, 0);
        affectCamera.transform.eulerAngles = new Vector3(RotationY, RotationX, 0);
    }

    void PlayFootstepSound()
    {
        AudioClip clip = isRunning ? runSound : walkSound;
        if (clip != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f); // 添加随机音调变化
            audioSource.PlayOneShot(clip);
        }
    }
}