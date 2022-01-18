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
    public Toggle speedUp;
    public Button pauseGame;
    public Button startGame;

    public GameObject mainMenu;

    private void Awake()
    {
        GameEvents.instance.OnStartGame += InitializeUI;
        GameEvents.instance.OnMapDetected += () => MapDetected();
        GameEvents.instance.OnMapLost += () => MapLost();
        GameEvents.instance.OnWaveChange += UpdateWaves;
        GameEvents.instance.OnDamageTaken += UpdateHealthBar;
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
        // start
        startGame.onClick.AddListener(() => GameManager.instance.gameStarted = true);
        startGame.onClick.AddListener(() => GameEvents.instance.StartGame());
        startGame.onClick.AddListener(() => startGame.gameObject.SetActive(false));

        // speed up
        speedUp.onValueChanged.AddListener((value) => SpeedUpGame(value));

        // pause
        pauseGame.onClick.AddListener(() => mainMenu.SetActive(true));
        pauseGame.onClick.AddListener(() => GameManager.instance.timeScale = Time.timeScale);
        pauseGame.onClick.AddListener(() => Time.timeScale = 0);


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

    void MapLost()
    {
        if (GameManager.instance.gameStarted)
        {
            mainMenu.SetActive(true);
            if(Time.timeScale != 0)
            {
                GameManager.instance.timeScale = Time.timeScale;
                Time.timeScale = 0;
            }
        }
        else
        {
            startGame.gameObject.SetActive(false);
        }
        
    }

    void MapDetected()
    {
        if (!GameManager.instance.gameStarted)
        {
            startGame.gameObject.SetActive(true);
        }
        
    }

    private void SpeedUpGame(bool value)
    {
        if (value)
        {
            Time.timeScale = 2.5f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


}
