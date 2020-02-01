using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string newLevel;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (animator.GetBool("leverActivated")))
        {
            SceneManager.LoadScene(newLevel);
        }
    }
}