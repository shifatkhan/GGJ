using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Transform checkPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") && checkPoint != null)
        {
            transform.position = checkPoint.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("CheckPoint"))
        {
            checkPoint = collider.transform;
            collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}
