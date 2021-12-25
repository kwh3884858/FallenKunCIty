using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : StateManager
{
	public override void InitStateSystem ()
	{
		PlayerIdleState idle = new PlayerIdleState(this.gameObject, this);
		idle.AddTransition (Transition.eTransition_Object_Run, StateID.eStateID_Object_Run);
		idle.AddTransition (Transition.eTransition_Object_Jump, StateID.eStateID_Object_Jump);
        idle.AddTransition(Transition.eTransition_Object_Push, StateID.eStateID_Object_Push);

        PlayerRunState run = new PlayerRunState (this.gameObject, this);
		run.AddTransition (Transition.eTransition_Object_Jump, StateID.eStateID_Object_Jump);
		run.AddTransition (Transition.eTransition_Object_Idle, StateID.eStateID_Object_Idle);
        run.AddTransition(Transition.eTransition_Object_Slid, StateID.eStateID_Object_Slid);

        PlayerJumpState jump = new PlayerJumpState (this.gameObject, this);
		jump.AddTransition (Transition.eTransition_Object_Idle, StateID.eStateID_Object_Idle);
		jump.AddTransition (Transition.eTransition_Object_Run, StateID.eStateID_Object_Run);

        PlayerSlidState playerSlidState = new PlayerSlidState(this.gameObject, this);
        playerSlidState.AddTransition(Transition.eTransition_Object_Run, StateID.eStateID_Object_Run);

        PlayerPushState playerPushState = new PlayerPushState(this.gameObject, this);
        playerPushState.AddTransition(Transition.eTransition_Object_Idle, StateID.eStateID_Object_Idle) ;
        playerPushState.AddTransition(Transition.eTransition_Object_Jump, StateID.eStateID_Object_Jump);

		m_StateSystem = new StateSystem ();
		m_StateSystem.AddState (idle);
		m_StateSystem.AddState (run);
		m_StateSystem.AddState (jump);
        m_StateSystem.AddState(playerSlidState);
        m_StateSystem.AddState(playerPushState);
	}
}
