using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _difficulty;
    public int difficulty { get => _difficulty; set => _difficulty = value; }


    // Game manager singleton initialization
    private static GameManager _instance;
    public static GameManager instance => _instance;


    private WaveManager _waveManager;
    public WaveManager waveManager => _waveManager;

    private GameObject _portal;
    public GameObject portal => _portal;

    private GameObject _castle;
    public GameObject castle => _castle;

    private bool castleDetected;
    private bool portalDetected;

    public bool gameStarted;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        castleDetected = false;
        portalDetected = false;
        gameStarted = false;

        InitializeManagers();

        GameEvents.instance.OnObjectiveDetected += CastleDetected;
        GameEvents.instance.OnSpawnDetected += PortalDetected;
    }

    private void Update()
    {
        if (gameStarted)
        {
            _castle = GameObject.Find("castle");
            _portal = GameObject.Find("portal");
        }
    }

    private void CastleDetected()
    {
        castleDetected = true;
        _castle = GameObject.Find("castle");

        if (portalDetected)
        {
            GameEvents.instance.SpawnAndObjectiveDetected();
        }
    }

    private void PortalDetected()
    {
        portalDetected = true;
        _portal = GameObject.Find("portal");

        if (castleDetected)
        {
            GameEvents.instance.SpawnAndObjectiveDetected();
        }
    }

    //private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    if (arg0.name == "Menu")
    //    {
    //        GameManager.instance.audioManager.Play(SoundType.MENU);
    //    }

    //    if (arg0.name == "MainScene")
    //    {
    //        _inGame = true;
    //        InitializeManagers();
    //    }
    //}

    public void InitializeManagers()
    {
        _waveManager = GameObject.FindObjectOfType<WaveManager>();
        //_uIManager = GameObject.FindObjectOfType<UIManager>();
        //_cameraManager = GameObject.FindObjectOfType<CameraManager>();
        //_popUpDialogManager = GameObject.FindObjectOfType<PopUpDialogManager>();
        //_timeManager = GameObject.FindObjectOfType<TimeManager>();
    }
}
