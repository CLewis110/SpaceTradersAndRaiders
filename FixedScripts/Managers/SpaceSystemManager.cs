//Created by teammate

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SpaceSystemManager : MonoBehaviour
{
    private GameObject gameManager;
    public string owner;
    [SerializeField] private bool isOwned = false;
    public string ownedColor;
    private int commonMinerals = 0;     //worth: 1pt per 10 stored on a players system
    private int rareMinerals = 0;       //worth 2 pts per 10 stored on a system
    private int veryRMinerals = 0;      //worth 3pts per 10 stored on a system

    public enum SystemType
    {
        none,
        alive,
    }

    public SystemType systemType = SystemType.none;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        if(isOwned)
        {
            systemType = SystemType.alive;
        }
        else
        {
            ownedColor = gameManager.GetComponent<GameManagerScript>().curPlayer;
        }
    }

    public int CalcMineralPoints(string playerColor)
    {
        int points = 0;

        //Debug.LogError("Color: " + ownedColor + "Minerals: " + commonMinerals);

        if (ownedColor.Equals(playerColor))
        {
            if (commonMinerals >= 10)
            {
                points += (commonMinerals / 10) * 1;
                //Debug.Log("Common points: " + points);
            }

            if (rareMinerals >= 10)
            {
                points += (rareMinerals / 10) * 2;
                //Debug.Log("Common + rare points: " + points);
            }

            if (veryRMinerals >= 10)
            {
                points += (veryRMinerals / 10) * 3;
                //Debug.Log("Common + very rare points: " + points);
            }
        }

        return points;
    }

    public void GenerateMinerals(string playerColor)
    {
        /*
         * TEMPORARY RATE: 50C, 30R, 20VR
         */
        
        if(isOwned)
        {
            if(playerColor.Equals(ownedColor))
            {
                switch (systemType)
                {
                    case SystemType.alive:
                        {
                            commonMinerals += 50;
                            rareMinerals += 30;
                            veryRMinerals += 20;

                            //Debug.LogError("Owner: " + ownedColor + " C: " + commonMinerals.ToString() + " R: " + rareMinerals.ToString()
                              //  + " VR: " + veryRMinerals.ToString());
                            break;
                        }
                    default:
                        {
                            Debug.Log("None is selected for SystemType.");
                            break;
                        }
                }//END Switch SystemType
            }//END If matching color
        }//END If isOwned
    }//END GenerateMinerals
}//END SpaceSystemManager
