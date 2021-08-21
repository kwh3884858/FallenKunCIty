using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour {
    public float m_MoveTime;
    private float m_CurrentTime;
    List<Vector3> pos;
    private int m_NextPosNum;

    // Use this for initialization
    void Start () {
        pos = new List<Vector3>();
        pos.Add(transform.position);
        for(int i = 0; i < transform.childCount; i++)
        {
            pos.Add(transform.GetChild(i).position);
        }
        m_CurrentTime = 0;
        m_NextPosNum = 1;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (m_CurrentTime > m_MoveTime)
        {
            m_NextPosNum += 1;
            m_CurrentTime = 0;
        }
        if (m_NextPosNum >= pos.Count)
            m_NextPosNum = 0;
        transform.position = Vector3.Lerp(transform.position, pos[m_NextPosNum], Time.deltaTime / m_MoveTime);
        m_CurrentTime += Time.deltaTime;
    }
}
