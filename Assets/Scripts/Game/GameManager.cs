using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        GameEvents.instance.OnMapDetected += AssignStructures;

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Update()
    {

    }

    private void AssignStructures()
    {
        _castle = GameObject.Find("castle");
        _portal = GameObject.Find("portal");
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "MainScene")
        {
            gameStarted = false;
            InitializeManagers();
        }

        else if(arg0.name == "StartScene")
        {
            _audioManager = GameObject.FindObjectOfType<AudioManager>();
            audioManager.Play("Theme");
        }


    }

    public void InitializeManagers()
    {
        _waveManager = GameObject.FindObjectOfType<WaveManager>();
        _vuMarkHandler = GameObject.FindObjectOfType<VuMarkHandler>();
        //_uIManager = GameObject.FindObjectOfType<UIManager>();
        //_cameraManager = GameObject.FindObjectOfType<CameraManager>();
        //_popUpDialogManager = GameObject.FindObjectOfType<PopUpDialogManager>();
        //_timeManager = GameObject.FindObjectOfType<TimeManager>();
    }
}
