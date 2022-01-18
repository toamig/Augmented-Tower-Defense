using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle range;
    public Toggle healthBars;
    public Slider ambientSound;
    public Slider sfx;
    public Button back;

    public GameObject mainMenu;

    private void Awake()
    {
        SetupButtons();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeOptions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeOptions()
    {
        range.isOn = true;
        range.isOn = true;
        ambientSound.value = 1;
        sfx.value = 1;
    }

    void SetupButtons()
    {
        // range
        range.onValueChanged.AddListener((value) => DisableRanges(value));

        // health bars
        healthBars.onValueChanged.AddListener((value) => DisableHealthBars(value));

        // back
        back.onClick.AddListener(() => mainMenu.SetActive(true));
        back.onClick.AddListener(() => gameObject.SetActive(false));
    }

    void DisableRanges(bool value)
    {
        if (value)
        {
            GameEvents.instance.EnableRanges();
        }
        else
        {
            GameEvents.instance.DisableRanges();
        }
    }

    void DisableHealthBars(bool value)
    {
        if (value)
        {
            GameEvents.instance.EnableHealthBars();
        }
        else
        {
            GameEvents.instance.DisableHealthBars();
        }
    }
}
