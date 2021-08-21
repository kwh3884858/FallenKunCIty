using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State
{
	public GameObject m_Model;
	public Animation m_Animation;
    public AnimatorControl m_Animator;
	public AudioSource m_AudioSource;
	public PlayerStateManager m_StateManager;
	public Player m_Player;
	public PlayerMove m_playerMove;
	public Controller2D m_controller;
    public static GameObject m_ControlObj;

	public PlayerState (GameObject obj, PlayerStateManager state)
	{
		m_Model = obj;
		//m_Animation = m_Model.transform.Find("Sprite").GetComponent<Animation> ();
        m_Animator = m_Model.transform.Find("Sprite").GetComponent<AnimatorControl>();

		m_AudioSource = m_Model.GetComponent<AudioSource> ();
		m_StateManager = state;
		m_Player = m_Model.GetComponent<Player> ();

		m_playerMove = m_Model.GetComponent<PlayerMove> ();
		m_controller = m_Model.GetComponent<Controller2D> ();

	}
}