using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Vector3 localScale;
    public EnemyGround enemyg;
    public EnemyFlying enemyf;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        if (enemyf != null)
            localScale.x = enemyf.health;
        else if (enemyg != null)
            localScale.x = enemyg.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyf != null)
            localScale.x = enemyf.health;
        else if(enemyg != null)
            localScale.x = enemyg.health;

        transform.localScale = localScale;
    }
}
