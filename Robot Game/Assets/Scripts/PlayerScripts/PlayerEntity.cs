using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    private int movementInputDirection;
    private bool grounded, canJump;
    private float groundedRadius = .1f;
    protected Rigidbody2D rigBod;
    protected Animator animator;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float movementSpeed, jumpForce;
    [SerializeField] public LayerMask whatIsGround;


    [SerializeField] public bool activePlayer = false;

    [SerializeField] public InventoryDisplay inventoryDisplay;

    public Inventory inventory;

    [Header("Player Specific Stats")]
    [SerializeField] public int stackSizeLimit = 5;
    [SerializeField] public int inventorySize = 9;
    [SerializeField] public Weapon weaponSlot;

    public override void Start()
    {
        base.Start();
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = new Inventory(inventorySize);
        inventory.player = this;
        if (activePlayer)
        {
            inventoryDisplay.SetPlayer(gameObject);
            inventoryDisplay.gameObject.SetActive(false);
        }
    }

    public override void Update()
    {
        base.Update();
        if (activePlayer)
        {
            PlayerInput();
        }
    }

    void FixedUpdate()
    {
        if (activePlayer)
        {
            Movement();
        }
    }

    public void TakeControl()
    {
        activePlayer = true;
        inventoryDisplay.SetPlayer(gameObject);
    }

    private void PlayerInput()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        movementInputDirection = (int)Input.GetAxisRaw("Horizontal");

        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
        }

        animator.SetInteger("Run", movementInputDirection);
        transform.GetChild(0).GetComponent<Animator>().SetInteger("Run", movementInputDirection);

        if (Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            rigBod.velocity = new Vector2(rigBod.velocity.x, jumpForce);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {           
                if (hit.collider.gameObject.GetComponent<PlayerEntity>() && hit.collider.gameObject != gameObject)
                {
                    activePlayer = false;
                    hit.collider.gameObject.GetComponent<PlayerEntity>().TakeControl();
                    Camera.main.GetComponent<CameraFollow>().followTransform = hit.collider.gameObject.transform;
                }
                if (hit.collider.gameObject.GetComponent<ItemObject>())
                {
                    if (inventory.Add(hit.collider.gameObject.GetComponent<ItemObject>().item))
                    {
                        inventoryDisplay.RefreshInventory();
                        Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        Debug.Log("full inventory");
                    }
                }
            }
        }

    }

    private void Movement()
    {
        if ((facingDirection > 0 && movementInputDirection < 0) || (facingDirection < 0 && movementInputDirection > 0))
        {
            Flip();
        }

        rigBod.velocity = new Vector2(movementSpeed * movementInputDirection, rigBod.velocity.y);
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
