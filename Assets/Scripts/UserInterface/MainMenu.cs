using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button options;
    public Button back;
    public Button exit;

    public GameObject optionsMenu;
    public GameObject exitScreen;

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

    void SetupButtons()
    {
        // options
        options.onClick.AddListener(() => optionsMenu.SetActive(true));
        options.onClick.AddListener(() => gameObject.SetActive(false));

        // back
        back.onClick.AddListener(() => gameObject.SetActive(false));
        back.onClick.AddListener(() => Time.timeScale = GameManager.instance.timeScale);

        // exit
        exit.onClick.AddListener(() => exitScreen.SetActive(true));
        exit.onClick.AddListener(() => gameObject.SetActive(false));

    }
}
