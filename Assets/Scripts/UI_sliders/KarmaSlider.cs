using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarmaSlider : MonoBehaviour
{
    public GameManager playerKarma;
    public Image fillImage;
    private Slider slider;
    private Color temp;

    void Awake()
    {
        slider = GetComponent<Slider>();
        temp = fillImage.color;
    }
    void Update()
    {
        float fillValue = (float)playerKarma.karma / playerKarma.maxKarma;
        if (fillValue <= slider.maxValue / 5)
        {
            fillImage.color = Color.white;
        }
        else
        {
            fillImage.color = temp;
        }
        slider.value = fillValue;
    }
}
