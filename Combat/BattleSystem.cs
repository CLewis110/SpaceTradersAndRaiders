using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //Use to change ShipDisplay and AttackDisplay
    public Sprite frigateDisplay;
    public Sprite cruiserDisplay;
    public Sprite battleshipDisplay;
    public Sprite destroyerDisplay;
    public Sprite dreadnaughtDisplay;

    //Ships to instantiate as child of player and enemy gameObjects
    //Change to reference all game objects involved from GameManager Script
    public GameObject playerShip;
    public GameObject enemyShip;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public GameObject pbs;
    public GameObject ebs;

    ShipTypeScript playerUnit;
    ShipTypeScript enemyUnit;

    public Text dialogueText;

    public BattleHud playerHud;
    public BattleHud enemyHud;

    public GameObject attackWindow;

    public int randNum;

    //Weapons
    private bool useLaser = false;
    private bool useMissiles = false;
    public LineRenderer lineRenderer;
    public ParticleSystem playerSparks;
    public ParticleSystem enemySparks;
    public ParticleSystem playerFailSparks;
    public ParticleSystem enemyFailSparks;

    public ParticleSystem playerExplosion;
    public ParticleSystem enemyExplosion;
    public GameObject missile;
    public GameObject antiMissile;
    private GameObject missileToDestroy;

    public CameraShake camShake;


    public BattleState state;

    private void Awake()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
        playerSparks.Stop();
        enemySparks.Stop();
        playerFailSparks.Stop();
        enemyFailSparks.Stop();
    }
    void Start()
    {
        lineRenderer.enabled = false;

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

   
    IEnumerator SetupBattle()
    {
        Collider2D enemyCollider;
        Collider2D playerCollider;
       
        GameObject playerShipID = Instantiate(playerShip, playerBattleStation);
        playerShipID.transform.SetParent(playerPrefab.transform);
        playerShipID.layer = pbs.layer;
        playerCollider = playerShipID.GetComponent<Collider2D>();
        playerCollider.enabled = false;
        playerUnit = playerShipID.GetComponentInChildren<ShipTypeScript>();


        GameObject enemyShipID = Instantiate(enemyShip, enemyBattleStation);
        enemyShipID.transform.SetParent(enemyPrefab.transform);
        enemyShipID.layer = ebs.layer;
        enemyCollider = enemyShipID.GetComponent<Collider2D>();
        enemyCollider.enabled = false;
        enemyUnit = enemyShipID.GetComponentInChildren<ShipTypeScript>();
        

        dialogueText.text = "A enemy " + enemyUnit.shipName + " Attacks...";

        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        Attack();

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHud.SetHP(enemyUnit.hull, enemyUnit.shields);
        dialogueText.text = "Attack successful!";



        yield return new WaitForSeconds(2f);

        lineRenderer.enabled = false;
        enemySparks.Stop();

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerRepair()
    {
        playerUnit.RepairDamage(playerUnit.repair);

        playerHud.SetHP(playerUnit.hull, playerUnit.shields);
        playerFailSparks.Play();
        dialogueText.text = "Repair successful!";
        
        yield return new WaitForSeconds(2f);
                
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn()); 
    }

    IEnumerator PlayerRun()
    {
        dialogueText.text = "Run successful!";

        yield return new WaitForSeconds(2f);


    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(4f);

        dialogueText.text = enemyUnit.shipName + " attacks!";

        yield return new WaitForSeconds(1f);

        int num = RandomGenerate();
        if (num > 1)
        {
            dialogueText.text = "Attack Successful!";
            if(num == 3)
            {
                useLaser = true;
                useMissiles = false;
                Attack();
            }
            else if(num == 4 || num == 5)
            {
                useMissiles = true;
                useLaser = false;
                Attack();
            }
            else
            {
                dialogueText.text = "Repair Successful!";
                enemyFailSparks.Play();
                enemyUnit.RepairDamage(enemyUnit.repair);
            }


            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            playerHud.SetHP(playerUnit.hull, playerUnit.shields);

            yield return new WaitForSeconds(1f);

            lineRenderer.enabled = false;
            playerSparks.Stop();

            if (isDead)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }

        else
        {
            enemyFailSparks.Play();
            dialogueText.text = "Attack unsuccessful...";
            yield return new WaitForSeconds(1f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }




    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!!";
            enemyUnit.Die();
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated!";
            playerUnit.Die();
        }
        yield return new WaitForSeconds(5f);

        SceneManager.UnloadSceneAsync("FightWindow");


    }


    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton()
    {
        useMissiles = false;
        useLaser = false;

        if (state != BattleState.PLAYERTURN)
            return;

        attackWindow.gameObject.SetActive(true);
        dialogueText.text = "Choose a weapon";

    }

    public void SelectLasers()
    {
        useLaser = true;
        useMissiles = false;
        dialogueText.text = "Choose cargo space to attack";
    }

    public void SelectMissiles()
    {
        useMissiles = true;
        useLaser = false;
        dialogueText.text = "Choose cargo space to attack";
    }

    public void OnRepairButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerRepair());
    }

    public void OnRunButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerRun());
    }

    public void AntiMissiles()
    {
        Instantiate(antiMissile, pbs.transform.position, transform.rotation);
        playerUnit.hull += 15;
    }

    public void CargoSpaceAttacked()
    {
        if(useLaser || useMissiles)
        {
            attackWindow.gameObject.SetActive(false);


            int num = RandomGenerate();
            if (num > 1)
            {
                StartCoroutine(PlayerAttack());
            }
            else
            {
                playerFailSparks.Play();
                dialogueText.text = "Attack unsuccessful...";


                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }


    }

    public int RandomGenerate()
    {
       return randNum = Random.Range(1, 7);
    }

    void Attack()
    {


        if(useLaser)
        {
            if(state == BattleState.PLAYERTURN)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, pbs.transform.position);
                lineRenderer.SetPosition(1, ebs.transform.position);
                enemySparks.Play();
                //StartCoroutine(camShake.Shake(1.5f, .2f));

            }
            if(state == BattleState.ENEMYTURN)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, ebs.transform.position);
                lineRenderer.SetPosition(1, pbs.transform.position);
                playerSparks.Play();
                //StartCoroutine(camShake.Shake(1.5f, .2f));
            }

        }
        if(useMissiles)
        {
            if (state == BattleState.PLAYERTURN)
            {
                Instantiate(missile, pbs.transform.position, transform.rotation);
            }
            if (state == BattleState.ENEMYTURN)
            {
                Instantiate(missile, ebs.transform.position, transform.rotation);
            }
        }

    }


}
