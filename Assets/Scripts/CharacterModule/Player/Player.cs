using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour {

	public static Player MainPlayer = null;

	private Animation m_anim;
	private PlayerStateManager m_StateManager;
    private Transform targetMark;
    private bool isHolding;
    [SerializeField]
    private bool isSliding;
    private bool isPushing;
    private GameObject m_Box;

	public LayerMask interactLayer;     //可交互图层，比如箱子的图层
	public LayerMask ctrLayer;      //可进行灵魂控制的物体的图层
    public Vector3 markOffset;
    public float m_HoldingSpeedRate;
    public float m_HoldingJumpRate;
    public float m_SlidingSpeedRate;
    public float m_PushingSpeedRate;
    
    public float m_StoneSpeed;
    public float m_PickTime;

    private PlayerMove m_playerMove;


    [HideInInspector]
	public GameObject m_AttackTarget = null;

    
	public AudioClip m_RunAudioClip;
	public AudioClip m_JumpUpAudioClip;
	public AudioClip m_JumpDownAudioClip;
	public AudioClip m_SoulSelectAudioClip;
	public AudioClip m_SoulControlAudioClip;
	public AudioClip m_SoulReturnAudioClip;
	public AudioClip m_DeadAudioClip;

	private void Awake()
	{
		MainPlayer = this;
        //targetMark = transform.Find("TargetMark");
        //targetMark.GetComponent<SpriteRenderer>().enabled = false;
        m_playerMove = GetComponent<PlayerMove>();
        m_Box = null;
    }

	// Use this for initialization
	void Start () {
		m_anim = this.GetComponent<Animation> ();
		m_StateManager = this.GetComponent<PlayerStateManager> ();

	}

	// Update is called once per frame
	void Update ()
	{

	}

    public bool IsHolding()
    {
        return isHolding;
    }

    public bool IsSliding()
    {
        return isSliding;
    }

    public bool IsPushing()
    {
        return isPushing;
    }

    public bool ChangePushingState(bool state)
    {
        if (state == isPushing)
            return false;
        if (state == true)
        {
            m_playerMove.moveSpeed *= m_PushingSpeedRate;
        }
        else
        {
            m_playerMove.moveSpeed /= m_PushingSpeedRate;
        }
        isPushing = state;
        return true;
    }

    public bool ChangeHoldingState(bool state)
    {
        if (state == isHolding)
            return false;
        if(state == true)
        {
            m_playerMove.moveSpeed *= m_HoldingSpeedRate;
            m_playerMove.ChangeJumpMaxHeight( m_playerMove.maxJumpHeight * m_HoldingJumpRate);
        }
        else
        {
            m_playerMove.moveSpeed /= m_HoldingSpeedRate;
            m_playerMove.ChangeJumpMaxHeight(m_playerMove.maxJumpHeight );

        }
        isHolding = state;
        return true;
    }

    public bool ChangeSlidingState(bool state)
    {
        if (state == isSliding)
            return false;
        if (state == true)
        {
            InputSystem.getInstance().StopControl(true);
            m_playerMove.moveSpeed *= m_SlidingSpeedRate;
        }
        else
        {
            m_playerMove.moveSpeed /= m_SlidingSpeedRate;
        }
        isSliding = state;
        return true;
    }
    

    public bool GetStone(Controller2D collider2D) {
        if (IsHolding())
        {
            return false;
        }
        RaycastHit2D[] hits = collider2D.GetHorizontalCollisions(interactLayer);
        Transform spriteTransform = transform.Find("Sprite").transform;
        Transform m = null;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != null&&hit.transform.tag=="Stone")
            {
                GameObject stonePic = Resources.Load<GameObject>("Prefabs/Item/" + hit.transform.name+"Pic");
                Transform stoneOffset = spriteTransform.Find("StoneOffset");
                stonePic = Instantiate(stonePic, hit.transform.position, Quaternion.identity);
                stonePic.GetComponent<Stone>().setData(m_StoneSpeed,m_PickTime, hit.transform.position + new Vector3(stoneOffset.position.x - hit.transform.position.x, stoneOffset.position.y - hit.transform.position.y, 0));
                stonePic.transform.parent = spriteTransform;
                stonePic.name = hit.transform.name;
                m = hit.transform;
                break;
            }
        }
        if (m != null)
        {
            ChangeHoldingState(true);
            Destroy(m.gameObject);
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool GetBox(Controller2D collider2D)
    {
        if (collider2D == null)
            return false;
        RaycastHit2D[] hits = collider2D.GetHorizontalCollisions(interactLayer);   //获取水平碰撞体
        foreach (RaycastHit2D hit in hits)
        {
            if (!hit)
            {

                continue;
            }
            if (hit.transform.tag == "Box")
            {
                if (m_Box == null)
                    m_Box = hit.transform.gameObject;
                else if (m_Box != hit.transform.gameObject)
                    m_Box = hit.transform.gameObject;
                return true;
            }
        }
        m_Box = null;
        return false;
    }

    public bool PushingBox(Vector2 dir)
    {
        if (!isPushing || m_Box == null)
            return false;
        m_Box.GetComponent<PlayerMove>().SetDirectionalInput(dir * m_PushingSpeedRate);
        return true;

    }

    public bool LayDownStone()
    {
        if (!isHolding)
            return false;
        Transform spriteTransform = transform.Find("Sprite").transform;
        Transform stonePic = spriteTransform.GetChild(spriteTransform.childCount-1);
        
        if (stonePic == null||spriteTransform==null)
            return false;
        Vector3 stonePos = stonePic.position;
        Destroy(stonePic.gameObject);
        stonePic.GetComponent<Collider2D>().isTrigger = true;
        GameObject stone = Resources.Load<GameObject>("Prefabs/Item/" + stonePic.name);
        stone = Instantiate(stone, stonePos, Quaternion.identity);
        stone.name = stonePic.name;
        ChangeHoldingState(false);
        return true;
    }
	public void PlayerAnimation (string clip)
	{
		if (m_anim != null)
			m_anim.CrossFade (clip);
	}


    public void setSoulTarget(Collider2D[] cols,int currentNo)
    {
        targetMark.GetComponent<SpriteRenderer>().enabled = true;
        Vector3 targetPos = new Vector3(cols[currentNo].transform.position.x, cols[currentNo].GetComponent<Controller2D>().raycastOrigins.topLeft.y);
        targetMark.position = targetPos +markOffset ;
    }

    public void ResetSoulTarget()
    {
        targetMark.GetComponent<SpriteRenderer>().enabled = false;
        targetMark.position = transform.position+markOffset;
    }

}
