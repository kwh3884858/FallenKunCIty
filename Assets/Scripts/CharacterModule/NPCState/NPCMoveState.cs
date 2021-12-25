using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveState : NPCState
{
    public NPCMoveState(GameObject go, NPCStateManager mgr) : base(go, mgr)
    {
        _stateID = StateID.eStateID_NPC_Move;
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
