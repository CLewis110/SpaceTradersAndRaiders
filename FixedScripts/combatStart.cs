using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class combatStart : MonoBehaviour
{
    public GameObject playerManager;
    public GameObject UIHandler;


    private void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        UIHandler = GameObject.Find("UI Handler");

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BaseUnitScript>().teamColor != this.gameObject.GetComponent<BaseUnitScript>().teamColor)
        {
            playerManager.GetComponent<PlayerManagerScript>().isCollidedWithEnemy = true;
            UIHandler.gameObject.GetComponent<ButtonActivationScript>().isFighting = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BaseUnitScript>().teamColor != this.gameObject.GetComponent<BaseUnitScript>().teamColor)
        {
            playerManager.GetComponent<PlayerManagerScript>().isCollidedWithEnemy = false;
            UIHandler.gameObject.GetComponent<ButtonActivationScript>().isFighting = false;
        }
    }
}