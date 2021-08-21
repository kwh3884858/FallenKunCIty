using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {
    public enum SkillType
    {
        Jump,
        Fire,
        Water,
        Hand,
        Wood
    }
    [SerializeField]
    private bool canMove = true;
    private bool canControl = true;
    
    
    public bool keyBoard=true;
    public Vector2 axis;
    public bool jump;
    public bool fire;
    public bool water;
    public bool hand;
    public bool wood;
    private JoyStick js;
    private SkillController m_skillController;

    static InputSystem inputSystem;
    

    void Start()
    {
        m_skillController = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillController>();
        inputSystem = this;
        js = GameObject.FindObjectOfType<JoyStick>();
        js.OnJoyStickTouchBegin += OnJoyStickBegin;
        js.OnJoyStickTouchMove += OnJoyStickMove;
        js.OnJoyStickTouchEnd += OnJoyStickEnd;
    }

    public static InputSystem getInstance()
    {
        if (!inputSystem.CanControl())
        {
            inputSystem.ResetAll();
        }
        if(!inputSystem.CanMove())
        {
            inputSystem.axis = Vector2.zero;
        }
        return inputSystem;
    }
    void OnJoyStickBegin(Vector2 vec)
    {
        Debug.Log("开始触摸虚拟摇杆");
    }

    void OnJoyStickMove(Vector2 vec)
    {
        axis = vec;
        
    }

    void OnJoyStickEnd()
    {
        Debug.Log("触摸移动摇杆结束");
        axis = Vector2.zero;
    }

	// Update is called once per frame
	void Update () {
        if (keyBoard)
        {
            axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            jump = Input.GetButtonDown("Jump");
            if(m_skillController.GetSkills().Contains(SkillType.Water))
               water = Input.GetButtonDown("Fire1");
            if (Input.GetButtonDown("Fire2"))
            {
                hand =true;
            }
            if (Input.GetButtonUp("Fire2"))
            {
                hand = false;
            }
        }
    }

    private void FixedUpdate()
    {
 //     jump = false;
        water = false;
        fire = false;
        wood = false;
    }

    public bool CanControl()
    {
        return canControl;
    }

    public void StopControl(bool state)
    {
        canControl = !state;
    }

    public bool CanMove()
    {

        return canMove;
    }

    public void StopMove(bool state)
    {
        canMove = !state;
    }

    private void ResetAll()
    {
        jump = false;
        water = false;
        fire = false;
        wood = false;
        axis = Vector2.zero;
        hand = false;
    }

    public void ChangeButton(SkillType type,bool state)
    {
        if (!m_skillController.GetSkills().Contains(type))
            return;
        switch (type)
        {
            case SkillType.Fire:
                fire = state;
                break;
            case SkillType.Hand:
                hand = state;
                break;
            case SkillType.Jump:
                jump = state;
                break;
            case SkillType.Water:
                water = state;
                break;
        }
    }

    
}
