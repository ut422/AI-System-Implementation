using UnityEngine;
using BehaviourMachine;

/// <summary>
///     Base class for all FSM states.
/// </summary>
public abstract class FsmState : StateBehaviour
{
    public enum UpdateMethodEnum { Update, FixedUpdate }


    [field: Header("Debugging")]
    [field: SerializeField]
    public bool PrintStateTransitions { get; private set; }

    [field: ReadOnlyGUI]
    [field: SerializeField]
    public float StateEnterTime { get; private set; } = 0;

    [field: Header("FSM")]
    [field: SerializeField]
    public UpdateMethodEnum UpdateMethod { get; private set; }

    [field: SerializeField]
    [field: HideInInspector]
    public FsmBlackboard Blackboard { get; private set; }


    // METHODS
    protected virtual void Awake()
    {
        // Let state initialize any data it needs
        // NOTE: runs only once
        OnStateAwake();
    }

    public virtual void OnEnable()
    {
        // Record time state was entered
        StateEnterTime = Time.time;

        // Debug some information in the console if wanted
        if (PrintStateTransitions)
            Debug.Log($"{name} entered state '{GetType().Name}' at {StateEnterTime:0.00}");

        // Let state run anything it needs for state runtime
        // NOTE: runs once per state call
        OnStateEnter();
    }

    public virtual void OnDisable()
    {
        // Debug some information in the console if wanted
        if (PrintStateTransitions)
            Debug.Log($"{name} exited state '{GetType().Name}' at {StateEnterTime:0.00}");
    }

    protected virtual void Update()
    {
        // Runs every frame while in state
        if (UpdateMethod == UpdateMethodEnum.Update)
            OnStateUpdate();
    }

    protected virtual void FixedUpdate()
    {
        // Runs every frame while in state
        if (UpdateMethod == UpdateMethodEnum.FixedUpdate)
            OnStateUpdate();
    }

    private new void OnValidate()
    {
        base.OnValidate();

        if (Blackboard == null)
        {
            Blackboard = GetComponent<FsmBlackboard>();
        }
    }

    private new void Reset() => OnValidate();

    /// <summary>
    ///     Runs once when state is entered (as result of a transition).
    /// </summary>
    public abstract void OnStateEnter();

    /// <summary>
    ///     Runs once when state is exited (as result of a transition).
    /// </summary>
    public abstract void OnStateExit();

    /// <summary>
    ///     Runs once when application is started or object is instantiated. Like MonoBehaviour.Awake()
    /// </summary>
    public abstract void OnStateAwake();

    /// <summary>
    ///     Runs every frame (via Update or FixedUpdate).
    /// </summary>
    public abstract void OnStateUpdate();

    /// <summary>
    ///     Request a state transition.
    /// </summary>
    /// <param name="transitionEventName">Transition event name.</param>
    public void SetTransition(string transitionEventName)
    {
        blackboard.SendEventTrigger(transitionEventName);
    }
}
