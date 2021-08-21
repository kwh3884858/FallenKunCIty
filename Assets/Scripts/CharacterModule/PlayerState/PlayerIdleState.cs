using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState {

    public PlayerIdleState(GameObject obj, PlayerStateManager state) : base(obj, state)
    {
        _stateID = StateID.eStateID_Object_Idle;
    }

    public override void OnEnter()
    {
    }

    public override void FixedUpdate()
    {
  
        m_playerMove.SetDirectionalInput(new Vector2(InputSystem.getInstance().axis.x, 0));   //设定左右移动方向
        SkillController skillController = m_Player.GetComponent<SkillController>();
        if (skillController.usingMagic == true)
        {
            skillController.usingMagic = false;
            m_Animator.ChangeAnimation(AnimatorControl.AnimationType.UsingMagicringDonw);
        }
        if (InputSystem.getInstance().jump==true)
        {
            m_playerMove.OnJumpInputDown();
        }
        if (InputSystem.getInstance().hand == true)
        {
            if (!m_Player.IsHolding())
            {

                if (m_Player.GetStone(m_controller))
                {
                    InputSystem.getInstance().hand = false;
                    m_Animator.ChangeAnimation(AnimatorControl.AnimationType.HoldStoneIdle);
                }
                if (m_Player.GetBox(m_controller))
                {
                    m_Player.ChangePushingState(true);
                    m_Animator.ChangeAnimation(AnimatorControl.AnimationType.PushIdle);
                }
            }
            else
            {
                InputSystem.getInstance().hand = false;
                m_Player.LayDownStone();
                m_Animator.ChangeAnimation(AnimatorControl.AnimationType.LayDown);
            }
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
            else if(m_playerMove.velocity.y > 0)
            {
                m_Animator.ChangeAnimation(AnimatorControl.AnimationType.JumpUp);
            }
            m_StateManager.SetTransition(Transition.eTransiton_Object_Jump);
        }

        if (Mathf.Abs(InputSystem.getInstance().axis.x) > 0.03f)
        {
            m_StateManager.SetTransition(Transition.eTransiton_Object_Run);
        }
        if (m_Player.IsPushing())
        {
            m_StateManager.SetTransition(Transition.eTransition_Object_Push);
        }
    }

    public override void Update()
    {
    }

    public bool GetStone()
    {
        if (m_Player.IsHolding())
        {
            return false;
        }
        RaycastHit2D[] hits = m_controller.GetHorizontalCollisions(m_Player.interactLayer);
        Transform spriteTransform= m_playerMove.transform.Find("Sprite").transform;
        Transform m=null;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != null)
            {
                GameObject stonePic = Resources.Load<GameObject>("Prefabs/StonePic");
                stonePic = GameObject.Instantiate(stonePic, hit.transform.position+new Vector3(0,0.15f,0), Quaternion.identity);
                stonePic.transform.parent= spriteTransform;
                m = hit.transform;
                break;
            }
        }
        if(m!=null)
            GameObject.Destroy(m.gameObject);

        return true;
    }


}
