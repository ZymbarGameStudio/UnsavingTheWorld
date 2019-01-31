using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform attackPosition;

    private float _speed = 2.0f;
    private float _jumpHeight = 300.0f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private bool _isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var right = moveHorizontal > 0;
        var left = moveHorizontal < 0;
        var up = moveVertical > 0;
        var down = moveVertical < 0;

        var isMoving = right | left | up | down;

        _animator.SetBool("isMoving", isMoving);
        
        if (right)
        {
            _spriteRenderer.flipX = false;
            transform.position += Vector3.right * _speed * Time.deltaTime;
            attackPosition.position = new Vector3(transform.position.x + 1, transform.position.y);
        }

        if (left)
        {
            _spriteRenderer.flipX = true;
            transform.position += Vector3.left * _speed * Time.deltaTime;
            attackPosition.position = new Vector3(transform.position.x - 1, transform.position.y);
        }

        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpHeight);
            _isGrounded = false;
            _animator.SetBool("isGrounded", _isGrounded);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Ground")
        {
            _isGrounded = true;
            _animator.SetBool("isGrounded", _isGrounded);
        }
    }
}
