using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Player
{
    public Transform target;
    public LayerMask targetCollisionMask;
    public static bool isAttacking = false;

    public Collider2D attackHitbox;

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (target.position.x - transform.position.x < -0.8)
            {
                // Moving left
                moveDirection = -1.0f;
            }
            else if (target.position.x - transform.position.x > 0.8)
            {
                // Moving right
                moveDirection = 1.0f;
            }
            else
            {
                // Enemy is at the same position as the target.
                moveDirection = 0f;
            }
        }
        else
        {
            // Enemy is attacking, so make him stand still.
            moveDirection = 0f;
        }

        animator.SetFloat("runningSpeed", Mathf.Abs(moveDirection));

        if (isAttacking)
            animator.SetBool("isAttacking", true);
        else
            animator.SetBool("isAttacking", false);

    }

    // FixedUpdate is called at a fixed interval, all physics code should be in here only
    void FixedUpdate()
    {
        // TODO: make enemy move when player comes withing certain range
        if(moveDirection < 0f && raycastController.collision.left)
        {
            OnJumpDown();
        }
        else if (moveDirection > 0f && raycastController.collision.right)
        {
            OnJumpDown();
        }

        calcBodyVelocity();
        Move(bodyVelocity * Time.deltaTime);

        //Checks current state of game obj and makes adjustment to velocity if necessary
        CheckState();
    }

    public void Attack()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(targetCollisionMask);
        filter.useLayerMask = true;

        Collider2D[] hit = new Collider2D[16];
        Physics2D.OverlapCollider(attackHitbox, filter, hit);
        
        // Check all collisions with attack. TODO: Check if it has same tag as Target
        foreach (Collider2D c in hit)
        {
            if(c != null && c.transform != null & c.gameObject != null)
                Debug.Log(c.transform.tag);
        }
    }
}
