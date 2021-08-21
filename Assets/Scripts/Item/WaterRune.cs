using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRune : MonoBehaviour
{
	public float m_Time;
	private float m_CurrentTime;
	private bool m_flag = false;

	SpriteRenderer sprite;
	private Transform player;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		sprite = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_flag) {
			if (m_CurrentTime > m_Time) {
				Skylight.DialogPlayer.Load ("WaterBadge");
				GameObject gameObj= GameObject.Find ("StoryTrigger/AttackTip");
                gameObj.SetActive(true);
                gameObj.GetComponent<StoryTrigger> ().Show ();
               

				InputSystem.getInstance ().StopControl (true);
				Destroy (gameObject);
			}
			//sprite.color = Color.Lerp(sprite.color, sprite.color + new Color(0, 0, 0, -1), Time.deltaTime / m_Time);
			transform.position = Vector3.Lerp (transform.position, player.position, Time.deltaTime / m_Time);
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, Time.deltaTime / m_Time);
			m_CurrentTime += Time.deltaTime;
		}
	}
	private void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.transform.parent != null && collision.transform.parent.tag == "Player") {
			Destroy (gameObject, m_Time);
			List<InputSystem.SkillType> skillTypes = new List<InputSystem.SkillType> ();
			skillTypes.Add (InputSystem.SkillType.Water);
			player.GetComponent<SkillController> ().StopSkill (skillTypes);

			m_flag = true;
			collision.transform.parent.GetComponent<SkillController> ().AddSkill (InputSystem.SkillType.Water);
			InputSystem.getInstance ().StopMove (true);
			//TODO之后修改，目前写死
			player.GetComponent<PlayerMove> ().isfacingRight = false;


		}
	}
}
