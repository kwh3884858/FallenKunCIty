using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
    
    public float m_DeathTime = 1;
    private float m_CountTime = 0;
    private float m_Worningridus;
    private GameObject m_Player;
    private Animator m_Animator;
    private bool die;
    // Use this for initialization
	void Start () {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Worningridus = Mathf.Abs(transform.GetChild(0).position.x - transform.position.x);
        m_Animator = GetComponent<Animator>();
        die = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs((m_Player.transform.position - transform.position).magnitude) < m_Worningridus)
        {
            m_Animator.SetBool("worn",true);
        }
        else
        {
            m_Animator.SetBool("worn", false);
        }
        if (die)
        {
            if (m_CountTime > m_DeathTime)
            {
                Destroy(gameObject);
            }
            m_CountTime += Time.deltaTime;
        }
	}

    public void Death()
    {
        die = true;
        m_Animator.SetTrigger("Die");
        m_Animator.speed = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / m_DeathTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.tag == "Player")
        {
            //游戏结束
            Skylight.UIManager.Instance().ShowPanel<UIDeathPanel>();
            m_Player.transform.position = GameObject.Find("BornPlace").transform.position;
        }
    }
}
