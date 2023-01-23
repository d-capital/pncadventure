using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public int health = 100;
    public int stamina = 50;
    
    public Weapon weapon;
    public float fireRate;
    private float nextFire;
    public float coolDownRate;

    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private bool isDashButtonDown;
    private float slideSpeed;

    public Vector3 mousePos;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    public Vector3 slideDirection;

    public HealthBarControll HealthBar;

    private State state;
    private enum State
    {
        Normal,
        DodgeRollSliding
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Normal:
                HandleMovement();
                HandelDodgeRoll();
                HandleFire();
                break;
            case State.DodgeRollSliding:
                HandleDodgeRollSliding();
                break;
        }
        //HandleMovement();
        
    }

    private void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        Vector3 aimDirection = mousePosWorld - transform.position;
        float aimAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngel;
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

    }


    private void HandelDodgeRoll()
    {
        if (Input.GetMouseButtonDown(1))
        {
            state = State.DodgeRollSliding;
            mousePos = Input.mousePosition;
            mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
            slideDirection = (mousePosWorld2D - new Vector2(transform.position.x, transform.position.y)).normalized;
            slideSpeed = 40f;
        }
    }
    private void HandleDodgeRollSliding()
    {
        transform.position += slideDirection * slideSpeed * Time.fixedDeltaTime;

        slideSpeed -= slideSpeed * 5f * Time.fixedDeltaTime;
        if  (slideSpeed < 1f)
        {
            state = State.Normal;
        }
    }

    private void HandleFire()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponentInChildren<Weapon>() != null)
        {
                Fire();
        }
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadDistinctLevel(PlayerData playerData, int lvlIndex)
    {
        SceneManager.LoadScene(lvlIndex);
        SaveSystem.SavePlayer(playerData);
    }

    void Fire()
    {
        weapon.Fire(stamina);
    }
}
