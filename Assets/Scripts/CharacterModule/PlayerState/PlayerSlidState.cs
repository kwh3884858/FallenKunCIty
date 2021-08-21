using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlidState : PlayerState
{

    public PlayerSlidState(GameObject obj, PlayerStateManager state) : base(obj, state)
    {
        _stateID = StateID.eStateID_Object_Slid;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter");
    }

    public override void FixedUpdate()
    {

        m_playerMove.SetDirectionalInput(new Vector2(m_playerMove.isfacingRight?1:-1, 0));   //设定左右移动方向

    }

    public override void ProcessTransition()
    {
        if (!m_Player.IsSliding())
        {
            InputSystem.getInstance().StopControl(false);
            m_Animator.ChangeAnimation(AnimatorControl.AnimationType.Run);
            m_StateManager.SetTransition(Transition.eTransiton_Object_Run);

        }
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
