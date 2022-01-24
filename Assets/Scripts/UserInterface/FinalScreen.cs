using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    public Text text;

    public Button back;
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

    void SetupButtons()
    {

        // back
        back.onClick.AddListener(() => SceneManager.LoadScene("StartScene"));

        // exit
        exit.onClick.AddListener(() => Application.Quit());
    }
}
