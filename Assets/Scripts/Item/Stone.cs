using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {
    private float m_Speed=1;
    private float m_PicTime = 1;
    private float m_CurrentTime = 0;
    private Vector3 m_FinalPos=Vector3.zero;
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentTime > m_PicTime)
        {
            transform.position = Vector3.Lerp(transform.position, m_FinalPos, Time.deltaTime * m_Speed);
        }
        m_CurrentTime += Time.deltaTime;
        if (Mathf.Abs((transform.position - m_FinalPos).y) < 0.1f)
        {
            Destroy(this);
        }
    }

    public void setData(float speed,float pickTime,Vector3 pos)
    {
        m_PicTime = pickTime;
        m_Speed = speed;
        m_FinalPos = pos;
    }
}
