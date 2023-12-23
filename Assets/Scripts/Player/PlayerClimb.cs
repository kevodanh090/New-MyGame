using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private float _vertical;
    private float _speed = 8f;
    private bool _isLadder;
    private bool _isClimbing;

    [SerializeField] private Joystick joyStick;
    [SerializeField] private Rigidbody2D rb2;
    [SerializeField] private float fallGravityMutiplier = 1.3f;
    [SerializeField] private float groundCheckRadius = 0.17f;
    
    // Update is called once per frame
    void Update()
    {
        if (joyStick.Vertical >= .2f)
        {
            _vertical = 1.0f;
        }
        if (joyStick.Vertical <= -.2f)
        {
            _vertical = -1.0f;
        }
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
    }
    private void FixedUpdate()
    {

       Climb(ref _isClimbing);
    }
    private void Climb(ref bool _isClimbing)
    {
        if(_isClimbing)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = true;
            //_isClimbing = false;
            _vertical = 0.09f;
            
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
            rb2.gravityScale = 1.3f;
            _vertical = 0.0f;
        }
    }
  
}
