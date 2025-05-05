using System;

public static class EventManager
{
    // Otros eventos existentes...

    // Evento para bombas activadas
    public static event Action OnBombsActivated;

    public static void TriggerBombsActivated()
    {
        OnBombsActivated?.Invoke();
    }
}