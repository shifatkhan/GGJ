using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private Player player;
    private bool disableSkills = false;

    // Skill states
    private bool dashPressed = false;
    private bool transcendPressed = false;
    private float transcendPressTime;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Tab"))
            dashPressed = true;
    }

    void FixedUpdate()
    {
        if(!disableSkills)
        {
            if (dashPressed) useDash();
            dashPressed = false;
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
        float animationTime = 0.2f;
        // temporarily disable all controls until skill is over
        StartCoroutine(suspendControls(animationTime));
        // add force to player's rigid body
        player.pushBody(bodyVelocity, 1000f, animationTime);
    }

    IEnumerator suspendControls(float time)
    {
        disableAllControls();
        yield return new WaitForSeconds(time);
        enableAllControls();
        resetAllSkillInputs();
    }

}
