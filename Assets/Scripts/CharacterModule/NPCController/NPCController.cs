using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NPCController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Moving
        if (m_currentActionPoint && !m_isMoveing)
        {
            m_isMoveing = true;
            float duration = GetDurationTime(m_currentActionPoint);
            transform.DOMoveX(m_currentActionPoint.transform.position.x, duration).OnComplete(OnMoveComplete);

            //Animation Start

        }
    }

    private void OnMoveComplete()
    {
        m_isMoveing = false;
        m_currentActionPoint = null;

        //Animation End

    }

    private float GetDurationTime(GameObject destination)
    {
        return Mathf.Abs(transform.position.x - destination.transform.position.x) / m_velcoity;
    }

    public float MoveToActionPoint(GameObject actionPoint)
    {
        Assert.IsNull(m_currentActionPoint, "Action Point is only accept one.");
        Assert.IsFalse(m_isMoveing, "The Game object can not be interrupt.");

        if (m_currentActionPoint || m_isMoveing)
        {
            return -1;
        }

        m_currentActionPoint = actionPoint;
        return GetDurationTime(actionPoint);
    }

    [Min(0)]
    public float m_velcoity = 0.5f;
    [Header("Private Values")]
    [SerializeField]
    private bool m_isMoveing = false;
    [SerializeField]
    private GameObject m_currentActionPoint;
}
