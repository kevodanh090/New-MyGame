
using Cainos;
using Cainos.CustomizablePixelCharacter;
using System;
using UnityEngine;
using UnityEngine.UI;






public class PlayerMove : MonoBehaviour
{
    //[SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Button jumpButton;
    [SerializeField] private float MoveSpeed = 2.5f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private MovementType defaultMovement = MovementType.Walk;
    [ExposeProperty]                                        // is the character dead? if dead, plays dead animation and disable control
    public bool IsDead
    {
        get { return isDead; }
        set
        {
            isDead = value;
            fx.IsDead = isDead;
            fx.DropWeapon();
        }
    }
    private bool isDead;


    private float _horizontal;
    //private float _vertical;
    private bool _isOnGround;
    private bool _isJumping;
    private bool _isDash;
    private int _jumpCount;
    private int _moveDownCount;
    private PixelCharacter fx;                              // the FXCharacter script attached the character
    private CapsuleCollider2D collider2d;                   // Collider compoent on the character
    private Rigidbody2D rb2d;                              // Rigidbody2D component on the character
    private Vector2 curVel;
    private Vector2 posBot;
    private Vector2 posTop;

    private const int JumpMax = 2;

    public float walkSpeedMax = 2.5f;                       // max walk speed, ideally should be half of runSpeedMax
    public float walkAcc = 10.0f;                           // walking acceleration

    public float runSpeedMax = 5.0f;                        // max run speed
    public float runAcc = 10.0f;                            // running acceleration

    public float groundBrakeAcc = 6.0f;                     // braking acceleration (from movement to still) while on ground
    public float airBrakeAcc = 1.0f;                        // braking acceleration (from movement to still) while in air

    public float airSpeedMax = 2.0f;                        // max move speed while in air
    public float airAcc = 8.0f;                             // air acceleration

    public float jumpSpeed = 5.0f;                          // speed applied to character when jump
    public float jumpCooldown = 0.55f;                      // time to be able to jump again after landing
    public float jumpGravityMutiplier = 0.6f;               // gravity multiplier when character is jumping, should be within [0.0,1.0], set it to lower value so that the longer you press the jump button, the higher the character can jump    
    public float fallGravityMutiplier = 1.3f;               // gravity multiplier when character is falling, should be equal or greater than 1.0
    public float groundCheckRadius = 0.17f;                 // radius of the circle at the character's bottom to determine whether the character is on ground
    public event Action PlayerMoveDownPlatform;
    private float jumpTimer;

    private void Awake()
    {
        fx = GetComponent<PixelCharacter>();
        collider2d = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        Init();   
    }

    private void Update()
    {
        if (joyStick.Horizontal >= .2f)
        {
            _horizontal = 1.0f;
        }else 
        if (joyStick.Horizontal <= -.2f)
        {
            _horizontal = -1.0f;
        }
        else
        {
            _horizontal = 0.0f;
        }
      
        //jumpButton.onClick.AddListener(() => Jump(ref _isOnGround, ref _isJumping));


        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Dash();
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    _moveDownCount++;
        //    if (_moveDownCount >= 1)
        //    {
        //        MoveDownPlatform();
        //        _moveDownCount = default;
        //    }
        //}
        bool inputRun = false;
       // bool inputCrounch = false;
        bool inputMoveModifier = false;
        if (defaultMovement == MovementType.Walk && inputMoveModifier) inputRun = true;
        if (defaultMovement == MovementType.Run && !inputMoveModifier) inputRun = true;
        //CHECK IF THE CHARACTER IS ON GROUND
        _isOnGround = false;
        Vector2 worldPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos + posBot, groundCheckRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].isTrigger) continue;
            if (colliders[i].gameObject != gameObject) _isOnGround = true;
        }
    

        Move(inputRun);
       
    }

    private void Init()
    {
        Application.targetFrameRate = 60;

        _horizontal = 0.0f;
        _isOnGround = default;
        _isJumping = default;
        _isDash = default;
        _jumpCount = default;
        _moveDownCount = default;
    }

    //private void CheckAnimation(float horizontal, float yVelocity, bool isGrounded)
    //{
    //    animationController.CheckAnimation(horizontal, yVelocity, isGrounded);
    //}

    private void Start()
    {
        posBot = collider2d.offset - new Vector2(0.0f, collider2d.size.y * 0.5f);
        posTop = collider2d.offset + new Vector2(0.0f, collider2d.size.y * 0.5f);
    }

    //private void Dash()
    //{
    //    _isDash = true;
    //    rb2d.AddForce(new Vector2(1f, 0f) * JumpForce, ForceMode2D.Impulse);
    //    //_isDash = false;
    //}

    //private void Jump(ref bool isOnGround, ref bool isJumping)
    //{
    //    if (!isOnGround || isJumping)
    //    {
    //        if (_jumpCount >= JumpMax)
    //        {
    //            return;
    //        }
    //    }

    //    isOnGround = false;
    //    isJumping = true;
    //    _jumpCount++;
    //    //rb2d.AddForce(new Vector2(0, 1f) * JumpForce, ForceMode2D.Impulse);
    //}

    private void Move(bool inputRunning)
    {
        if (isDead) return;

        //GET CURRENT SPEED FROM RIGIDBODY

        curVel = rb2d.velocity;
        float acc = 0.0f;
        float max = 0.0f;
        float brakeAcc = 0.0f;
           
        if (_isOnGround)
        {
          acc = inputRunning ? runAcc : walkAcc;
          max = inputRunning ? runSpeedMax : MoveSpeed;
          brakeAcc = groundBrakeAcc;

        }
        else
        {
                acc = airAcc;
                max = airSpeedMax;
                brakeAcc = airBrakeAcc;
        }
        if (Mathf.Abs(_horizontal) > 0.01f)
        {
            //if current horizontal speed is out of allowed range, let it fall to the allowed range
             bool shouldMove = true;
             if (_horizontal > 0 && curVel.x >= max)
             {
                curVel.x =Mathf.MoveTowards(curVel.x, max, brakeAcc * Time.deltaTime);
                //curVel.x = curVel.x * MoveSpeed * Time.deltaTime;
                shouldMove = false;
             }
             if (_horizontal < 0 && curVel.x <= -max)
             {
                curVel.x = Mathf.MoveTowards(curVel.x, -max, brakeAcc * Time.deltaTime);
                //curVel.x  = -curVel.x * MoveSpeed * Time.deltaTime;
                shouldMove = false;
             }
             //otherwise, add movement acceleration to cureent velocity
              if (shouldMove) curVel.x += acc * Time.deltaTime * _horizontal;
              
        }
          //no horizontal movement input, brake to speed zero
        else
        {
            
            curVel.x = 0.0f;// Mathf.MoveTowards(curVel.x, 0.0f, brakeAcc * Time.deltaTime);
        }
      
        if (curVel.y > 0)
        {
            curVel.y += Physics.gravity.y * (fallGravityMutiplier - 1.0f) * Time.deltaTime;
        }
         rb2d.velocity = curVel;
         float movingBlend = Mathf.Abs(curVel.x) / MoveSpeed;    
         fx.MovingBlend = Mathf.Abs(curVel.x) / MoveSpeed;
         fx.SpeedVertical = curVel.y;
         fx.Facing = Mathf.RoundToInt(_horizontal);
         fx.IsGrounded = _isOnGround;
    }


    private void FixedUpdate()
    {
      
    }
    public enum MovementType
    {
        Walk,
        Run
    }

    private void OnDrawGizmosSelected()
    {
        //Draw the ground detection circle
        Gizmos.color = Color.white;
        Vector2 worldPos = transform.position;
        Gizmos.DrawWireSphere(worldPos + posBot, groundCheckRadius);

    }
   

}
