using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWater : MonoBehaviour {
    private Vector2 m_Dir;
    public float m_EndTime;
    public float m_ShutTime;
    private float m_CurrentTime;
    public float m_Speed;
    private Animator m_Animator;
    private bool shuting;
    
	// Use this for initialization
	void Start () {

        m_Animator = GetComponent<Animator>();
        shuting = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (m_CurrentTime > m_EndTime + 0.5f)
        {
            Destroy(transform.gameObject);
            return;
        }
        else if (m_CurrentTime > m_EndTime)
        {
            transform.Translate(m_Dir * Time.deltaTime * m_Speed);
            m_Animator.SetBool("Dispear", true);
        }else if (m_CurrentTime > m_ShutTime)
        {
            if (!shuting)
            {
                m_Dir.x = transform.localScale.x * transform.parent.localScale.x;
                m_Dir.Normalize();
                transform.parent = null;
                m_Animator.SetBool("Shut", true);
                shuting = true;
            }

            transform.Translate(m_Dir * Time.deltaTime * m_Speed);
        }
        
        m_CurrentTime += Time.deltaTime;


    }

    public bool SetSkillData(Vector2 dir)
    {

        //if (dir.x < 0)
        //{
        //    Vector3 scale = transform.localScale;
        //    scale.x = -scale.x;
        //    transform.localScale = scale;
        //}
            
        m_Dir = dir.normalized;
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.GetComponent<Flower>().Death();
            Destroy(transform.gameObject);
            
        }
    }
}
