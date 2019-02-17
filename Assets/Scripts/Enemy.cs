using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _health = 5;

    [SerializeField]
    private float _dazedTime = 0.3f;

    private float _currentDazedTime = 0;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_currentDazedTime > 0)
            _currentDazedTime -= Time.deltaTime;
        else
            _animator.SetFloat("dazed", _currentDazedTime);
    }

    public void ReceiveDamange()
    {
        _health -= 1;
        _currentDazedTime = _dazedTime;
        _animator.SetFloat("dazed", _currentDazedTime);

        if (_health <= 0)
            Destroy(gameObject);
    }
}
