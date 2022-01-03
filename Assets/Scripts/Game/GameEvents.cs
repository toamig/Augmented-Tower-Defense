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

    public event Action OnSpawnDetected;
    public void SpawnDetected()
    {
        OnSpawnDetected?.Invoke();
    }

    public event Action OnObjectiveDetected;
    public void ObjectiveDetected()
    {
        OnObjectiveDetected?.Invoke();
    }

    public event Action OnSpawnAndObjectiveDetected;
    public void SpawnAndObjectiveDetected()
    {
        OnSpawnAndObjectiveDetected?.Invoke();
    }

}
