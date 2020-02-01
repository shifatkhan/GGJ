using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;
    private bool leverIsOn;
    private bool interactAllowed;

    private void Awake()
    {
        leverIsOn = false;
    }
    private void Update()
    {
        if (interactAllowed && Input.GetButtonDown("Interact"))
        {
            UseLever();
        }
    }
    void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactAllowed = true;
            Debug.Log("Hello");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactAllowed = false;
        }
    }
    private void UseLever()
    {
        if (leverIsOn)
            leverIsOn = false;
        else
            leverIsOn = true;

        myAnimationController.SetBool("leverActivated", leverIsOn);
    }
}
