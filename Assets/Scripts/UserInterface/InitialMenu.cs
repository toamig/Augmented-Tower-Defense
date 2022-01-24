using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialMenu : MonoBehaviour
{
    public Dropdown difficulty;
    public Button start;
    public Button exit;

    private void Awake()
    {
        SetupButtons();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupButtons()
    {
        start.onClick.AddListener(PlayGame);
        exit.onClick.AddListener(Application.Quit);
    }

    public void PlayGame()
    {
        GameManager.instance.difficulty = difficulty.value;
        SceneManager.LoadScene("MainScene");
    }


}
