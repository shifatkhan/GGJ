using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Player
{
    public Transform target;

    void Update()
    {
        // TODO: add 0.1 error checking to remove glitching
        if(target.position.x < transform.position.x)
        {
            moveDirection = -1.0f;
        }
        else
        {
            moveDirection = 1.0f;
        }

        animator.SetFloat("runningSpeed", Mathf.Abs(moveDirection));
    }
}
