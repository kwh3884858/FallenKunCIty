using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator))]
public class AnimatorControl : MonoBehaviour
{
	public enum AnimationType
	{
		Idle,       // 默认状态
		Run,       //移动状态
		Attack,     //怪物攻击
		JumpUp,     //跳跃上升阶段
		JumpDown,   //跳跃下降阶段
		JumpLand,       //跳跃着陆阶段
		DoubleJump,     //双重跳
		LayDown,        //放下石头
		HoldStoneIdle,  //抱石站立状态
		HoldStoneMove,  //抱石移动
		HoldStoneJump,  //抱石跳跃
		Sliding,        //滑动
		PushIdle,       //推动站立
		PushMove,       //推动移动
		UsingMagicring,     //直射法阵
		UsingMagicringDonw, //下方法阵
		Death
	}
	Animator m_Animator;
	AnimationType m_CurrentType;
	PlayerMove m_PlayerMove;
	// Use this for initialization
	void Start ()
	{
		m_Animator = GetComponent<Animator> ();
		m_CurrentType = AnimationType.Idle;
		m_PlayerMove = transform.parent.GetComponent<PlayerMove> ();
	}

	// Update is called once per frame
	void Update ()
	{
		m_Animator.SetFloat ("Speed", Mathf.Abs (m_PlayerMove.velocity.x));
	}

	private void ResetAnimation ()
	{
		switch (m_CurrentType) {
		case AnimationType.JumpDown:
                m_Animator.SetBool("JumpDown", false);
                m_Animator.SetBool("CatJump", false);
                break;
		case AnimationType.JumpUp:
				m_Animator.SetBool("JumpUp", false);
                m_Animator.SetBool("CatJump", false);
                break;
        case AnimationType.Sliding:
			m_Animator.SetBool ("Sliding", false);
			break;
		case AnimationType.PushIdle:
			m_Animator.SetBool ("Pushing", false);

			break;

		}
		m_CurrentType = AnimationType.Idle;
	}

	public bool ChangeAnimation (AnimationType type)
	{
		if (type == m_CurrentType)
			return true;
		ResetAnimation ();
		switch (type) {
		case AnimationType.Idle:
			m_Animator.SetBool ("Holding", false);
			m_Animator.SetFloat ("Speed", 0f);
			break;
		case AnimationType.Run:
			m_Animator.SetBool ("Sliding", false);
			m_Animator.SetFloat ("Speed", 1F);
			break;
		case AnimationType.JumpUp:
			m_Animator.SetBool ("JumpUp", true);
                m_Animator.SetBool("CatJump", true);
                break;
		case AnimationType.JumpDown:
			m_Animator.SetBool ("JumpDown", true);
                m_Animator.SetBool("CatJump", true);
                break;
		case AnimationType.JumpLand:
			m_Animator.SetTrigger ("JumpLand");
                m_Animator.SetBool("CatJump", false);
                break;
		case AnimationType.DoubleJump:
			m_Animator.SetTrigger ("DoubleJump");
			break;
		case AnimationType.HoldStoneIdle:
			m_Animator.SetBool ("Holding", true);
                InputSystem.getInstance().StopControl(true);
                Invoke("startControl", 1f);
                Skylight.DialogPlayer.Load ("WoundedSprite");
			break;
		case AnimationType.Sliding:
			m_Animator.SetBool ("Sliding", true);
			break;
		case AnimationType.LayDown:
			m_Animator.SetBool ("Holding", false);
                InputSystem.getInstance().StopControl(true);
                Invoke("startControl", 1f);
                break;
		case AnimationType.UsingMagicringDonw:
			m_Animator.SetTrigger ("UsingMagic");
			break;
		case AnimationType.PushIdle:
			m_Animator.SetBool ("Pushing", true);
			break;
		}
		m_CurrentType = type;
		return true;
	}
	void startControl()
	{
		InputSystem.getInstance ().StopControl (false);
	}
}
