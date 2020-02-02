using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] private float stopTime = 0.5f;
    [SerializeField] private float dropForce = 0.1f;

    public LayerMask targetCollisionMask;
    [SerializeField] private int attackDamage = 20;

    private Player player;
    private Rigidbody2D rb;
    private bool disableSkills = false;

    protected Animator animator;

    // Skill states
    private bool dashPressed = false;
    private bool transcendPressed = false;
    private float transcendPressTime;

    private bool doGroundPound = false;
    private bool isGroundpounding = false;

    public Collider2D attackHitbox;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Dash input
        if (Input.GetButtonDown("Tab"))
            dashPressed = true;

        if (Input.GetKeyDown("q"))
        {
            if (!player.onGround)
            {
                Debug.Log("GP pressed");
                doGroundPound = true;
            }
        }

        if (Input.GetKeyDown("w"))
            animator.SetTrigger("attack_w"); ;
    }

    void FixedUpdate()
    {
        if(!disableSkills)
        {
            if (dashPressed) useDash();
            dashPressed = false;

            if (doGroundPound && !isGroundpounding) GroundPoundAttack();
            doGroundPound = false;
        }
    }

    private void disableAllControls()
    {
        player.disableControls = true;
        disableSkills = true;
    }

    private void enableAllControls()
    {
        player.disableControls = false;
        disableSkills = false;
    }

    private void resetAllSkillInputs()
    {
        dashPressed = false;
    }

    private void useDash()
    {
        // TODO deduct stam

        // find direction of dash
        float moveDirection = 1f;   // 1 = right, -1 = left; default as facing right
        if (!player.facingRight)
            moveDirection = -1f;

        Vector2 bodyVelocity = new Vector2(moveDirection, 0f);
        float animationTime = 0.25f;
        // temporarily disable all controls until skill is over
        StartCoroutine(suspendControls(animationTime));
        // add force to player's rigid body
        player.pushBody(bodyVelocity, 700f);

        // Revert back to custom physics after animation
        StartCoroutine(tempAddRigidBodyWeight(animationTime));
    }

    // Do the Ground Pound attack
    private void GroundPoundAttack()
    {
        Debug.Log("GP called");
        isGroundpounding = true;
        StopAndSpin();
        StartCoroutine("DropAndSmash");
    }

    // Animation at the start of a ground pound attack (spin in the air)
    private void StopAndSpin()
    {
        Debug.Log("GP StopAndSpin");
        ClearForces();
        rb.gravityScale = 0;
        animator.SetTrigger("isSpinning");
        //player.pushBody(Vector2.up, 0.08f);
    }

    // After a few seconds (after the stop and spin) drop down.
    private IEnumerator DropAndSmash()
    {
        yield return new WaitForSeconds(stopTime);
        player.pushBody(Vector2.down, dropForce);
        Debug.Log("GP DropAndSmash");
        //animator.SetBool("isDropping", true);
    }

    // Player has dropped onto the ground.
    private void CompleteGroundPound()
    {
        StartCoroutine(tempAddRigidBodyWeight(0.0f));
        player.setPhysicsEnabled(true);
        isGroundpounding = false;
        //animator.SetBool("isDropping", false);
        Debug.Log("GP COMPLETES");
    }

    private void ClearForces()
    {
        Debug.Log("GP ClearForces");
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        player.bodyVelocity = Vector2.zero;
        player.setPhysicsEnabled(false);
    }

    public void Attack_W()
    {
        Debug.Log("ATTACK W");
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(targetCollisionMask);
        filter.useLayerMask = true;

        Collider2D[] hit = new Collider2D[16];
        Physics2D.OverlapCollider(attackHitbox, filter, hit);

        // Check all collisions with attack. TODO: Check if it has same tag as Target
        foreach (Collider2D c in hit)
        {
            if (c != null && c.transform != null & c.gameObject != null && c.transform.CompareTag("enemy"))
            {
                Debug.Log("ATTACKED: "+c.transform.tag);
                if(c.gameObject.GetComponent<EnemyGround>() != null)
                    c.gameObject.GetComponent<EnemyGround>().ReceiveDamage(attackDamage);
                else if (c.gameObject.GetComponent<EnemyFlying>() != null)
                    c.gameObject.GetComponent<EnemyFlying>().ReceiveDamage(attackDamage);
            }
        }
    }

    IEnumerator suspendControls(float time)
    {
        disableAllControls();
        yield return new WaitForSeconds(time);
        enableAllControls();
        resetAllSkillInputs();
    }

    IEnumerator tempAddRigidBodyWeight(float time)
    {
        rb.mass = 1f;
        yield return new WaitForSeconds(time);
        rb.mass = 0.0001f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal.y >= 0.5)
        {
            CompleteGroundPound();
        }
    }

}
