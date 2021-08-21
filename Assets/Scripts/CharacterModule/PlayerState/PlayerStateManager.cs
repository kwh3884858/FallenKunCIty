using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {

	private StateSystem m_StateSystem;

	void Awake ()
	{
		InitStateSystem ();
	}
	// Use this for initialization
	void Start ()
	{
		Init ();
	}
	private void Init ()
	{
//		this.GetComponent<Animation> ().Play ("idle");
	}
	// Update is called once per frame
	public void Update ()
	{
		m_StateSystem.CurrentState.ProcessTransition ();
	}

	public void FixedUpdate ()
	{
		m_StateSystem.CurrentState.FixedUpdate ();
	}
	public void InitStateSystem ()
	{
		PlayerIdleState idle = new PlayerIdleState(this.gameObject, this);
		idle.AddTransition (Transition.eTransiton_Object_Run, StateID.eStateID_Object_Run);
		idle.AddTransition (Transition.eTransiton_Object_Jump, StateID.eStateID_Object_Jump);
        idle.AddTransition(Transition.eTransition_Object_Push, StateID.eStateID_Object_Push);
        //idle.AddTransition (Transition.eTransiton_Object_Push, StateID.eStateID_Object_Push);
        //      idle.AddTransition(Transition.eTransiton_Object_SoulSelect, StateID.eStateID_Object_SoulSelect);
        //      idle.AddTransition(Transition.eTransiton_Object_CashStone, StateID.eStateID_Object_CashStone);

        PlayerRunState run = new PlayerRunState (this.gameObject, this);
		run.AddTransition (Transition.eTransiton_Object_Jump, StateID.eStateID_Object_Jump);
		run.AddTransition (Transition.eTransiton_Object_Idle, StateID.eStateID_Object_Idle);
        run.AddTransition(Transition.eTransition_Object_Slid, StateID.eStateID_Object_Slid);
        //run.AddTransition (Transition.eTransiton_Object_Push, StateID.eStateID_Object_Push);
        //      run.AddTransition(Transition.eTransiton_Object_SoulSelect, StateID.eStateID_Object_SoulSelect);

        PlayerJumpState jump = new PlayerJumpState (this.gameObject, this);
		jump.AddTransition (Transition.eTransiton_Object_Idle, StateID.eStateID_Object_Idle);
		jump.AddTransition (Transition.eTransiton_Object_Run, StateID.eStateID_Object_Run);
        //jump.AddTransition(Transition.eTransiton_Object_SoulSelect, StateID.eStateID_Object_SoulSelect);

        PlayerSlidState playerSlidState = new PlayerSlidState(this.gameObject, this);
        playerSlidState.AddTransition(Transition.eTransiton_Object_Run, StateID.eStateID_Object_Run);

        PlayerPushState playerPushState = new PlayerPushState(this.gameObject, this);
        playerPushState.AddTransition(Transition.eTransiton_Object_Idle, StateID.eStateID_Object_Idle) ;
        playerPushState.AddTransition(Transition.eTransiton_Object_Jump, StateID.eStateID_Object_Jump);
  //      PushState push = new PushState (this.gameObject, this);
		//push.AddTransition (Transition.eTransiton_Object_Idle, StateID.eStateID_Object_Idle);
		//push.AddTransition (Transition.eTransiton_Object_Run, StateID.eStateID_Object_Run);

		//SoulSelectState soulSelect = new SoulSelectState (this.gameObject, this);
		//soulSelect.AddTransition (Transition.eTransiton_Object_Idle, StateID.eStateID_Object_Idle);
		//soulSelect.AddTransition (Transition.eTransiton_Object_SoulOut, StateID.eStateID_Object_SoulOut);       //2.状态设置错误，改为soulout

		//SoulOutState soulOut = new SoulOutState (this.gameObject, this);
		//soulOut.AddTransition (Transition.eTransiton_Object_Idle, StateID.eStateID_Object_Idle);

  //      CashStoneState cashStone = new CashStoneState(this.gameObject, this);
  //      cashStone.AddTransition(Transition.eTransiton_Object_Idle, StateID.eStateID_Object_Idle);

		m_StateSystem = new StateSystem ();
		m_StateSystem.AddState (idle);
		m_StateSystem.AddState (run);
		m_StateSystem.AddState (jump);
        m_StateSystem.AddState(playerSlidState);
        m_StateSystem.AddState(playerPushState);
		//m_StateSystem.AddState (push);
		//m_StateSystem.AddState (soulSelect);
		//m_StateSystem.AddState (soulOut);
  //      m_StateSystem.AddState(cashStone);

	}
	public void SetTransition (Transition t)
	{
		m_StateSystem.PerformTransition (t);
	}

	public StateID GetCurrentStateID ()
	{
		return m_StateSystem.CurrentStateID;
	}

	public StateID GetLastStateID ()
	{
		return m_StateSystem.LastStateID;
	}

	public State GetState (StateID id)
	{
		return m_StateSystem.GetState (id);
	}
}
