using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressEvent : MonoBehaviour, IPointerClickHandler, IPointerDownHandler,IPointerUpHandler
{
    public InputSystem.SkillType skillType;
    private SkillController m_Skill;
    private Image m_Image;
    //private bool flag = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        //InputSystem.getInstance().ChangeButton(skillType);
        if (m_Skill.GetSkills().Contains(skillType) && skillType == InputSystem.SkillType.Hand)
            InputSystem.getInstance().ChangeButton(skillType,true);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(m_Skill.GetSkills().Contains(skillType)&&skillType!=InputSystem.SkillType.Hand)
            InputSystem.getInstance().ChangeButton(skillType,true);
    }
    void Start()
    {
        m_Skill = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillController>();
        m_Image = transform.GetComponent<Image>();
        m_Image.color = Color.white-Color.black;
    }
    void Update()
    {        
        if (m_Skill.GetSkills().Contains(skillType) && m_Image.color != Color.white)
        {
            m_Image.color = Vector4.Lerp(m_Image.color, Color.white, Time.deltaTime/ m_Skill.m_SkillApearSpeed);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_Skill.GetSkills().Contains(skillType) && skillType == InputSystem.SkillType.Hand)
            InputSystem.getInstance().ChangeButton(skillType,false);
    }
}
