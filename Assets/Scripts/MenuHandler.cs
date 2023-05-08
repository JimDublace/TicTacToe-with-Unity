using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public Canvas StartMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartMenu.enabled = true;
    }

     public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
