using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TargetSelect : MonoBehaviour
{

    public GameObject targetShip;
    public bool unitSelected = false;

    //public GameObject gm;

    private void Awake()
    {
        //gm = GameObject.Find("GameManager");
        targetShip = null;

    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (targetShip == null)
            {
                MouseClickToSelectedUnit();
            }
        }
              
        if (Input.GetMouseButton(1))
        {
            DeselectUnit();
        }
      
    }

    void MouseClickToSelectedUnit()
    {
        GameObject tempSelectedUnit;


        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        if (hit.collider != null)
        {
            if (unitSelected == false)
            {
                tempSelectedUnit = hit.transform.gameObject;

                //Only select enemy ships -- Need to fix combat initiation
                // if (tempSelectedUnit.GetComponent<BaseUnitScript>().teamColor != gm.GetComponent<GameManagerScript>().curPlayer)
                {

                        targetShip = tempSelectedUnit;
                        unitSelected = true;

                }

            }
        }

    }

    void DeselectUnit()
    {
        if (targetShip != null)
        {

                targetShip = null;
                unitSelected = false;


        }

    }

}
