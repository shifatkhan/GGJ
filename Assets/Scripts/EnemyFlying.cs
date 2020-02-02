using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFlying : MonoBehaviour
{
    public LayerMask targetCollisionMask;
    public bool isAttacking = false;

    protected Animator animator;
    private AIPath aIPath;

    public Collider2D attackHitbox;

    [SerializeField] private int attackDamage = 20;

    // Called before Start
    private void Awake()
    {
        aIPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            aIPath.canMove = true;
            if (aIPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(transform.localScale.x > 0 ? transform.localScale.x * -1f : transform.localScale.x,
                    transform.localScale.y, transform.localScale.z);

            }
            else if (aIPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(transform.localScale.x < 0 ? transform.localScale.x * -1f : transform.localScale.x,
                   transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            // Enemy is attacking, so make him stand still.
            aIPath.canMove = false;
        }

        if (isAttacking)
            animator.SetBool("isAttacking", true);
        else
            animator.SetBool("isAttacking", false);

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
            if (c != null && c.transform != null & c.gameObject != null && c.transform.CompareTag("Player"))
            {
                Debug.Log(c.transform.tag);
                c.gameObject.GetComponent<Player>().ReceiveDamage(attackDamage);
            }
        }
    }
}
