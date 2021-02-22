//Created by teammate
using TMPro;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using GridPathfindingSystem;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    //Turn Variables
    public bool endTurn;
    public int turnCount = 0;
    private string[] turnArray = new string[4];
    public string curPlayer;
    public int turnAssign;
    private GameObject curPlayerRef;
    
    private GameObject[] playerList;
    private int totalPlayers;
    
    //Text display variables
    public TextMeshProUGUI txtPTurn;
    public TextMeshProUGUI txtWealth;
    public TextMeshProUGUI txtPower;
    public TextMeshProUGUI txtAPoints;

    //Victor Variables
    private bool isVictor;
    private string victorName;

    //Grid Related Variables
    [SerializeField] private MovementTilemapVisual movementTilemapVisual;
    private Grid<GridCombatSystem.GridObject> grid;
    private MovementTilemap movementTilemap;
    public GridPathfinding gridPathfinding;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        int mapWidth = 48;
        int mapHeight = 48;
        float cellSize = 4f;
        Vector3 origin = new Vector3(0, 0);
        turnAssign = 0;

        playerList = GameObject.FindGameObjectsWithTag("PlayerManager");
        totalPlayers = playerList.Length;

        ChooseTurnOrder();

        for (int index = 0; index < totalPlayers; index++)
        {
            if (turnArray[turnCount] == playerList[index].GetComponent<PlayerManagerScript>().teamColor)
            {
                //playerList[index].GetComponent<PlayerManagerScript>().isTurn = true;
                playerList[index].GetComponent<UnitMovement>().enabled = true;
                playerList[index].GetComponent<UnitMovement>().canMove = true;
                //Debug.LogError("Turn: " + playerList[index].GetComponent<PlayerManagerScript>().teamColor + " bool: " + playerList[index].GetComponent<PlayerManagerScript>().isTurn);
                curPlayer = playerList[index].GetComponent<PlayerManagerScript>().teamColor;
                curPlayerRef = playerList[index];
            }
        }

        grid = new Grid<GridCombatSystem.GridObject>(mapWidth, mapHeight, cellSize, origin, (Grid<GridCombatSystem.GridObject> g, int x, int y) => new GridCombatSystem.GridObject(g, x, y));

        gridPathfinding = new GridPathfinding(origin + new Vector3(1, 1) * cellSize * .5f, new Vector3(mapWidth, mapHeight) * cellSize, cellSize);
        gridPathfinding.RaycastWalkable();
        //gridPathfinding.PrintMap((Vector3 vec, Vector3 size, Color color) => World_Sprite.Create(vec, size, color));

        movementTilemap = new MovementTilemap(mapWidth, mapHeight, cellSize, origin);
    }

    private void Start()
    {
        movementTilemap.SetTilemapVisual(movementTilemapVisual);
    }

    public void Update()
    {
        DisplayPlayerTurn();

        /*if (!isVictor)
        {
            if (playerList[0] != null)
            {
                VictoryCheck();
            }
        }*/
    }

    public void VictoryCheck()
    {
        //Victory condition constants
        const int TRADER_WEALTH = 50;
        const int TRADER_POWER = 50;
        const int TRADER_ACHIEVE_POINTS = 50;

        const int RAIDER_WEALTH = 60;
        const int RAIDER_POWER = 60;
        const int RAIDER_ACHIEVE_POINTS = 25;

        int numTotalSystems = 2;
        int numPlayerHeld = 0;
        GameObject lastPlayer;

        //Player type variables
        const string TRADER = "trader";
        const string RAIDER = "raider";

        //Run through array of player managers or controllers
        for(int index = 0; index < totalPlayers; index++)
        {
            //Check if only one player owns a system
            if (playerList[index].GetComponent<PlayerManagerScript>().GetSystemsOwned() >= 0)
            {
                //Store the player holding the system
                lastPlayer = playerList[index];
                numPlayerHeld++;

                if (numPlayerHeld == 1)
                {
                    //The last player is the victor
                    victorName = lastPlayer.GetComponent<PlayerManagerScript>().playerName;
                    isVictor = true;
                    break;
                }
                else
                {
                    Debug.LogError("Either more than one player holds a system or something is wrong.");
                }
            }

            //Check if one player own more than half of the systems
            if (playerList[index].GetComponent<PlayerManagerScript>().GetSystemsOwned() >= numTotalSystems * 0.5)
            {
                isVictor = true;
                victorName = playerList[index].GetComponent<PlayerManagerScript>().playerName;
                break;
            }

            //Check if player is raider or trader
            switch (playerList[index].GetComponent<PlayerManagerScript>().typeState)
            {
                case TRADER:
                {
                    //Check point values for each player
                    if(playerList[index].GetComponent<PlayerManagerScript>().GetWealth() >= TRADER_WEALTH)
                    {
                        playerList[index].GetComponent<PlayerManagerScript>().SetWealthMet(1);
                    }

                    if (playerList[index].GetComponent<PlayerManagerScript>().GetPower() >= TRADER_POWER)
                    {
                        playerList[index].GetComponent<PlayerManagerScript>().SetPowerMet(1);
                    }

                    if (playerList[index].GetComponent<PlayerManagerScript>().GetAchievePoints() >= TRADER_ACHIEVE_POINTS)
                    {
                        playerList[index].GetComponent<PlayerManagerScript>().SetAPMet(1);
                    }

                    if (playerList[index].GetComponent<PlayerManagerScript>().CheckIfMet())
                    {
                        isVictor = true;
                        victorName = playerList[index].GetComponent<PlayerManagerScript>().playerName;
                        break;
                    }
                    
                    break;
                }
                case RAIDER:
                {
                    //Check point values for each player
                    if(playerList[index].GetComponent<PlayerManagerScript>().GetWealth() >= RAIDER_WEALTH)
                    {
                        playerList[index].GetComponent<PlayerManagerScript>().SetWealthMet(1);
                    }

                    if (playerList[index].GetComponent<PlayerManagerScript>().GetPower() >= RAIDER_POWER)
                    {
                        playerList[index].GetComponent<PlayerManagerScript>().SetPowerMet(1);
                    }

                    if (playerList[index].GetComponent<PlayerManagerScript>().GetAchievePoints() >= RAIDER_ACHIEVE_POINTS)
                    {
                        playerList[index].GetComponent<PlayerManagerScript>().SetAPMet(1);
                    }

                    if (playerList[index].GetComponent<PlayerManagerScript>().CheckIfMet())
                    {
                        isVictor = true;
                        victorName = playerList[index].GetComponent<PlayerManagerScript>().playerName;
                        break;
                    }

                    break;
                }
                default:
                {
                    Debug.LogError("Didn't enter Switch statement.");
                    break;
                }
            }
        }
    }

    public void ChooseTurnOrder()
    {
        bool notPicked = true;
        int count = 0;
        int randNum = Random.Range(0, totalPlayers);
        bool canAdd = false;
        //Debug.LogError(totalPlayers.ToString());

        for (int index = 0; index < totalPlayers; index++)
        {
            turnArray[index] = "white";// playerList[index].GetComponent<PlayerManagerScript>().teamColor;
            //Debug.Log("String array: " + turnArray[index]);
        }

        while (notPicked)
        {
            for (int index = 0; index < totalPlayers; index++)
            {
                if (turnArray[index].Contains(playerList[randNum].GetComponent<PlayerManagerScript>().teamColor))
                {
                    canAdd = false;
                    index = 5;
                    randNum = Random.Range(0, totalPlayers);
                }
                else
                {
                    canAdd = true;
                }
            }

            if (canAdd)
            {
                //Debug.LogError("Chosen string: " + playerList[randNum].GetComponent<PlayerManagerScript>().teamColor);
                turnArray[count] = playerList[randNum].GetComponent<PlayerManagerScript>().teamColor;
                //Debug.LogError("Turn " + count.ToString() + ": " + turnArray[count]);
                count++;
                randNum = Random.Range(0, totalPlayers);
            }

            if (count > totalPlayers - 1)
            {
                notPicked = false;
            }
        }
    }

    public void SelectCycleTurn()
    {
        turnCount++;

        if (turnCount > totalPlayers - 1)
        {
            turnCount = 0;
            //Debug.LogError("A full rotation? " + turnCount.ToString() + "totalplayers: " + totalPlayers.ToString());
        }

        for (int index = 0; index < totalPlayers; index++)
        {
            if (turnArray[turnCount] == playerList[index].GetComponent<PlayerManagerScript>().teamColor)
            {
                //Debug.LogError("Turn: " + playerList[index].GetComponent<PlayerManagerScript>().teamColor + " bool: " + playerList[index].GetComponent<PlayerManagerScript>().isTurn);
                //playerList[index].GetComponent<PlayerManagerScript>().isTurn = true;
                playerList[index].GetComponent<PlayerManagerScript>().notDone = true;
                //playerList[index].GetComponent<UnitMovement>().enabled = true;
                //playerList[index].GetComponent<UnitMovement>().canMove = true;
                //Debug.LogError("Turn: " + playerList[index].GetComponent<PlayerManagerScript>().teamColor + " bool: " + playerList[index].GetComponent<PlayerManagerScript>().isTurn);
                //curPlayer = playerList[index].GetComponent<PlayerManagerScript>().teamColor;
                curPlayerRef = playerList[index];
                curPlayer = curPlayerRef.GetComponent<PlayerManagerScript>().teamColor;
                //Debug.Log("Curplayer" + playerList[index].GetComponent<PlayerManagerScript>().teamColor);
            }
        }
    }

    private void DisplayPlayerTurn()
    {
        txtPTurn.text = curPlayer + "'s turn.";//turnArray[turnCount].ToUpper() + "'s turn.";
        txtWealth.text = curPlayerRef.GetComponent<PlayerManagerScript>().GetWealth().ToString();
        txtPower.text = curPlayerRef.GetComponent<PlayerManagerScript>().GetPower().ToString();
        txtAPoints.text = curPlayerRef.GetComponent<PlayerManagerScript>().GetAchievePoints().ToString();
    }

    public GameObject GetCurPlayerRef()
    {
        GameObject tempObject = null;

        for (int index = 0; index < totalPlayers; index++)
        {
            if (curPlayer == playerList[index].GetComponent<PlayerManagerScript>().teamColor)
            {
                tempObject = playerList[index];
                //Debug.Log("PlayerREf: " + tempObject.ToString());
            }
        }

        return tempObject;
    }

    public Grid<GridCombatSystem.GridObject> GetGrid()
    {
        return grid;
    }

    public MovementTilemap GetMovementTilemap()
    {
        return movementTilemap;
    }

    public class EmptyGridObject
    {

        private Grid<EmptyGridObject> grid;
        private int x;
        private int y;

        public EmptyGridObject(Grid<EmptyGridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;

            Vector3 worldPos00 = grid.GetWorldPosition(x, y);
            Vector3 worldPos10 = grid.GetWorldPosition(x + 1, y);
            Vector3 worldPos01 = grid.GetWorldPosition(x, y + 1);
            Vector3 worldPos11 = grid.GetWorldPosition(x + 1, y + 1);

            Debug.DrawLine(worldPos00, worldPos01, Color.white, 999f);
            Debug.DrawLine(worldPos00, worldPos10, Color.white, 999f);
            Debug.DrawLine(worldPos01, worldPos11, Color.white, 999f);
            Debug.DrawLine(worldPos10, worldPos11, Color.white, 999f);
        }

        public override string ToString()
        {
            return "";
        }
    }
}
