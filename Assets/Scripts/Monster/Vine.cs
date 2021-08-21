using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {
    public float m_AttackTime=1f;

    private float m_wornArea;
    private Animator m_Animator;
    private float m_CurrentTime;
    private Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_wornArea = Mathf.Abs(transform.GetChild(0).position.x - transform.position.x);
        m_Animator =GetComponent<Animator>();
        m_CurrentTime = m_AttackTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_CurrentTime > m_AttackTime)
        {
            m_CurrentTime = 0;
            if(Mathf.Abs(player.position.x - transform.position.x) < m_wornArea)
            {
                m_Animator.SetTrigger("Attack");
            }
        }
        m_CurrentTime += Time.deltaTime;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent == player)
        {
            Debug.Log("die");
            Skylight.UIManager.Instance().ShowPanel<UIDeathPanel>();
            player.transform.position = GameObject.Find("BornPlace").transform.position;
        }
    }

}
