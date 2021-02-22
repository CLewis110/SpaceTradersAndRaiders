using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject victory;
    public GameObject redLose;
    public GameObject greenLose;
    public GameObject blueLose;
    public GameObject yellowLose;

    public void Victory()
    {
        victory.SetActive(true);
    }

    public void RedLose()
    {
        victory.SetActive(true);
    }

    public void GreenLose()
    {
        victory.SetActive(true);
    }

    public void BlueLose()
    {
        victory.SetActive(true);
    }

    public void YellowLose()
    {
        victory.SetActive(true);
    }
}
