using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState : State
{
    public NPCState(GameObject go, NPCStateManager state)
    {
        m_Model = go;
        m_StateManager = state;
    }
    public GameObject m_Model;
    public NPCStateManager m_StateManager;
}
