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
        GameEvents.instance.OnStartGame += InitializeUI;
        GameEvents.instance.OnSpawnAndObjectiveDetected += () => startGame.SetActive(true);
        GameEvents.instance.OnWaveChange += UpdateWaves;
        GameEvents.instance.OnDamageTaken += UpdateHealthBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeUI()
    {
        Castle castle = GameManager.instance.castle.GetComponent<Castle>();

        // health bar
        healthBar.GetComponentInChildren<Text>().text = castle.healthPoints + "/" + castle.maxHealthPoints;

        // waves
        waves.GetComponentInChildren<Text>().text = "00/" + GameManager.instance.waveManager.waves.Length.ToString("D2");
    }

    public void SetupButtons()
    {
        //Start
        startGame.GetComponent<Button>().onClick.AddListener(() => GameManager.instance.gameStarted = true);
        startGame.GetComponent<Button>().onClick.AddListener(() => GameEvents.instance.StartGame());
        startGame.GetComponent<Button>().onClick.AddListener(() => startGame.SetActive(false));

    }

    private void UpdateHealthBar()
    {
        Castle castle = GameManager.instance.castle.GetComponent<Castle>();

        healthBar.GetComponent<Slider>().value = castle.healthPoints / castle.maxHealthPoints;
        healthBar.GetComponentInChildren<Text>().text = castle.healthPoints + "/" + castle.maxHealthPoints;
    }

    private void UpdateWaves()
    {
        waves.GetComponentInChildren<Text>().text = (GameManager.instance.waveManager.nextWave + 1).ToString("D2") + "/" + GameManager.instance.waveManager.waves.Length.ToString("D2");
    }


}
