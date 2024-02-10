using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : GenericMonoSingleton<EventService>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public EventController OnLevelComplete { get; private set; }
    public EventController OnObjectShot { get; private set; }
    public EventController<float> OnPlayerDamaged { get; private set; }
    public EventController<Status> OnApplyStatus { get; private set; }
    public EventController OnPlayerDied { get; private set; }
    public EventController<SuckableObjectStateManager> OnObjectSucked { get; private set; }
    public EventController<bool> OnGamePaused { get; private set; }

    public EventService()
    {
        OnLevelComplete = new EventController();
        OnPlayerDamaged = new EventController<float>();
        OnObjectShot = new EventController();
        OnApplyStatus = new EventController<Status>();
        OnPlayerDied = new EventController();
        OnObjectSucked = new EventController<SuckableObjectStateManager>();
        OnGamePaused = new EventController<bool>();
    }
}
