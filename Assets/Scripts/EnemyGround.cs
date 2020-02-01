using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Player
{
    public Transform target;
    public static bool isAttacking = false;

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            // TODO: add 0.1 error checking to remove glitching
            if (target.position.x < transform.position.x)
            {
                moveDirection = -1.0f;
            }
            else if (target.position.x > transform.position.x)
            {
                moveDirection = 1.0f;
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
}
