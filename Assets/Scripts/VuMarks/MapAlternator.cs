using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAlternator : MonoBehaviour
{
    public GameObject easyMap;
    public GameObject mediumMap;
    public GameObject hardMap;

    private void Awake()
    {
        EnableDifficultyMap();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableDifficultyMap()
    {
        switch (GameManager.instance.difficulty)
        {
            case 0:
                easyMap.SetActive(true);
                break;
            case 1:
                mediumMap.SetActive(true);
                break;
            case 2:
                hardMap.SetActive(true);
                break;
            default:
                break;
        }
    }
}
