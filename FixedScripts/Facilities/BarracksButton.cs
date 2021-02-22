using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarracksButton : MonoBehaviour
{
    public Button myBarracksButton;

    public void Start()
    {
        myBarracksButton.gameObject.SetActive(false);
    }
}
