using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivationScript : MonoBehaviour
{
    public DockButton dockOptions;
    public FightButton fight;
    public BarracksButton barracks;

    public bool isFighting;
    public bool atBarracks;
    public bool isDocked;

    public GameObject UIHandler;
    

    void Start()
    {
        UIHandler = GameObject.Find("UI Handler");
        dockOptions.myDockButton.gameObject.SetActive(false);
        //fight.ftgBtn.gameObject.SetActive(false);
        barracks.myBarracksButton.gameObject.SetActive(false);
        isFighting = false;
        atBarracks = false;

    }

    public void Update()
    {

        if (UIHandler.gameObject.GetComponent<BuildOptionsScript>().selectedDock != null)
            DockButton();

        if (UIHandler.gameObject.GetComponent<BuildOptionsScript>().selectedDock == null && !isDocked)
            DockButtonDisable();


        if (isFighting)
            FightButton();
        if (!isFighting)
            //FightButtonDisable();

        if (UIHandler.gameObject.GetComponent<BuildOptionsScript>().barracksSelected != null)
            BarracksButton();
        if (!atBarracks && UIHandler.gameObject.GetComponent<BuildOptionsScript>().barracksSelected == null)
            BarracksButtonDisable();

    }


    public void DockButton()
    {
        dockOptions.myDockButton.gameObject.SetActive(true);
    }

    public void DockButtonDisable()
    {
        dockOptions.myDockButton.gameObject.SetActive(false);
    }

    public void FightButton()
    {
        fight.ftgBtn.gameObject.SetActive(true);
    }

    public void FightButtonDisable()
    {
        fight.ftgBtn.gameObject.SetActive(false);
    }

    public void BarracksButton()
    {
        barracks.myBarracksButton.gameObject.SetActive(true);
    }

    public void BarracksButtonDisable()
    {
        barracks.myBarracksButton.gameObject.SetActive(false);
    }
}
