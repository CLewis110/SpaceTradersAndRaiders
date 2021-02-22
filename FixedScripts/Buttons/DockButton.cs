using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DockButton : MonoBehaviour
{
    public Button myDockButton;

    private void Awake()
    {
        //myDockButton.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        myDockButton = GetComponent<Button>();

    }
       

    public void Fight()
    {
        SceneManager.LoadScene("Fight Window", LoadSceneMode.Additive);
        myDockButton.gameObject.SetActive(false);
    }


}
