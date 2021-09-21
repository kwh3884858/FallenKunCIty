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
        if (m_currentActionPoint && !m_isMoveing)
        {
            m_isMoveing = true;
            float duration = (transform.position.x - m_currentActionPoint.transform.position.x) / m_velcoity;
            transform.DOLocalMoveX(m_currentActionPoint.transform.position.x, duration).OnComplete(OnMoveComplete);
        }
    }

    private void OnMoveComplete()
    {
        m_isMoveing = false;
        m_currentActionPoint = null;
    }

    public void MoveToActionPoint(GameObject actionPoint)
    {
        Assert.IsNull(m_currentActionPoint, "Action Point is only accept one.");
        Assert.IsFalse(m_isMoveing, "The Game object can not be interrupt.");

        if (m_currentActionPoint || m_isMoveing == false)
        {
            return;
        }

        m_currentActionPoint = actionPoint;
    }

    [Min(0)]
    public float m_velcoity;
    [Header("Private Values")]
    [SerializeField]
    private bool m_isMoveing = false;
    [SerializeField]
    private GameObject m_currentActionPoint;
}
