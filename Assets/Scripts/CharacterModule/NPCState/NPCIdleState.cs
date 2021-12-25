using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCState
{
    public NPCIdleState(GameObject go, NPCStateManager mgr):base(go,mgr)
    {
        _stateID = StateID.eStateID_NPC_Idle;
    }

    public override void OnEnter()
    {
    }

    public override void FixedUpdate()
    {

    }

    public override void ProcessTransition()
    {
    }

    public override void Update()
    {
    }

}
