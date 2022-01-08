using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;
using System;

public class GhostCatHUD : UIPanel
{

    public override void PanelInit()
    {
        base.PanelInit();

        LogicManager.Instance().RegisterCallback((int)SkylightStaticData.LogicType.InteractionIconShow, OnInteraction);
        LogicManager.Instance().RegisterCallback((int)SkylightStaticData.LogicType.InteractionIconExit, OnInteractionExit);
        m_icon = transform.Find("InteractionIcon").gameObject;
    }

    private bool OnInteractionExit(LogicManager.LogicData vars)
    {
        m_icon.transform.position = new Vector2(-100, -100);
        return true;
    }

    private bool OnInteraction(LogicManager.LogicData vars)
    {
        m_icon.transform.position = vars.m_uiScreenPosition + new Vector2(0, m_iconOffset);
        return true;
    }

    public float m_iconOffset = 65;
    GameObject m_icon;
}
