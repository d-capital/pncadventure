using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public int health = 100;

    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private bool isDashButtonDown;
    private float slideSpeed;

    public Vector3 mousePos;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    public Vector3 slideDirection;

    private State state;
    private enum State
    {
        Normal,
        DodgeRollSliding
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");
        //moveDirection = new Vector2(moveX, moveY).normalized;
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                HandelDodgeRoll();
                break;
            case State.DodgeRollSliding:
                HandleDodgeRollSliding();
                break;
        }
        HandleMovement();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        if (isDashButtonDown)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed*2, moveDirection.y * moveSpeed*2);
            isDashButtonDown = false;
        }
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }
        
        moveDirection = new Vector2(moveX, moveY).normalized;
        
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
          //  HandelDodgeRoll();
            //HandleDodgeRollSliding();
        //}

    }

    
    private void HandelDodgeRoll()
    {
        if (Input.GetMouseButtonDown(1))
        {
            state = State.DodgeRollSliding;
            mousePos = Input.mousePosition;
            mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
            slideDirection = (mousePosWorld - transform.position).normalized;
            slideSpeed = 70f;
        }
    }
    private void HandleDodgeRollSliding()
    {
        transform.position += slideDirection * slideSpeed * Time.deltaTime;

        slideSpeed -= slideSpeed * 5f * Time.deltaTime;
        if  (slideSpeed < 5f)
        {
            state = State.Normal;
        }
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
