using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text shieldsText;
    public Slider hullSlider;
    public Image shipDisplay;


    public void SetHUD(ShipTypeScript ship)
    {
        nameText.text = ship.shipName;
        shieldsText.text = "Shields: " + ship.shields;
        hullSlider.maxValue = ship.hullMax;
        hullSlider.value = ship.hull;
    }

    public void SetHP(int hp, int shields)
    {
        hullSlider.value = hp;
        shieldsText.text = "Shields: " + shields;
    }
}
