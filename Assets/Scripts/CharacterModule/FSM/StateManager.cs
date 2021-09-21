using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    public abstract void InitStateSystem();

    void Awake()
    {
        InitStateSystem();
    }

    public void Update()
    {
        m_StateSystem.CurrentState.ProcessTransition();

        m_currentState = m_StateSystem.CurrentStateID.ToString();
        m_lastState = m_StateSystem.LastStateID.ToString();
    }

    public void LateUpdate()
    {
        m_StateSystem.CurrentState.FixedUpdate();
    }

    public void SetTransition(Transition t)
    {
        m_StateSystem.PerformTransition(t);
    }

    public StateID GetCurrentStateID()
    {
        return m_StateSystem.CurrentStateID;
    }

    public StateID GetLastStateID()
    {
        return m_StateSystem.LastStateID;
    }

    public State GetState(StateID id)
    {
        return m_StateSystem.GetState(id);
    }

    protected StateSystem m_StateSystem;
    public string m_currentState;
    public string m_lastState;
}
