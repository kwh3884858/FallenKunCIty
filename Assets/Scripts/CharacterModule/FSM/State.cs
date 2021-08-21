﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Transition
{
    eTransiton_Null = 0, // Use this transition to represent a non-existing transition in your system

    eTransiton_Object_Idle = 200,
    eTransiton_Object_Run,
    eTransiton_Object_Jump,
	eTransiton_Object_Push,
    eTransition_Object_Slid,
    eTransition_Object_Push,

}
public enum StateID
{
    eStateID_Null = 0, // Use this ID to represent a non-existing State in your system	

    eStateID_Object_Idle = 200,
    eStateID_Object_Run,
    eStateID_Object_Jump,
	eStateID_Object_Slid,
    eStateID_Object_Push,

}


public abstract class State
{
    protected Dictionary<Transition, StateID> _stateMap = new Dictionary<Transition, StateID>();
    protected StateID _stateID;
    public StateID ID
    {
        get { return _stateID; }
    }

	protected State()
    {
    }

    public void AddTransition(Transition trans, StateID id)
    {
        // Check if anyone of the args is invalid
        if (trans == Transition.eTransiton_Null)
        {
            Debug.LogError("State ERROR: eTransiton_Null is not allowed for a real transition");
            return;
        }

        if (id == StateID.eStateID_Null)
        {
            Debug.LogError("State ERROR: eStateID_Null is not allowed for a real ID");
            return;
        }

        // Since this is a Deterministic FSM,
        //   check if the current transition was already inside the map
        if (_stateMap.ContainsKey(trans))
        {
            Debug.LogError("State ERROR: State " + _stateMap.ToString() + " already has transition " + trans.ToString() +
                           "Impossible to assign to another state");
            return;
        }

        _stateMap.Add(trans, id);
    }


    public void DeleteTransition(Transition trans)
    {
        // Check for eTransiton_Null
        if (trans == Transition.eTransiton_Null)
        {
            Debug.LogError("State ERROR: eTransiton_Null is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (_stateMap.ContainsKey(trans))
        {
            _stateMap.Remove(trans);
            return;
        }
        Debug.LogError("State ERROR: Transition " + trans.ToString() + " passed to " + _stateID.ToString() +
                       " was not on the state's transition list");
    }


    public StateID GetOutputState(Transition trans)
    {
        // Check if the map has this transition
        if (_stateMap.ContainsKey(trans))
        {
            return _stateMap[trans];
        }
        return StateID.eStateID_Null;
    }


    public virtual void OnEnter() { }

    public virtual void OnExit() { }

    public abstract void ProcessTransition();

    public abstract void Update();

    public abstract void FixedUpdate();
}