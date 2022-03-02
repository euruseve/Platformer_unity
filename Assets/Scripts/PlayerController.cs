using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;

    private float _horizontal = 0f;
    private const float speedXMultiplier =  50f;

    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFacingRight = true;
    private bool _isFinish = false;
    private bool _isLeverArm = false;

    private FixedJoystick _fixedJoystick;
    private Finish _finish;
    private Rigidbody2D _rb;
    private LeverArm _leverArm;


    void Start() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _fixedJoystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<FixedJoystick>();
        _leverArm = FindObjectOfType<LeverArm>();
    }

    void Update() 
    {

        // _horizontal = Input.GetAxis("Horizontal");
        _horizontal = _fixedJoystick.Horizontal;
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        if(Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    void FixedUpdate() 
    {
        _rb.velocity = new Vector2(_horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if(_isJump)
        {
            _rb.AddForce(new Vector2(0f, 500f));
            _isGround = false;
            _isJump = false;
        }

        if(_horizontal > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0f && _isFacingRight)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if(other.CompareTag("Finish"))
        {
            _isFinish = true;
            Debug.Log("1");
        }

        if(leverArmTemp != null)
        {
            _isLeverArm = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if(other.CompareTag("Finish"))
        {
            _isFinish = false;
            Debug.Log("2");
        }
        if(leverArmTemp != null)
        {
            _isLeverArm = false;
        }

    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    public void Jump()
    {
        if(_isGround)
        {
            _isJump = true;
            jumpSound.Play();
        }
    }

    public void Interact()
    {
        if(_isFinish)
        {
            _finish.FinishLevel();
        }
        if(_isLeverArm)
        {
            _leverArm.ActiveLeverArm();
        }
    }

}
