using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack = 0;
    public float startTimeAttack = 0.01f;

    public LayerMask enemyLayer;
    public Transform attackPosition;
    public float attackRange;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if(Input.GetButton("Attack"))
            {
                _animator.Play("Attack");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);
                // fazer código de damage
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<Enemy>().ReceiveDamange();
                }
            }

            timeBetweenAttack = startTimeAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
