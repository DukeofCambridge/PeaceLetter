using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public float speed;
    public string startingAnimation;

    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    private Vector2 moveInput;

    private bool isMoving;
    public bool inputDisable;  // 过场动画时禁止移动
    private float inputX;
    private float inputY;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        inputDisable = false;
        //animator.Play(startingAnimation);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!inputDisable)
            PlayerInput();
        else
            isMoving = false;
        SetAnimation();
    }
    private void FixedUpdate()
    {
        if (!inputDisable)
            Movement();
    }

    private void PlayerInput()
    {
        // if (inputY == 0)
        inputX = Input.GetAxisRaw("Horizontal");
        // if (inputX == 0)
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 && inputY != 0)
        {
            inputX = inputX * 0.7f;
            inputY = inputY * 0.7f;
        }

        moveInput = new Vector2(inputX, inputY);

        isMoving = moveInput != Vector2.zero;

    }
    // 乘 Time.deltaTime(每帧运行的时间) 是为了修正不同设备不同帧数下的运行，该方法在FixedUpdate中调用
    private void Movement()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + moveInput * speed * Time.deltaTime);
    }


    private void SetAnimation()
    {
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("InputX", inputX);
            animator.SetFloat("InputY", inputY);
        }

    }

}
