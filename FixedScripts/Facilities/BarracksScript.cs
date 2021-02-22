using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksScript : MonoBehaviour
{
    public GameObject selectedUnit;
    public GameObject selectedBarracks;

    public bool unitSelected = false;
    public bool barracksSelected = false;

    //
    public GameObject playerManager;
    public GameObject uiHandler;

    private void Awake()
    {
        playerManager = GameObject.Find("PlayerManager");
        uiHandler = GameObject.Find("UI Handler");
    }

    public void Update()
    {
        if (playerManager.GetComponent<UnitMovement>().selectedUnit != null)
        {
            if (this.gameObject == playerManager.GetComponent<UnitMovement>().selectedUnit)
            {
                uiHandler.GetComponent<BuildOptionsScript>().barracksSelected = this.gameObject;
            }

        }
        if (playerManager.GetComponent<UnitMovement>().selectedUnit == null)
            uiHandler.GetComponent<BuildOptionsScript>().barracksSelected = null;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.GetComponent<BaseUnitScript>().teamColor == this.gameObject.GetComponent<BaseUnitScript>().teamColor)
        {
            GameObject tempSelectedUnit;
            GameObject tempSelectedBarracks;

            tempSelectedUnit = collision.gameObject;
            tempSelectedBarracks = this.gameObject;

            selectedUnit = tempSelectedUnit;
            unitSelected = true;

            selectedBarracks = tempSelectedBarracks;
            barracksSelected = true;

            uiHandler.GetComponent<BuildOptionsScript>().barracksSelected = selectedBarracks;
            uiHandler.GetComponent<BuildOptionsScript>().dockedShip = selectedUnit;

            playerManager.GetComponent<PlayerManagerScript>().isCollided = true;
            uiHandler.GetComponent<ButtonActivationScript>().atBarracks = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {


        if (collision.gameObject.GetComponent<BaseUnitScript>().teamColor == this.gameObject.GetComponent<BaseUnitScript>().teamColor)
        {
            selectedUnit = null;
            selectedBarracks = null;
            unitSelected = false;
            barracksSelected = false;

            uiHandler.GetComponent<BuildOptionsScript>().barracksSelected = null;
            uiHandler.GetComponent<BuildOptionsScript>().dockedShip = null;

            playerManager.GetComponent<PlayerManagerScript>().isCollided = false;
            uiHandler.GetComponent<ButtonActivationScript>().atBarracks = false;

        }
    }
}
