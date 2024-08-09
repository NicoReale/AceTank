using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler
{

    Image _slider;
    public PlayerUIHandler(Image slider)
    {
        _slider = slider;
    }


    public void UpdateSlider(float HP)
    {
        _slider.fillAmount = HP / 100;
    }

}
