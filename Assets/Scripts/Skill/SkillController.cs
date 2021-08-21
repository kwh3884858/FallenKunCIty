using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour {

    public float m_SkillApearSpeed;
    public float m_MagicringEndTime;
    public float m_DownMagicringEndTime;
    public bool usingMagic=false;
    //public float m_WaterBulletShutTime;
    //public float m_WaterBulletEndTime;
    //public float m_WaterBulletSpeed;
    //public float m_WaterShutTime;
    //public float m_WaterEndTime;
    //public float m_WaterSpeed;

    
    private List<InputSystem.SkillType> m_Skills;
    private List<InputSystem.SkillType> m_Record;
    private Controller2D controller;
    private Player player;
    // Use this for initialization
    void Start () {
        m_Skills = new List<InputSystem.SkillType>();
        m_Record = new List<InputSystem.SkillType>();
        m_Skills.Add(InputSystem.SkillType.Hand);
        m_Skills.Add(InputSystem.SkillType.Jump);
        
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller2D>();
        player = controller.GetComponent<Player>();
    }

	// Update is called once per frame
	void Update () {
        if (InputSystem.getInstance().water)
        {
            if (InputSystem.getInstance().axis.y <= -0.9)
            {
                WaterSkill(1);
                InputSystem.getInstance().StopControl(true);
            }else
            {
                if (!player.IsHolding() && !player.IsPushing() && !player.IsSliding())
                {
                    if (!InputSystem.getInstance().CanMove())
                        InputSystem.getInstance().StopMove(false);
                    WaterSkill(0);
                }
            }
        }
    }

    public bool AddSkill(InputSystem.SkillType type)
    {
        if (m_Skills.Contains(type))
            return false;
        m_Skills.Add(type);
        
        return true;
    }

    public List<InputSystem.SkillType> GetSkills()
    {
        return m_Skills;
    }

    public bool DeleteSkill(InputSystem.SkillType type)
    {
        if (!m_Skills.Contains(type))
        {
            return false;
        }
        return m_Skills.Remove(type);
    }

    public void StopSkill(List<InputSystem.SkillType> skills)
    {
        if (m_Record.Count > 0)
            return;
        m_Record.AddRange(m_Skills);
        m_Skills.Clear();
        m_Skills.AddRange(skills);

    }
    public void StartSkill()
    {
        if (m_Record.Count > 0)
        {
            m_Skills.Clear();
            m_Skills.AddRange(m_Record);
            m_Record.Clear();
        }
    }

    public void WaterSkill(int mode)
    {
        Transform spriteTransform = transform.GetComponentInChildren<SpriteRenderer>().transform;
        if (mode == 0)
        {
            GameObject waterSkill1 = Resources.Load<GameObject>("Prefabs/WaterSkill1");
            GameObject magicring = Resources.Load<GameObject>("Prefabs/Magicring");
            Transform shut1 = spriteTransform.Find("MagicShut1");
            Transform magicringPos = spriteTransform.Find("Magicring");
            

            waterSkill1 = Instantiate(waterSkill1, shut1.position, Quaternion.identity, spriteTransform);
            waterSkill1.transform.localScale = waterSkill1.transform.localScale / spriteTransform.localScale.y;
            waterSkill1.GetComponent<SkillWater>().SetSkillData(new Vector2(spriteTransform.localScale.x, 0));

            magicring = Instantiate(magicring, magicringPos.position, Quaternion.identity, spriteTransform);
            magicring.GetComponent<Animator>().speed = magicring.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length / m_MagicringEndTime ;
            Destroy(magicring, m_MagicringEndTime);
        }
        else
        {
            usingMagic = true;
            InputSystem.getInstance().StopControl(true);

            GameObject waterSkill2 = Resources.Load<GameObject>("Prefabs/WaterSkill2");
            GameObject magicring = Resources.Load<GameObject>("Prefabs/MagicringH");
            Transform shut1 = spriteTransform.Find("MagicShut2");


            waterSkill2 = Instantiate(waterSkill2, shut1.position, Quaternion.identity);
            waterSkill2.transform.parent=spriteTransform;
            waterSkill2.transform.localScale = waterSkill2.transform.localScale * Mathf.Sign( spriteTransform.localScale.x);
            waterSkill2.GetComponentInChildren<SkillWater2>().SetSkillData(new Vector2(spriteTransform.localScale.x, 0));

            magicring = Instantiate(magicring, shut1.position, Quaternion.identity);
            magicring.transform.parent = spriteTransform;
            Animator magAnimator = magicring.GetComponentInChildren<Animator>();
            magAnimator.speed = magAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / m_DownMagicringEndTime;
            Destroy(magicring, m_DownMagicringEndTime);
        }
    }
}
