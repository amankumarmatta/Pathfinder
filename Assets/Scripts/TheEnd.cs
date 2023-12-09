using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheEnd : MonoBehaviour
{
    public Button home;

    private void Start()
    {
        home.onClick.AddListener(Home);
    }

    void Home() 
    {
        SceneManager.LoadScene("Start");
    }
}
