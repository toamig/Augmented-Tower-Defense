using System;

public class GameEvents
{
    private static GameEvents _instance;
    public static GameEvents instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEvents();
            }
            return _instance;
        }
    }

    private GameEvents()
    {

    }

    public event Action OnMapDetected;
    public void MapDetected()
    {
        OnMapDetected?.Invoke();
    }

    public event Action OnMapLost;
    public void MapLost()
    {
        OnMapLost?.Invoke();
    }

    public event Action OnWaveChange;
    public void WaveChange()
    {
        OnWaveChange?.Invoke();
    }

    public event Action OnDamageTaken;
    public void DamageTaken()
    {
        OnDamageTaken?.Invoke();
    }

    public event Action OnStartGame;
    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public event Action OnVictory;
    public void Victory()
    {
        OnVictory?.Invoke();
    }

    public event Action OnDefeat;
    public void Defeat()
    {
        OnDefeat?.Invoke();
    }

    public event Action OnDisableRanges;
    public void DisableRanges()
    {
        OnDisableRanges?.Invoke();
    }

    public event Action OnEnableRanges;
    public void EnableRanges()
    {
        OnEnableRanges?.Invoke();
    }

    public event Action OnDisableHealthBars;
    public void DisableHealthBars()
    {
        OnDisableHealthBars?.Invoke();
    }

    public event Action OnEnableHealthBars;
    public void EnableHealthBars()
    {
        OnEnableHealthBars?.Invoke();
    }

    public event Action<float> OnVFXVolumeChanged;
    public void VFXVolumeChanged(float volume)
    {
        OnVFXVolumeChanged?.Invoke(volume);
    }

    public event Action<float> OnBackgroundVolumeChanged;
    public void BackgroundVolumeChanged(float volume)
    {
        OnBackgroundVolumeChanged?.Invoke(volume);
    }
}
