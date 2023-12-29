using Cainos.CustomizablePixelCharacter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Rigidbody2D rb2;
    [SerializeField] private float fallGravityMutiplier = 1.3f;
    [SerializeField] private float groundCheckRadius = 0.17f;
    [SerializeField] private Transform playerModel;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float jumpSpeed = 9.0f;

    private float _vertical;
    private float _horizontal;
    private float _speed = 6.0f;
    private bool _isLadder;
    private bool _isClimbing;
    private bool _isOnGround;
    private bool _isJumping;
    private int _jumpCount;
    
    private const int JumpMax = 2;
    private PixelCharacter fx;

 
    // Update is called once per frame
    void Update()
    {
        if (joyStick.Vertical >= .2f)
        {
            _vertical = 1.0f;
        }
        else
        if (joyStick.Vertical <= -.2f)
        {
            _vertical = -1.0f;
        }
        else { _vertical = 0.0f;}

        if (_isLadder && Mathf.Abs(_vertical) > 0.01f)
        {
            _isClimbing = true;
        }
     

    }
   
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _isClimbing = default;
        _isClimbing = default;
        _isOnGround = default;
}
    private void FixedUpdate()
    {
        
       Climb(ref _isClimbing);
    }
    private void Climb(ref bool _isClimbing)
    {
        if (GameController.isDead) return;
        if (_isClimbing)
        {
           
            //curVel = rb2.velocity;
            Debug.Log($" _vertical= {_vertical}");
            rb2.gravityScale = 0.0f;
            if (_vertical > 0)
            {

                
                rb2.velocity = new Vector2(rb2.velocity.x, _vertical * _speed);
                
                
            }
            if (_vertical < 0)
            {
                rb2.velocity = new Vector2(rb2.velocity.x, _vertical * _speed);
                
            }
            _isClimbing = false;
        }
        
        //else
        //{
        //    curVel.y += Physics.gravity.y * (fallGravityMutiplier - 1.0f) * Time.deltaTime;
        //}
    }
    public void Jump()
    {
        if (GameController.isDead) return;
        if (!_isOnGround || _isJumping)
        {
            if (_jumpCount >= JumpMax)
            {
                return;
            }
            return;
        }

        _isOnGround = false;
        _isJumping = true;
        _jumpCount++;
        rb2.velocity = new Vector2(rb2.velocity.x, 8.0f /* _speed*/);
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isOnGround = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Ground"))
        {
            return;
        }

        _isJumping = false;
        _jumpCount = default;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            //if(Mathf.Abs(_vertical) > 0.01f)
            //{
            //    _isLadder = true;
            //}
            //else 
            //{ _isLadder = false; }

            _isLadder = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
            rb2.gravityScale = 1.3f;
            //_vertical = 0.0f;
        }
    }
  
}
