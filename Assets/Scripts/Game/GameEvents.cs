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

}
