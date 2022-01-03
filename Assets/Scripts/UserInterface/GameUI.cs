using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject gold;
    public GameObject waves;
    public GameObject speedUp;
    public GameObject pauseGame;
    public GameObject startGame;

    private void Awake()
    {
        GameEvents.instance.OnSpawnAndObjectiveDetected += () => startGame.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.instance.gameStarted)
        {
            UpdateHealthBar();
        }

    }

    public void SetupButtons()
    {
        //Start
        startGame.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.gameStarted = true);
        startGame.GetComponent<Button>().onClick.AddListener(() => Debug.Log("TEST"));
        startGame.GetComponent<Button>().onClick.AddListener(() => startGame.SetActive(false));

    }

    private void UpdateHealthBar()
    {
        Castle castle = GameManager.instance.castle.GetComponent<Castle>();

        healthBar.GetComponent<Slider>().value = castle.healthPoints / castle.maxHealthPoints;
        healthBar.GetComponentInChildren<Text>().text = castle.healthPoints + "/" + castle.maxHealthPoints;
    }


}
