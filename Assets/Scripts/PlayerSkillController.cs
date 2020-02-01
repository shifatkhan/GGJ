using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private Player player;
    private bool disableSkills = false;

    // Skill states
    private bool tabPressed = false;

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
            tabPressed = true;          // TODO add false
    }

    void FixedUpdate()
    {
        if(!disableSkills)
        {
            if (tabPressed) useDash();
            tabPressed = false;
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

    private void useDash()
    {
        

    }

}
