using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWater2 : MonoBehaviour {
    private Vector2 m_Dir;
    public float m_EndTime;             //技能结束时间
    public float m_ShutTime;            //动画播放事件
    public float m_AnimationEndTime;    //动画结束时间
    private float m_CurrentTime;        
    private Animator m_Animator;
    private bool shuting;

    // Use this for initialization
    void Start()
    {

        m_Animator = GetComponent<Animator>();
        shuting = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_CurrentTime > m_EndTime )
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        else if (m_CurrentTime > m_ShutTime)
        {
            if (!shuting)
            {
                InputSystem.getInstance().StopControl(false);
                m_Animator.SetTrigger("Extend");
                GetComponent<Animator>().speed = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length / (m_AnimationEndTime-m_ShutTime);
                m_Dir.Normalize();
                transform.parent.parent = null;
                shuting = true;
            }

        }

        m_CurrentTime += Time.deltaTime;


    }

    public bool SetSkillData(Vector2 dir)
    {
        m_Dir = dir.normalized;
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent == null)
            return;
        if (collision.transform.parent.tag == "Player")
        {
            collision.transform.parent.GetComponent<Player>().ChangeSlidingState(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent == null)
            return;
        if (collision.transform.parent.tag == "Player")
        {
            collision.transform.parent.GetComponent<Player>().ChangeSlidingState(false);
        }
    }

    private void PlayerOut()
    {
        transform.parent.parent.parent.GetComponent<Player>().ChangeSlidingState(false);
    }
}
