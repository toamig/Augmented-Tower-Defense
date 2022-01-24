using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _difficulty;
    public int difficulty { get => _difficulty; set => _difficulty = value; }

    private float _timeScale;
    public float timeScale
    {
        get => _timeScale;
        set => _timeScale = value;
    }

    // Game manager singleton initialization
    private static GameManager _instance;
    public static GameManager instance => _instance;

    private WaveManager _waveManager;
    public WaveManager waveManager => _waveManager;

    private VuMarkHandler _vuMarkHandler;
    public VuMarkHandler vuMarkHandler => _vuMarkHandler;

    private AudioManager _audioManager;
    public AudioManager audioManager => _audioManager;

    private GameObject _portal;
    public GameObject portal => _portal;

    private GameObject _castle;
    public GameObject castle => _castle;

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

        gameStarted = false;

        InitializeManagers();

        GameEvents.instance.OnMapDetected += AssignStructures;
    }

    private void Update()
    {

    }

    private void AssignStructures()
    {
        _castle = GameObject.Find("castle");
        _portal = GameObject.Find("portal");
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
        _vuMarkHandler = GameObject.FindObjectOfType<VuMarkHandler>();
        _audioManager = GameObject.FindObjectOfType<AudioManager>();
        //_uIManager = GameObject.FindObjectOfType<UIManager>();
        //_cameraManager = GameObject.FindObjectOfType<CameraManager>();
        //_popUpDialogManager = GameObject.FindObjectOfType<PopUpDialogManager>();
        //_timeManager = GameObject.FindObjectOfType<TimeManager>();
    }
}
