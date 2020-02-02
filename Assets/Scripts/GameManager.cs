using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject repairedGrid;
    [SerializeField] private GameObject brokenGrid;

    private bool isRepairedGrid;

    //Variables to control the karma/duration of other world swap
    private float karma;
    private float karmaCost = 20f;
    private float karmaUsage = 0.5f; //Change to a timed system or smth

    void Start()
    {
        isRepairedGrid = false;
        brokenGrid.SetActive(true);
        repairedGrid.SetActive(false);

        //Change base karma later
        karma = 100f;
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && (karma > karmaCost))
        {
            ChangeTileSet();
        }
        if (isRepairedGrid)
        {
            karma -= karmaUsage;

            if (karma == 0.0)
            {
                ChangeTileSet();
            }
        }
        else if (!isRepairedGrid && karma < 100)
        {
            karma += karmaUsage;
        }
    }
    //Swaps the active tileset
    private void ChangeTileSet()
    {
        if (isRepairedGrid)
        {
            repairedGrid.SetActive(false);
            brokenGrid.SetActive(true);
        }
        else
        {
            brokenGrid.SetActive(false);
            repairedGrid.SetActive(true);
        }

        isRepairedGrid = !isRepairedGrid;
    }
}
