using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject repairedGrid;
    [SerializeField] private GameObject brokenGrid;
    [SerializeField] private GameObject lever;
    public bool isRepairedGrid;

    //Variables to control the karma/duration of other world swap
    public float karma;
    public float maxKarma;
    private float karmaCost = 20f;
    private float karmaUsage = 20f; //Change to a timed system or smth

    void Start()
    {
        if (lever != null)
            lever.SetActive(false);

        isRepairedGrid = false;
        brokenGrid.SetActive(true);
        repairedGrid.SetActive(false);

        //Change base karma later
        karma = 100f;
        maxKarma = 100f;
    }

    void Update()
    {
        if (Input.GetButtonDown("Transcend") && (karma > karmaCost))
        {
            ChangeTileSet();
        }
        if (isRepairedGrid)
        {
            karma -= karmaUsage * Time.deltaTime;

            if (karma <= 0)
            {
                ChangeTileSet();
            }
        }
        else if (!isRepairedGrid && karma < 100)
        {
            karma += karmaUsage * Time.deltaTime;
        }
    }
    //Swaps the active tileset
    private void ChangeTileSet()
    {
        if (isRepairedGrid)
        {
            repairedGrid.SetActive(false);
            brokenGrid.SetActive(true);

            if (lever != null)
                lever.SetActive(false);
        }
        else
        {
            brokenGrid.SetActive(false);
            repairedGrid.SetActive(true);

            if (lever != null)
                lever.SetActive(true);
        }

        isRepairedGrid = !isRepairedGrid;
    }
}
