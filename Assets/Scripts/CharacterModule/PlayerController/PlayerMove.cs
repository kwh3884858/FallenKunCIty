using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerMove : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    public float doubleJumpRate = 0.5f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6f;
	[HideInInspector]
	public bool underControl=false;
    [HideInInspector]
    public bool inWater = false;

    public bool isfacingRight = true;
	private bool oldfacing =true;

    public float jumpBufferTime = 0.1f;         //跳跃缓冲时间
    private bool jumpBuffer = false;
    [HideInInspector]
    public bool isJumping = false;

    public bool canDoubleJump;

    private bool isDoubleJumping = false;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    [HideInInspector]
    public Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    private Vector2 directionalInput;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update()
    {

        CalculateVelocity();

		Flip ();
        controller.Move(velocity * Time.deltaTime, directionalInput);
        if (controller.collisions.above || controller.collisions.below)
		{
			velocity.y = 0f;
			if (underControl == false) {
				directionalInput.x = 0f;
			}
		}
        if (IsGround()&&isJumping==true)
        {
            isJumping = false;
        }
    }

    public void ChangeJumpMaxHeight(float jumpMaxHeight)
    {
        gravity = -(2 * jumpMaxHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public bool OnJumpInputDown()
    {
        bool flag = false;
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
            isJumping = true;
            flag = true;
            
        }
        if (canDoubleJump && !controller.collisions.below && !isDoubleJumping)
        {
            velocity.y = maxJumpVelocity*doubleJumpRate;
            isDoubleJumping = true;
            flag = true;
        }
        return flag;
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

	public bool IsGround(){
		return controller.collisions.below;
	}

    public bool CanJump()
    {
        if (jumpBuffer && IsGround())
        {
            OnJumpInputDown();
            CancelInvoke();
            jumpBuffer = false;
            return true;
        }
        else
            return false;
    }

    public void JumpBuffer()
    {
        jumpBuffer = true;
        Invoke("resetJumpBuffer", jumpBufferTime);
    }

    public void Dead()
    {
        underControl = false;
        inWater = true;
    }

    private void resetJumpBuffer()
    {
        jumpBuffer = false;
    }

	private void Flip(){
        Transform spriteTrans = transform.Find("Sprite").transform;

        if (velocity.x > 0.1f) {
			isfacingRight = true;
		} else if (velocity.x < -0.1f) {
			isfacingRight = false;
		}
        if (isfacingRight != oldfacing) {

            spriteTrans.localScale = new Vector3 (-spriteTrans.localScale.x, spriteTrans.localScale.y, spriteTrans.localScale.z);
            //spriteTrans.position = controller.raycastOrigins.center;
			oldfacing = isfacingRight;
		}
	}

    private void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;
    }
}
