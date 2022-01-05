using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Interaction : MonoBehaviour
{
    public void PlayAnimation()
    {
        Assert.IsNotNull(m_animator);
        m_animator.SetBool("IsTriggered", true);
    }

    public Animator m_animator;
}
