using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

	public float xMargin = 1f;
	public float yMargin = 1f;
	public float xSmooth = 8f;
	public float ySmooth = 8f;
	public Vector2 maxXAndY;
	public Vector2 minXAndY;
	public float yOffset = 3;

	public Transform player;
	void Awake ()
	{

	}
	void Start ()
	{
		player = GameObject.Find ("Character").transform;
	}
	bool CheckXMargin ()
	{
		return Mathf.Abs (transform.position.x - player.position.x) > xMargin;
	}

	bool CheckYMargin ()
	{
		return Mathf.Abs (transform.position.y - player.position.y) > yMargin;
	}

	void FixedUpdate ()
	{

		//Debug.Log (player.transform.position);
		TrackPlayer ();
	}

	void TrackPlayer ()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if (CheckXMargin ()) {
			targetX = Mathf.Lerp (transform.position.x, player.position.x, Time.deltaTime * xSmooth);
		}

		if (CheckYMargin ()) {
			targetY = Mathf.Lerp (transform.position.y, player.position.y + yOffset, Time.deltaTime * ySmooth);
		}

		targetX = Mathf.Clamp (targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp (targetY, minXAndY.y, maxXAndY.y);

		transform.position = new Vector3 (targetX, targetY, transform.position.z);
	}

}