using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string newLevel;
    private bool interactAllowed;

    private void Awake()
    {
        interactAllowed = false;
    }
    private void Update()
    {
        if (interactAllowed && Input.GetButtonDown("Submit"))
            SceneManager.LoadScene(newLevel);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            interactAllowed = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            interactAllowed = false;
    }
}