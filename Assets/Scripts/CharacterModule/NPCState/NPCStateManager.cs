using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateManager : StateManager
{
    public override void InitStateSystem()
    {
        NPCIdleState idle = new NPCIdleState(this.gameObject, this);
        idle.AddTransition(Transition.eTransition_NPC_Move, StateID.eStateID_NPC_Move);

        NPCMoveState move = new NPCMoveState(this.gameObject, this);
        move.AddTransition(Transition.eTransition_NPC_Idle, StateID.eStateID_NPC_Idle);
        
        m_StateSystem = new StateSystem();
        m_StateSystem.AddState(idle);
        m_StateSystem.AddState(move);
    }
}
