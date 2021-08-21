using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerState
{

    public PlayerPushState(GameObject obj, PlayerStateManager state) : base(obj, state)
    {
        _stateID = StateID.eStateID_Object_Push;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter");
    }
    public override void OnExit()
    {
        Debug.Log("Out");
        m_Player.ChangePushingState(false);
    }

    public override void FixedUpdate()
    {

        m_playerMove.SetDirectionalInput(new Vector2(InputSystem.getInstance().axis.x, 0));   //设定左右移动方向
        if (InputSystem.getInstance().jump == true)
        {
            m_playerMove.OnJumpInputDown();
        }
        if (m_Player.GetBox(m_controller))
        {
            m_Player.PushingBox(InputSystem.getInstance().axis);
        }
    }

    public override void ProcessTransition()
    {
        
        if (m_playerMove.isJumping)
        {
            if (m_playerMove.velocity.y < 0)
            {
                m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpDown);
            }
            else if (m_playerMove.velocity.y > 0)
            {
                m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpUp);
            }
            m_StateManager.SetTransition(Transition.eTransiton_Object_Jump);
        }
        if (InputSystem.getInstance().hand == false)
        {
            m_Animator.ChangeAnimation(AnimatorControl.AnimationType.Idle);
            m_StateManager.SetTransition(Transition.eTransiton_Object_Idle);
        }
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
