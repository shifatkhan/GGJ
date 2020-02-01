using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("ATACK");
        if (collider.gameObject.CompareTag("Player"))
        {
            EnemyGround.isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            EnemyGround.isAttacking = false;
        }
    }
}
