using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SpaceDockScript : MonoBehaviour
{

    public GameObject selectedUnit;
    public GameObject selectedDock;

    public bool unitSelected = false;
    public bool dockSelected = false;

    //
    public GameObject playerManager;
    public GameObject uiHandler;

    public int engine = 0;
    public int shieldGenerator = 0;
    public int armor = 0;
    public int beamWeapon = 0;
    public int missile = 0;
    public int antiMissle = 0;
    public int lifeSupport = 0;
    public int repairSystem = 0;

    //Animator Stuff
    public Animator anim;
    public string tColor;


    private void Awake()
    {
        playerManager = GameObject.Find("PlayerManager");
        uiHandler = GameObject.Find("UI Handler");

        //Animator Stuff
        tColor = this.GetComponent<BaseUnitScript>().teamColor;
    }

    public void Update()
    {
        if (playerManager.GetComponent<UnitMovement>().selectedUnit != null)
        {
            if (this.gameObject == playerManager.GetComponent<UnitMovement>().selectedUnit)
                uiHandler.GetComponent<BuildOptionsScript>().selectedDock = this.gameObject;

        }
        if (playerManager.GetComponent<UnitMovement>().selectedUnit == null)
            uiHandler.GetComponent<BuildOptionsScript>().selectedDock = null;

        SetDockState();
    }

    public void SetDockState()
    {

            if (tColor == "blue")
            {
                anim.SetBool("isBlue", true);
            }
            else if (tColor == "yellow")
            {
                anim.SetBool("isYellow", true);
            }
            else if (tColor == "green")
            {
                anim.SetBool("isGreen", true);
            }
            else if (tColor == "red")
            {
                anim.SetBool("isRed", true);
            }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<BaseUnitScript>().teamColor == this.gameObject.GetComponent<BaseUnitScript>().teamColor)
        {
            GameObject tempSelectedUnit;
            GameObject tempSelectedDock;

            tempSelectedUnit = collision.gameObject;
            tempSelectedDock = this.gameObject;

            selectedUnit = tempSelectedUnit;
            unitSelected = true;

            selectedDock = tempSelectedDock;
            dockSelected = true;

            uiHandler.GetComponent<BuildOptionsScript>().selectedDock = selectedDock;
            uiHandler.GetComponent<BuildOptionsScript>().dockedShip = selectedUnit;

            playerManager.GetComponent<PlayerManagerScript>().isCollided = true ;
            uiHandler.GetComponent<ButtonActivationScript>().isDocked = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {


        if (collision.gameObject.GetComponent<BaseUnitScript>().teamColor == this.gameObject.GetComponent<BaseUnitScript>().teamColor)
        {
            selectedUnit = null;
            selectedDock = null;
            unitSelected = false;
            dockSelected = false;

            uiHandler.GetComponent<BuildOptionsScript>().selectedDock = null;
            uiHandler.GetComponent<BuildOptionsScript>().dockedShip = null;

            playerManager.GetComponent<PlayerManagerScript>().isCollided = false;
            uiHandler.GetComponent<ButtonActivationScript>().isDocked = false;

        }
    }







}

