using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScreen : MonoBehaviour
{
    public Button yes;
    public Button no;

    public GameObject mainMenu;

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
        // yes
        Application.Quit();

        // no
        no.onClick.AddListener(() => mainMenu.SetActive(true));
        no.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
