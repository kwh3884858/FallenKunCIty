using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState {
    public PlayerJumpState(GameObject obj, PlayerStateManager state) : base(obj, state)
    {
        _stateID = StateID.eStateID_Object_Jump;

    }

    public override void FixedUpdate()
    {

        m_playerMove.SetDirectionalInput(new Vector2(InputSystem.getInstance().axis.x, 0));
        if (InputSystem.getInstance().water)
        {
            m_Animator.ChangeAnimation(AnimatorControl.AnimationType.UsingMagicring);
        }
        if (InputSystem.getInstance().jump)
        {
            if (m_playerMove.OnJumpInputDown() == true && !m_Player.IsHolding())
            {
                m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpUp);
            }
            
        }
        if (m_playerMove.velocity.y < 0)
        {
            m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpDown);
        }
        else 
        {
            m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpUp);
        }

        
    }

    public override void ProcessTransition()
    {
        if (m_playerMove.isJumping == false)
        { 
            if (Mathf.Abs(m_playerMove.velocity.x) < 0.03f)
            {
                m_StateManager.SetTransition(Transition.eTransiton_Object_Idle);
            }
            else
            {
                m_StateManager.SetTransition(Transition.eTransiton_Object_Run);
            }
        }
    }

    public override void Update()
    {
    }
    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
        m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpLand);
    }

}
