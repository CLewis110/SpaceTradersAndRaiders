using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuildOptionsScript : MonoBehaviour
{
    public GameObject optionsWindow;
    public GameObject shipWindow;
    public GameObject componentWindow;
    public GameObject ShipInventory;

    public GameObject selectedDock;
    public GameObject barracksSelected;
    public GameObject dockedShip;

    public GameObject frigate;
    public GameObject destroyer;
    public GameObject cruiser;
    public GameObject battleship;
    public GameObject dreadnaught;

    public void Options()
    {
        optionsWindow.SetActive(true);
    }

    public void Back()
    {
        optionsWindow.SetActive(true);
        shipWindow.SetActive(false);
        componentWindow.SetActive(false);
        ShipInventory.SetActive(false);
    }

    public void BuildShip()
    {
        optionsWindow.SetActive(false);
        shipWindow.SetActive(true);

    }

    public void BuildComponent()
    {
        optionsWindow.SetActive(false);
        componentWindow.SetActive(true);
    }

    public void CollectComponent()
    {
        optionsWindow.SetActive(false);
        ShipInventory.SetActive(true);
    }

    public void Exit()
    {
        optionsWindow.SetActive(false);
    }

    public void BuildFrigate()
    {
        Instantiate(frigate);
    }

    public void BuildDestroyer()
    {
        Instantiate(destroyer);
    }


    public void BuildCruiser()
    {
        Instantiate(cruiser);
    }


    public void BuildBattleship()
    {
        Instantiate(battleship);
    }


    public void BuildDreadnaught()
    {
        Instantiate(dreadnaught);
    }

    public void BuildEngineComponent()
    {
        selectedDock.GetComponent<SpaceDockScript>().engine++;
    }

    public void BuildShieldGeneratorComponent()
    {
        selectedDock.GetComponent<SpaceDockScript>().shieldGenerator++;
    }

    public void BuildArmorComponent()
    {
        selectedDock.GetComponent<SpaceDockScript>().armor++;
    }

    public void BuildBeamWeapon()
    {
        selectedDock.GetComponent<SpaceDockScript>().beamWeapon++;
    }

    public void BuildMissleComponent()
    {
        selectedDock.GetComponent<SpaceDockScript>().missile++;
    }

    public void BuildAntiMissle()
    {
        selectedDock.GetComponent<SpaceDockScript>().antiMissle++;
    }

    public void BuildLifeSupport()
    {
        selectedDock.GetComponent<SpaceDockScript>().lifeSupport++;
    }

    public void BuildRepairSystem()
    {
        selectedDock.GetComponent<SpaceDockScript>().repairSystem++;
    }

    public void CollectEngineComponent()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().engine != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddEngineComponent();
            else
                Debug.Log("No engine components!");
        }
        else
            Debug.Log("No ship docked!");

    }

    public void CollectShieldGeneratorComponent()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().shieldGenerator != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddShieldGeneratorComponent();
            else
                Debug.Log("No shield generator components!");
        }
        else
            Debug.Log("No ship docked!");
    }

    public void CollectArmorComponent()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().armor != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddArmorComponent();
            else
                Debug.Log("No armor components!");
        }
        else
            Debug.Log("No ship docked!");
    }

    public void CollectBeamWeapon()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().beamWeapon != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddBeamWeapon();
            else
                Debug.Log("No beam weapon components!");
        }
        else
            Debug.Log("No ship docked!");
    }

    public void CollectMissleComponent()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().missile != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddMissleComponent();
            else
                Debug.Log("No missile components!");
        }
        else
            Debug.Log("No ship docked!");
    }

    public void CollectAntiMissle()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().antiMissle != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddAntiMissle();
            else
                Debug.Log("No missile components!");
        }
        else
            Debug.Log("No ship docked!");
    }

    public void CollectLifeSupport()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().lifeSupport != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddLifeSupport();
            else
                Debug.Log("No life support components!");
        }
        else
            Debug.Log("No ship docked!");

    }

    public void CollectRepairSystem()
    {
        if (dockedShip != null)
        {
            if (selectedDock.GetComponent<SpaceDockScript>().repairSystem != 0)
                dockedShip.GetComponent<ShipTypeScript>().AddRepairSystem();
            else
                Debug.Log("No repair system components!");
        }
        else
            Debug.Log("No ship docked!");
    }


    public void EndTurn()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

        gameManager.GetComponent<GameManagerScript>().SelectCycleTurn();
    }
}
