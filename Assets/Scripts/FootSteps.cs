using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    bool isMoving = false;
    AudioSource audioSrc;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(player.onGround && GetComponent<AudioSource>().isPlaying == false )
        // {
        //     GetComponent<AudioSource>().volume = Random.Range(0.8f, 1);
        //     GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.1f);
        //     GetComponent<AudioSource>().Play();
        // }

    }
}
