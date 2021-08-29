using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

using Skylight;


public class UIDialogPanel : UIPanel
{
	Text m_content;
	Text m_name;
	Image m_tachie;
	Transform m_offset;
	Image m_background;

	Text m_logContent;

	//button position
	Transform m_skin;
	Transform m_log;
	Transform m_auto;
	Transform m_hide;


	//is auto play dialog
	bool m_isAuto;
	//is hide dialog box
	bool m_isHide;


	//timer for typing effect typeing interval
	private float m_charInterval = 0.1f;
	//whether show all char
	private bool m_isFinishTyping;

	// Use this for initialization
	public override void PanelInit ()
	{
		AddClickEvent ("BelowContent/Content", UpdateContent);
		AddClickEvent ("Background", UpdateContent);
		AddClickEvent ("Tachie", UpdateContent);

		AddClickEvent ("TopRightContent/JumpImg", JumpDialog);
		AddClickEvent ("TopRightContent/Auto", AutoDialog);
		AddClickEvent ("TopRightContent/Hide", HideContent);
		AddClickEvent ("TopRightContent/Log", LogContent);

		AddButtonClick ("Log/Cancel", CancelLogContent);


		m_content = transform.Find ("BelowContent/Content").GetComponent<Text> ();
		m_name = transform.Find ("BelowContent/Name").GetComponent<Text> ();
		m_tachie = transform.Find ("Tachie").GetComponent<Image> ();
		m_offset = m_tachie.GetComponent<RectTransform> ();
		m_background = transform.Find ("Background").GetComponent<Image> ();

		m_logContent = transform.Find ("Log/Content").GetComponent<Text> ();

		m_skin = transform.Find ("TopRightContent/JumpImg");
		m_log = transform.Find ("TopRightContent/Log");
		m_auto = transform.Find ("TopRightContent/Auto");
		m_hide = transform.Find ("TopRightContent/Hide");
		EventTriggerListener.Get (m_skin).OnPreHover (1.2f);
		EventTriggerListener.Get (m_log).OnPreHover (1.2f);
		EventTriggerListener.Get (m_auto).OnPreHover (1.2f);
		EventTriggerListener.Get (m_hide).OnPreHover (1.2f);

		m_isFinishTyping = true;
		m_isAuto = false;
		m_isHide = false;

		InitDialog ();
	}

	public void InitDialog ()
	{

		if (DialogPlayer.IsReading ()) {
			UpdateDialog ();
		}

	}


	void JumpDialog (BaseEventData eventData)
	{
		CloseDialog ();

	}
	void CloseDialog ()
	{


		UIManager.Instance ().ClosePanel<UIDialogPanel> ();
		LogicManager.LogicData logicData = new LogicManager.LogicData ();
		logicData.m_name = DialogPlayer.m_storyName;
		LogicManager.Instance ().Notify (
			(int)SkylightStaticData.LogicType.DialogPlayerCallback, logicData
		);

		//CheckCallback ();
	}

	void AutoDialog (BaseEventData eventData)
	{
		m_isAuto = !m_isAuto;
		if (m_isAuto) {
			m_auto.GetComponent<Image> ().color = new Color (255, 0, 0);
			SoundService.Instance ().PlayEffect ("button02");
			//if content is still in typing;
			if (m_isFinishTyping == false) {
				//show all left text
				m_isFinishTyping = true;
				return;
			}
			if (DialogPlayer.IsReading ()) {

				UpdateDialog ();

			} else {
				CloseDialog ();

			}
		} else {
			m_auto.GetComponent<Image> ().color = new Color (255, 255, 255);
		}
	}

	void LogContent (BaseEventData eventData)
	{
		//recover auto button color
		m_auto.GetComponent<Image> ().color = new Color (255, 255, 255);

		transform.Find ("Log").GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
		//stop auto play
		m_isAuto = false;
		//stop typing
		if (m_isFinishTyping == false) {
			//show all left text
			m_isFinishTyping = true;
			return;
		}

	}

	void CancelLogContent ()
	{
		transform.Find ("Log").GetComponent<RectTransform> ().localPosition = new Vector3 (-880, 0, 0);

	}

	void HideContent (BaseEventData eventData)
	{
		m_isHide = !m_isHide;
		if (m_isHide == true) {

			transform.Find ("BelowContent").GetComponent<RectTransform> ().localPosition = new Vector3 (0, -175, 0);
		} else {

			transform.Find ("BelowContent").GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
		}

	}

	void UpdateContent (BaseEventData eventData)
	{

		SoundService.Instance ().PlayEffect ("button02", false, 0.5f, true);
		//if content is still in typing;
		if (m_isFinishTyping == false) {
			//show all left text
			m_isFinishTyping = true;
			return;
		}
		if (DialogPlayer.IsReading ()) {

			UpdateDialog ();

		} else {
			CloseDialog ();

		}

	}

	void UpdateDialog ()
	{
		StartCoroutine (Typing ());

		m_name.text = DialogPlayer.LoadName ();
		m_tachie.sprite = DialogPlayer.LoadTachie ();
		m_background.sprite = DialogPlayer.LoadBackground ();
		Vector2 offset = DialogPlayer.LoadOffset ();
		m_offset.localPosition = new Vector3 (offset.x, offset.y,
											 m_offset.localPosition.z);

		if (m_isAuto == true) {
			//start auto
			StartCoroutine (Auto (() => {
				//exit function
				if (m_isAuto == false) {
					return;
				}

				Skylight.SoundService.Instance ().PlayEffect ("button02");
				//if content is still in typing;
				if (m_isFinishTyping == false) {
					//show all left text
					m_isFinishTyping = true;
					return;
				}
				if (DialogPlayer.IsReading ()) {

					UpdateDialog ();

				} else {
					CloseDialog ();

				}
			}, DialogPlayer.LoadContent ().Length * m_charInterval + 4f));
		}
	}

	IEnumerator Auto (System.Action action, float delaySeconds)
	{
		yield return new WaitForSeconds (delaySeconds);
		action ();
	}

	IEnumerator Typing ()
	{
		string word = DialogPlayer.LoadContent ();

		m_content.text = "";
		m_isFinishTyping = false;
		SoundService.Instance ().PlayEffect ("TypingSound", true);
		foreach (char letter in word.ToCharArray ()) {
			if (m_isFinishTyping) {
				break;
			}

			m_content.text += letter;

			yield return new WaitForSeconds (m_charInterval);
		}
		SoundService.Instance ().StopEffect ("TypingSound");
		m_isFinishTyping = true;
		m_content.text = word;
		LogContentAddNewText (DialogPlayer.LoadName (), word);
	}

	void LogContentAddNewText (string character, string word)
	{
		m_logContent.text += character + ":" + word + "\n";
	}

	//private bool isSelected = false;


	void CheckCallback ()
	{
		//UIBlackCurtain blackCurtain;


		//switch (DialogPlayer.m_storyName) {
		//case "HanserWakeUpFromBed":


		//	blackCurtain = UIManager.Instance ().ShowUI<UIBlackCurtain> ();

		//	DialogPlayer.Load ("CoffeeHouse");
		//	//UIManager.Instance ().CloseUI ();
		//	SoundService.Instance ().PlayEffect ("strange_wave");
		//	blackCurtain.AddCallback (() => {
		//		UIManager.Instance ().CloseUI ();
		//		SoundService.Instance ().PlayEffect ("hubbub_at_a_cafe");
		//		SoundService.Instance ().StopMusic ();
		//		SoundService.Instance ().AdjustVolume (1f);
		//		SoundService.Instance ().PlayMusic ("hushuu");
		//	});
		//	blackCurtain.PlayFadeIn ();


		//	break;

		//case "CoffeeHouse":

		//	blackCurtain = UIManager.Instance ().ShowUI<UIBlackCurtain> ();

		//	DialogPlayer.Load ("TownRoad");
		//	SoundService.Instance ().StopEffect ("hubbub_at_a_cafe");
		//	SoundService.Instance ().PlayEffect ("strange_wave");

		//	blackCurtain.AddCallback (() => {
		//		UIManager.Instance ().CloseUI ();
		//		//SoundService.Instance().
		//	});
		//	blackCurtain.PlayFadeIn ();


		//	break;

		//case "ClearHouse":

		//	blackCurtain = UIManager.Instance ().ShowUI<UIBlackCurtain> ();

		//	//DialogPlayer.Load ("TownRoad");
		//	//SoundService.Instance ().StopEffect ("hubbub_at_a_cafe.mp3");
		//	SoundService.Instance ().StopMusic ();
		//	SoundService.Instance ().AdjustVolume (0.6f);
		//	SoundService.Instance ().PlayMusic ("kurikaesareru_higeki");

		//	blackCurtain.AddCallback (() => {
		//		UIManager.Instance ().CloseUI ();
		//		//SoundService.Instance().
		//	});
		//	blackCurtain.PlayFadeIn ();
		//	break;

		//case "Diary":
		//	blackCurtain = UIManager.Instance ().ShowUI<UIBlackCurtain> ();

		//	//DialogPlayer.Load ("TownRoad");
		//	//SoundService.Instance ().StopEffect ("hubbub_at_a_cafe.mp3");
		//	SoundService.Instance ().StopMusic ();
		//	SoundService.Instance ().PlayMusic ("kurutta");

		//	blackCurtain.AddCallback (() => {
		//		UIManager.Instance ().CloseUI ();
		//		//SoundService.Instance().
		//	});
		//	blackCurtain.PlayFadeIn ();
		//	break;

		//case "StewardSuccess":
		//	Debug.Log ("StewardSuccess");
		//	if (!isSelected) {

		//		isSelected = !isSelected;
		//		if (GameObject.Find ("Scene") != null) {
		//			m_scene.isRunOrRation = true;//关闭时可以移动

		//			m_scene.NotOnDialogOrTalk = true;//关闭时可以打开背包
		//		}
		//		if (GameObject.Find ("RigidBodyFPSController") != null) {
		//			GameObject.Find ("RigidBodyFPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ().mouseLook.SetCursorLock (false);

		//		}
		//		//Cursor.visible = true;
		//		return;
		//	}

		//	isSelected = !isSelected;
		//	Skyunion.UIManager.Instance ().ClosePanel<UIInvestigateScenePanel> ();
		//	if (GameObject.Find ("RigidBodyFPSController") != null) {
		//		GameObject.Find ("RigidBodyFPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ().mouseLook.SetCursorLock (true);

		//	}
		//	Skyunion.SceneManager.Instance ().CloseScene ();
		//	Skyunion.SceneManager.Instance ().ShowScene<ParkBattleScene> ();
		//	Skyunion.UIManager.Instance ().ShowPanel<UIBattleReadyPanel> ();
		//	Skyunion.GameRoot.Instance ().AddSystemManger ();


		//	break;

		//case "StewardFailure":

		//	if (!isSelected) {

		//		isSelected = !isSelected;

		//		if (GameObject.Find ("Scene") != null) {
		//			m_scene.isRunOrRation = true;//关闭时可以移动

		//			m_scene.NotOnDialogOrTalk = true;//关闭时可以打开背包
		//		}

		//		if (GameObject.Find ("RigidBodyFPSController") != null) {
		//			GameObject.Find ("RigidBodyFPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ().mouseLook.SetCursorLock (false);

		//		}               //Cursor.visible = true;
		//		return;
		//	}

		//	isSelected = !isSelected;
		//	Skyunion.UIManager.Instance ().ClosePanel<UIInvestigateScenePanel> ();
		//	if (GameObject.Find ("RigidBodyFPSController") != null) {
		//		GameObject.Find ("RigidBodyFPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ().mouseLook.SetCursorLock (true);

		//	}

		//	Skyunion.SceneManager.Instance ().CloseScene ();
		//	Skyunion.SceneManager.Instance ().ShowScene<ParkBattleScene> ();
		//	Skyunion.UIManager.Instance ().ShowPanel<UIBattleReadyPanel> ();
		//	Skyunion.GameRoot.Instance ().AddSystemManger ();


		//	break;
		//case "Win":
		//SceneManager.Instance ().CloseScene ();
		//UIManager.Instance ().ShowPanel<UIGameCompletePanel> ();
		//break;


		//}
	}

}
