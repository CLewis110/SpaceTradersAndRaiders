using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitScript : MonoBehaviour
{
    public string teamColor;

    public int x;
    public int y;

    public UnitMovement unitMove;

    public GameObject playerManager;
    public GameObject UIHandler;
    public GameObject gameManager;

    public bool isSelected = false;

    private float moveSpeed = 1f;

    //Distance between start and end points to keep track of distance
    private float journeyLength;
    private float distanceAllowed;

    private bool unitInMovemet;

    public enum movementStates
    {
        Unselected,
        Selected,
        Moved,
        Wait
    }
    public movementStates unitMoveState;

    //Pathfinding variables

    //Path for moving unit's transform

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //Debug.Log("After: " + gameManager.ToString());
        AssignTeam();
        SetLocation();
        unitMoveState = movementStates.Unselected;
    }

    public void AssignTeam()
    {
        teamColor = gameManager.GetComponent<GameManagerScript>().curPlayer;
    }

    public void moveAgain()
    {
        setMovementState(0);
        //setIdleAnimation();

    }

    public movementStates getMovementStateEnum(int i)
    {
        if (i == 0)
        {

            return movementStates.Unselected;
        }
        else if (i == 1)
        {

            return movementStates.Selected;

        }
        else if (i == 2)
        {
            return movementStates.Moved;
        }
        else if (i == 3)
        {
            return movementStates.Wait;
        }
        return movementStates.Unselected;
    }


    public void setMovementState(int i)
    {
        if (i == 0)
        {

            unitMoveState = movementStates.Unselected;
        }
        else if (i == 1)
        {

            unitMoveState = movementStates.Selected;
        }
        else if (i == 2)
        {
            unitMoveState = movementStates.Moved;
        }
        else if (i == 3)
        {
            unitMoveState = movementStates.Wait;
        }
    }





    //Need to fix
    public void SetLocation()
    {
        //this.transform.position = unitMove.selectedUnit.transform.position;
    }

    public void dealDamage(int x)
    {
        //Update UI
        //currentHealthPoints = currentHealthPoints - x;
    }

    public void unitDie()
    {
        //Destroy game object
    }



}