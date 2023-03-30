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
    public float coolDownRate;

    private Vector2 moveDirection;
    private bool isDashButtonDown;

    public Vector3 mousePos;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    public Vector3 slideDirection;

    public HealthBarControll HealthBar;

    public bool isCabCrewDead = false;

    public bool isQteActive = false;

    public Animator animator;

    public Texture2D cursorTextureNoWeapon;
    public Texture2D cursorTextureWithWeapon;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.SetMaxHealth(health);
        Cursor.SetCursor(cursorTextureNoWeapon, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        HandleFire();
        HandleDash();
        rb.angularVelocity = 0;

    }

    private void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        if (!isQteActive)
        {
            Vector3 aimDirection = mousePosWorld - transform.position;
            float aimAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngel;
            if (!isMoving())
            {
                animator.SetBool("isMoving", false);
            }
        }
        if (isDashButtonDown)
        {
            float dashAmount = 4f;
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + moveDirection * dashAmount);
            isDashButtonDown = false;
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        if (isMoving())
        {
            animator.SetBool("isMoving", true);
        }

    }


    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
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
        weapon.PlayerFire(stamina);
    }

    bool isMoving()
    {
        return Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.DownArrow)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.RightArrow);
    }

    public void ChangeToWeaponAim()
    {
        Cursor.SetCursor(cursorTextureWithWeapon, hotSpot, cursorMode);
    }

    public void ShowGameOver()
    {
        
        GameOver[] gameOverScreens = Resources.FindObjectsOfTypeAll<GameOver>();
        foreach (GameOver screen in gameOverScreens)
        {
            screen.GetComponent<GameOver>().ShowGameOverScreen();
        }

    }
}
