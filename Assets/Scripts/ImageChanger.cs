using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageChanger : MonoBehaviour
{
    [SerializeField] private Sprite newSprite;
    [SerializeField] private GameManager manager;
    private Sprite currentSprite;

    // Start is called before the first frame update

    void Start()
    {
        currentSprite = this.transform.GetComponent<UnityEngine.UI.Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isRepairedGrid)
            this.transform.GetComponent<UnityEngine.UI.Image>().sprite = newSprite;
        else
            this.transform.GetComponent<UnityEngine.UI.Image>().sprite = currentSprite;
    }
}
