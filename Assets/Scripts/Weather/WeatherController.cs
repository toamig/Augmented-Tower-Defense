using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeatherController : MonoBehaviour
{
    public GameObject lightClouds;
    public GameObject heavyClouds;
    public GameObject lightRain;
    public GameObject heavyRain;
    public ParticleSystem[] rainParticles;

    private WeatherNetwork weatherNetwork;
    public WeatherNetwork.WeatherTypes weather = WeatherNetwork.WeatherTypes.Clear;
    private ParticleSystem.EmissionModule rainEmission;
    private ParticleSystem.MainModule rainColor;

    // Start is called before the first frame update
    void Awake()
    {
        WeatherNetwork.OnWeatherDataReceived += OnWeatherDataBroadcast;
        GameEvents.instance.OnMapDetected += setWeather;
        weatherNetwork = new WeatherNetwork();
        StartCoroutine(weatherNetwork.ProcessWeather());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnWeatherDataBroadcast(WeatherNetwork.WeatherTypes type)
    {
        Debug.Log(type);
        weather = type;
    }

    private void setWeather()
    {
        if (weather == WeatherNetwork.WeatherTypes.Clear)
        {

        }
        else if (weather == WeatherNetwork.WeatherTypes.Clouds)
        {
            lightClouds.SetActive(true);
            heavyClouds.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.Drizzle)
        {
            lightClouds.SetActive(true);
            lightRain.SetActive(true);
            foreach(ParticleSystem ps in rainParticles)
            {
                rainEmission = ps.emission;
                rainEmission.rateOverTime = 5;
            }
        }
        else if (weather == WeatherNetwork.WeatherTypes.FewClouds)
        {
            lightClouds.SetActive(true);
        }
        else if (weather == WeatherNetwork.WeatherTypes.HeavyRain)
        {
            lightClouds.SetActive(true);
            heavyClouds.SetActive(true);
            lightRain.SetActive(true);
            heavyRain.SetActive(true);
            foreach (ParticleSystem ps in rainParticles)
            {
                rainEmission = ps.emission;
                rainEmission.rateOverTime = 50;
            }
        }
        else if (weather == WeatherNetwork.WeatherTypes.LightRain)
        {
            lightClouds.SetActive(true);
            lightRain.SetActive(true);
            foreach (ParticleSystem ps in rainParticles)
            {
                rainEmission = ps.emission;
                rainEmission.rateOverTime = 10;
            }
        }
        else if (weather == WeatherNetwork.WeatherTypes.Rain)
        {
            lightClouds.SetActive(true);
            heavyClouds.SetActive(true);
            lightRain.SetActive(true);
            heavyRain.SetActive(true);
            GameManager.instance.audioManager.Play("Rain");

            foreach (ParticleSystem ps in rainParticles)
            {
                rainEmission = ps.emission;
                rainEmission.rateOverTime = 25;
                rainColor = ps.main;
                rainColor.startColor = Color.blue;
            }
        }
        else if (weather == WeatherNetwork.WeatherTypes.Snow)
        {
            lightClouds.SetActive(true);
            heavyClouds.SetActive(true);
            lightRain.SetActive(true);
            heavyRain.SetActive(true);
            foreach (ParticleSystem ps in rainParticles)
            {
                rainEmission = ps.emission;
                rainEmission.rateOverTime = 5;
                rainColor = ps.main;
                rainColor.startColor = Color.white;
            }
        }
        else if (weather == WeatherNetwork.WeatherTypes.Strom)
        {
            GameManager.instance.audioManager.Play("Wind");
            lightClouds.SetActive(true);
            heavyClouds.SetActive(true);
            lightRain.SetActive(true);
            heavyRain.SetActive(true);
            foreach (ParticleSystem ps in rainParticles)
            {
                rainEmission = ps.emission;
                rainEmission.rateOverTime = 50;
            }
        }
        else
        {

        }
    }
}
