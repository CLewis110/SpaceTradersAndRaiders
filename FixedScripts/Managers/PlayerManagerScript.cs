using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
//using CodeMonkey.Utils;

public class PlayerManagerScript : MonoBehaviour
{
    //public GameManagerScript gameManager;
    public PlayerManagerScript self;
    public string playerName;
    public bool notDone = true;
    public int playerTurn;
    public GameObject[] fleet;

    //Player states
    private enum TypeState
    {
        trader,
        raider
    }

    private enum TeamColor
    {
        red,
        blue,
        green,
        yellow,
        noone
    }

    public string typeState = TypeState.trader.ToString();
    public string teamColor = TeamColor.blue.ToString();

    //Player points
    private int wealth = 0;
    private int power = 0;
    private int achievePoints = 0;
    
    private int wealthMet = 0;
    private int powerMet = 0;
    private int achievePointsMet = 0;

    //public bool isTurn = false;
    public bool isCollided = false;
    public bool isCollidedWithEnemy = false;

    private int systemsOwned = 1;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerName = "Test";
    }

    public bool CheckIfMet()
    {
        if(wealthMet > 0 && powerMet > 0 && achievePointsMet > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetWealth()
    {
        return wealth;
    }

    public void SetWealth(int wealth)
    {
        this.wealth = wealth;
        //Debug.Log("SetWealth ran: " + GetWealth().ToString());
    }

    public int GetPower()
    {
        return power;
    }

    public void SetPower(int power)
    {
        this.power += power;
        //Debug.Log("Power: " + GetPower().ToString());
    }

    public int GetAchievePoints()
    {
        return achievePoints;
    }

    public void SetAchievePoints(int achievePoints)
    {
        this.achievePoints += achievePoints;
        //Debug.Log("AchievePoints: " + GetAchievePoints().ToString());
    }

    public void SetPointsMet(int wMet, int pMet, int aPMet)
    {
        wealthMet = wMet;
        powerMet = pMet;
        achievePointsMet = aPMet;
    }

    public void SetWealthMet(int wMet)
    {
        wealthMet = wMet;
    }

    public void SetPowerMet(int pMet)
    {
        powerMet = pMet;
    }

    public void SetAPMet(int aPMet)
    {
        achievePointsMet = aPMet;
    }

    public int GetSystemsOwned()
    {
        return systemsOwned;
    }
}