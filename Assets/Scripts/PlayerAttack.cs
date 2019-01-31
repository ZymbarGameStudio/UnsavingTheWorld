using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack;
    public float startTimeAttack;

    public LayerMask enemyLayer;
    public Transform attackPosition;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenAttack <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);
                // fazer código de damage
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
