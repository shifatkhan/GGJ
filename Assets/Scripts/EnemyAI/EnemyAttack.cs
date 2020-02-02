using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyGround enemyGround;
    EnemyFlying enemyFlying; 

    void Start()
    {
        enemyGround = transform.parent.GetComponent<EnemyGround>();
        enemyFlying = transform.parent.GetComponent<EnemyFlying>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if(enemyGround != null)
                enemyGround.isAttacking = true;

            if (enemyFlying != null)
                enemyFlying.isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (enemyGround != null)
                enemyGround.isAttacking = false;

            if (enemyFlying != null)
                enemyFlying.isAttacking = false;
        }
    }
}
