using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnitMovement : MonoBehaviour
{

    public GameObject selectedUnit;
    public bool unitSelected = false;

    private Vector3 targetPos;
    private float speed = 5f;

    public bool canMove = false;

    public GameObject gm;   
    public GameObject uiHandler;

    private void Awake()
    {
        uiHandler = GameObject.Find("UI Handler");
        selectedUnit = null;
        gm = GameObject.Find("GameManager");
    }

    void Update()
    {
        //Debug.Log("Canmove: " + canMove.ToString() + " Test: " + test);

        if (canMove)
        {
            if (Input.GetMouseButton(0))
            {
                if (selectedUnit == null)
                {
                    MouseClickToSelectedUnit();
                }
                else if (selectedUnit.GetComponent<BaseUnitScript>().unitMoveState == selectedUnit.GetComponent<BaseUnitScript>().getMovementStateEnum(1))
                {
                    if (selectedUnit.gameObject.tag != "SpaceDock" && selectedUnit.gameObject.tag != "Barracks")
                        Move();
                }
            }

            if (Input.GetMouseButton(1))
            {
                DeselectUnit();
            }
        }
    }

    void MouseClickToSelectedUnit()
    {
        GameObject tempSelectedUnit;


        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        if(hit.collider != null)
            {
                if (unitSelected == false)
                {
                    tempSelectedUnit = hit.transform.gameObject;

                    if (tempSelectedUnit.GetComponent<BaseUnitScript>().teamColor == gm.GetComponent<GameManagerScript>().curPlayer)
                    {
                        if (tempSelectedUnit.GetComponent<BaseUnitScript>().unitMoveState == tempSelectedUnit.GetComponent<BaseUnitScript>().getMovementStateEnum(0))

                        {
                            selectedUnit = tempSelectedUnit;
                            selectedUnit.GetComponent<BaseUnitScript>().setMovementState(1);
                            unitSelected = true;

                        }
                    }

                }
        }
        return;
    }

    public void DeselectUnit()
    {
        if(selectedUnit != null)
        {
            if(selectedUnit.GetComponent<BaseUnitScript>().unitMoveState == selectedUnit.GetComponent<BaseUnitScript>().getMovementStateEnum(1))
            {
                selectedUnit.GetComponent<BaseUnitScript>().setMovementState(0);

                selectedUnit = null;
                unitSelected = false;

                uiHandler.GetComponent<BuildOptionsScript>().barracksSelected = null;
                //uiHandler.GetComponent<BuildOptionsScript>().selectedDock = null;
            }
        }

    }

    //Move and turn player
    public void Move()
    {
        // Get mouse location
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get calculations from selectedUnit


        // Move
        selectedUnit.transform.position = new Vector3(target.x, target.y, selectedUnit.transform.position.z);
    }

}
