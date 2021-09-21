using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(GameObject obj, PlayerStateManager state) : base(obj, state)
    {
        _stateID = StateID.eStateID_Object_Run;

    }

    public override void FixedUpdate()
    {
        m_playerMove.SetDirectionalInput(new Vector2(InputSystem.getInstance().axis.x,0));
        if (InputSystem.getInstance().jump)
        {
            m_playerMove.OnJumpInputDown();
        }
    }

    public override void ProcessTransition()
    {
        if (m_Player.IsSliding())
        {
            
            m_Animator.ChangeAnimation(AnimatorControl.AnimationType.Sliding);
            m_StateManager.SetTransition(Transition.eTransition_Object_Slid);
            
        }
        if (Mathf.Abs(m_playerMove.velocity.x) < 0.03f)
        {
            m_StateManager.SetTransition(Transition.eTransition_Object_Idle);
        }
        if (m_playerMove.isJumping)
        {
            if (m_playerMove.velocity.y < 0)
            {
                m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpDown);
            }
            else if(m_playerMove.velocity.y > 0)
            {
                m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpUp);
            }

            m_StateManager.SetTransition(Transition.eTransition_Object_Jump);
        }
    }

    public override void OnEnter()
    {
       
    }

    public override void Update()
    {
    }
}
