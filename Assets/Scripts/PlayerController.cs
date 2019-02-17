using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;

    [SerializeField]
    private Transform attackPosition;

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private float _jumpHeight = 300.0f;

    [SerializeField]
    private float _dazedTime = 0.3f;

    private float _currentDazedTime;

    private float _currentSpeed;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private bool _alive = true;
    private bool _isGrounded = false;

    public static PlayerController Player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
            Player = this;

        _currentSpeed = _speed;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_alive)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var right = moveHorizontal > 0;
            var left = moveHorizontal < 0;
            var up = moveVertical > 0;
            var down = moveVertical < 0;

            var isMoving = right | left | up | down;

            if (_currentDazedTime <= 0)
            {
                _animator.SetBool("isMoving", isMoving);
                _currentSpeed = _speed;
            }
            else
            {
                _currentDazedTime -= Time.deltaTime;
            }

            _animator.SetFloat("dazed", _currentDazedTime);

            if (right)
            {
                _spriteRenderer.flipX = false;
                transform.position += Vector3.right * _currentSpeed * Time.deltaTime;
                attackPosition.position = new Vector3(transform.position.x + 1, transform.position.y);
            }

            if (left)
            {
                _spriteRenderer.flipX = true;
                transform.position += Vector3.left * _currentSpeed * Time.deltaTime;
                attackPosition.position = new Vector3(transform.position.x - 1, transform.position.y);
            }

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpHeight);
                _isGrounded = false;
                _animator.SetBool("isGrounded", _isGrounded);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Ground")
        {
            if(!_isGrounded)
            {
                _isGrounded = true;
                _animator.SetBool("isGrounded", _isGrounded);
            }
        }
        else
        {
            if(col.gameObject.tag == "Enemy")
            {
                _health--;

                if (_health <= 0)
                    Die();
                else
                {
                    _currentSpeed = -2.5f;
                    _currentDazedTime = _dazedTime;
                    _animator.SetFloat("dazed", _currentDazedTime);
                }
            }
        }
    }

    void Die()
    {
        _alive = false;
        _animator.SetBool("alive", _alive);
    }
}
