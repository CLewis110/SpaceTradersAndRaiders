using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTypeScript : MonoBehaviour
{
    public int maxCargoSpace;
    public int usedCargoSpace = 0;

    public GameObject dock;
    public GameObject uiHandler;

    public string shipName;
    //Components
    public int engine = 1;
    public int shieldGenerator = 0;
    public int armor = 0;
    public int beamWeapon = 0;
    public int missile = 0;
    public int antiMissle = 0;
    public int lifeSupport = 0;
    public int repairSystem = 0;

    //calculated values
    public int hullMax = 100;
    public int hull;
    public int shields;
    public int repair;
    public int movement;

    public int damage;

    public string tColor;

    //Sprites
    public Animator anim;

    //Array to keep track of ship components and display to ui


    private void Awake()
    {
        tColor = this.GetComponent<BaseUnitScript>().teamColor;
        ShipType();

        shipName = this.tag;
        uiHandler = GameObject.Find("UI Handler");

        setColor();

        damage = 10;
        repair = 10;

    }

    private void Update()
    {

        setShipState();
    }

    public void setShipState()
    {
        if (armor > 0 && armor < 3)
        {
            anim.SetBool("isArmored", true);
        }
        else if(armor == 0)
            anim.SetBool("isArmored", false);

        if (armor >= 3)
        {
            anim.SetBool("isArmored2", true);
        }
        else
            anim.SetBool("isArmored2", false);

        if (shields > 0)
        {
            anim.SetBool("isShielded", true);
        }
        else
            anim.SetBool("isShielded", false);
    }

    public void setColor()
    {
        if(tColor == "blue")
        {
            anim.SetBool("isBlue", true);
        }
        else if (tColor == "yellow")
        {
            anim.SetBool("isYellow", true);
        }
        else if(tColor == "green")
        {
            anim.SetBool("isGreen", true);
        }
        else if (tColor == "red")
        {
            anim.SetBool("isRed", true);
        }



    }

    //Initial ship type values
    public void ShipType()
    {
        if (this.tag == "Frigate")
        {
            maxCargoSpace = 5;
            hull = 30;
            shields = 4;
            movement = 7;
        }

        if (this.tag == "Destroyer")
        {
            maxCargoSpace = 10;
            hull = 45;
            shields = 4;
            movement = 7;
        }

        if (this.tag == "Cruiser")
        {
            maxCargoSpace = 15;
            hull = 60;
            shields = 5;
            movement = 5;
        }

        if (this.tag == "Battleship")
        {
            maxCargoSpace = 20;
            hull = 65;
            shields = 6;
            movement = 5;
        }

        if (this.tag == "Dreadnaught")
        {
            maxCargoSpace = 25;
            hull = 100;
            shields = 7;
            movement = 4;
        }

    }

    //Add components
    public void AddEngineComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            engine++;
            usedCargoSpace++;
            movement += 1;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().engine--;
        }
        else
            Debug.Log("No cargo space left!");

    }

    public void AddShieldGeneratorComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            shieldGenerator++;
            usedCargoSpace++;
            shields += 1;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().shieldGenerator--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void AddArmorComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            armor++;
            usedCargoSpace++;
            hull += 1;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().armor--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void AddBeamWeapon()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            beamWeapon++;
            usedCargoSpace++;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().beamWeapon--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void AddMissleComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            missile++;
            usedCargoSpace++;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().missile--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void AddAntiMissle()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            antiMissle++;
            usedCargoSpace++;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().antiMissle--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void AddLifeSupport()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            lifeSupport++;
            usedCargoSpace++;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().lifeSupport--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void AddRepairSystem()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            repairSystem++;
            usedCargoSpace++;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().repairSystem--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    //Remove Components
    public void RemoveEngineComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            engine--;
            usedCargoSpace--;
            movement--;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().engine--;
        }
        else
            Debug.Log("No cargo space left!");

    }

    public void RemoveShieldGeneratorComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            shieldGenerator--;
            usedCargoSpace--;
            shields--;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().shieldGenerator--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void RemoveArmorComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            armor--;
            usedCargoSpace--;
            hull -= 1;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().armor--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void RemoveBeamWeapon()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            beamWeapon--;
            usedCargoSpace--;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().beamWeapon--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void RemoveMissleComponent()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            missile--;
            usedCargoSpace--;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().missile--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void RemoveAntiMissle()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            antiMissle--;
            usedCargoSpace--;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().antiMissle--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void RemoveLifeSupport()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            lifeSupport--;
            usedCargoSpace--;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().lifeSupport--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    public void RemoveRepairSystem()
    {
        if (usedCargoSpace < maxCargoSpace)
        {
            repairSystem--;
            usedCargoSpace--;
            dock = uiHandler.GetComponent<BuildOptionsScript>().selectedDock;
            dock.GetComponent<SpaceDockScript>().repairSystem--;
        }
        else
            Debug.Log("No cargo space left!");
    }

    //Calculate moveDist

    public bool TakeDamage(int dmg)
    {
        if (shields > 0)
            shields -= dmg;
        if(shields <= 0)
        {
            shields = 0;
            hull -= dmg;
        }


        if (hull <= 0)
            return true;
        else
            return false;
    }

    public void RepairDamage(int rpr)
    {
        hull += rpr;
    }

    public void Die()
    {
        Destroy(gameObject);
    }


}