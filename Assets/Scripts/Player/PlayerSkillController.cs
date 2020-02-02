using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] private float stopTime = 0.5f;
    [SerializeField] private float dropForce = 0.1f;

    private Player player;
    private Rigidbody2D rb;
    private bool disableSkills = false;

    // Skill states
    private bool dashPressed = false;
    private bool transcendPressed = false;
    private float transcendPressTime;

    private bool doGroundPound = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Dash input
        if (Input.GetButtonDown("Tab") && player.stamina >= 30)
            dashPressed = true;

        if (Input.GetKeyDown("q"))
        {
            if (!player.onGround)
            {
                doGroundPound = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(!disableSkills)
        {
            if (dashPressed) useDash();
            dashPressed = false;

            if (doGroundPound) GroundPoundAttack();
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
        player.UseStamina(30);

        // Revert back to custom physics after animation
        StartCoroutine(tempAddRigidBodyWeight(animationTime));
    }

    // Do the Ground Pound attack
    private void GroundPoundAttack()
    {
        StopAndSpin();
        StartCoroutine("DropAndSmash");
    }

    // Animation at the start of a ground pound attack (spin in the air)
    private void StopAndSpin()
    {
        ClearForces();
        rb.gravityScale = 0;
    }

    // After a few seconds (after the stop and spin) drop down.
    private IEnumerator DropAndSmash()
    {
        yield return new WaitForSeconds(stopTime);
        player.pushBody(Vector2.down, dropForce);
    }

    // Player has dropped onto the ground.
    private void CompleteGroundPound()
    {
        StartCoroutine(tempAddRigidBodyWeight(0.0f));
    }

    private void ClearForces()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        player.bodyVelocity = Vector2.zero;
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
        ClearForces();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal.y >= 0.5)
        {
            CompleteGroundPound();
        }
    }

}
