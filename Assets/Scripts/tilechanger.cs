using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilechanger : MonoBehaviour
{
    [SerializeField] private GameObject repairedGrid;
    [SerializeField] private GameObject badGrid;

    private bool isRepairedGrid;

    //Variables to control the karma/duration of other world swap
    //Add to Player Script????
    private int karma;
    private int karmaCost = 20;
    private int karmaUsage = 1; //Change to a timed system or smth

    void Start()
    {
        isRepairedGrid = false;
        badGrid.SetActive(true);
        repairedGrid.SetActive(false);

        //Change later
        karma = 100;
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

            if (karma == 0)
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
            badGrid.SetActive(true);
        }
        else
        {
            badGrid.SetActive(false);
            repairedGrid.SetActive(true);
        }

        isRepairedGrid = !isRepairedGrid;
    }
}
