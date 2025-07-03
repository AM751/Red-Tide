using UnityEngine;
using System.Collections;

public class WeatherSystem : MonoBehaviour
{
    public GameObject sunnyWeather;
    public GameObject rainyWeather;

    public float minWeatherDuration = 10f;
    public float maxWeatherDuration = 30f;

    private enum WeatherState { Sunny, Rainy }
    private WeatherState currentWeather;

    void Start()
    {
        // Initialize with random weather
        SetRandomWeather();
        StartCoroutine(ChangeWeatherRoutine());
    }

    IEnumerator ChangeWeatherRoutine()
    {
        while (true)
        {
            // Wait a random amount of time before changing weather
            float duration = Random.Range(minWeatherDuration, maxWeatherDuration);
            yield return new WaitForSeconds(duration);

            // Switch weather
            SetRandomWeather();
        }
    }

    void SetRandomWeather()
    {
        currentWeather = (WeatherState)Random.Range(0, 2);

        switch (currentWeather)
        {
            case WeatherState.Sunny:
                ActivateSunnyWeather();
                break;
            case WeatherState.Rainy:
                ActivateRainyWeather();
                break;
        }
    }

    void ActivateSunnyWeather()
    {
        sunnyWeather.SetActive(true);
        rainyWeather.SetActive(false);
    }

    void ActivateRainyWeather()
    {
        sunnyWeather.SetActive(false);
        rainyWeather.SetActive(true);
    }
}