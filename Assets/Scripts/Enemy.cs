using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;

    [SerializeField]
    private float _dazedTime = 0.3f;

    [SerializeField]
    private float _speed = 1f;

    private float _currentSpeed;

    private float _currentDazedTime = 0;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private Transform _playerPosition;

    void Start()
    {
        _currentDazedTime = _speed;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerPosition = PlayerController.Player.transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();

        if (_currentDazedTime > 0)
            _currentDazedTime -= Time.deltaTime;
        else
        {
            _currentSpeed = _speed;
            _animator.SetFloat("dazed", _currentDazedTime);
        }
    }

    void Move()
    {
        var direction = transform.position.x - _playerPosition.transform.position.x;

        if (direction < 0)
        {
            transform.position += Vector3.right * _currentSpeed * Time.deltaTime;
            _spriteRenderer.flipX = true;
        }
        else
        {
            if (direction > 0)
            {
                transform.position += Vector3.left * _currentSpeed * Time.deltaTime;
                _spriteRenderer.flipX = false;
            }
        }
    }

    public void ReceiveDamange()
    {
        _health -= 1;
        _currentDazedTime = _dazedTime;
        _currentSpeed = -1.5f;
        _animator.SetFloat("dazed", _currentDazedTime);

        if (_health <= 0)
            Destroy(gameObject);
    }
}
