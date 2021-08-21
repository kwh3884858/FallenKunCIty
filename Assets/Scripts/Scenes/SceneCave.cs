using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;

public class SceneCave : BaseScene
{
	GameObject m_canvas;
	InputSystem m_inputSystem;

	public override void SceneInit ()
	{
		//throw new System.NotImplementedException ();
		GameObject.Find ("StoryTrigger/DangerousPlants").GetComponent<StoryTrigger> ().Show ();

	}
	public override void SceneShow ()
	{
		//throw new System.NotImplementedException ();

		UIManager.Instance ().ShowPanel<MenuButton> ();
		SoundService.Instance ().PlayMusic ("GameBgm", true);
		m_canvas = GameObject.Find ("SceneCave/Canvas");
		m_inputSystem = GameObject.Find ("SceneCave/InputSystem").GetComponent<InputSystem> ();

		LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.DialogPlayerStart, CloseUIAndStopCharater);
		LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.DialogPlayerCallback, ShowUIAndStartCharacter);
		LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.SettingpanelOpen, CloseUIAndStopCharater);
		LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.SettingPanelClose, ShowUIAndStartCharacter);


	}

	public override void SceneClose ()
	{
		//throw new System.NotImplementedException ();

		SoundService.Instance ().StopMusic ();
	}



	bool CloseUIAndStopCharater (LogicManager.LogicData vars = null)
	{
		m_canvas.SetActive (false);
		m_inputSystem.StopControl (true);
		return true;
	}

	bool ShowUIAndStartCharacter (LogicManager.LogicData vars = null)
	{
		m_canvas.SetActive (true);
		m_inputSystem.StopControl (false);
		return true;
	}

}
