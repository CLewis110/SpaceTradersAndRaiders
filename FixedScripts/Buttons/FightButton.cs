using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightButton : MonoBehaviour
{
    public Button ftgBtn;
    public GameObject uiHandler;

    // Start is called before the first frame update
    void Start()
    {
        uiHandler = GameObject.Find("UI Handler");
        ftgBtn = GetComponent<Button>();
    }

    private void Awake()
    {
        ftgBtn.gameObject.SetActive(true);
    }

    public void Fight()
    {
        ftgBtn.gameObject.SetActive(false);
        uiHandler.gameObject.SetActive(false);
        SceneManager.LoadScene("FightWindow", LoadSceneMode.Additive);

    }
}