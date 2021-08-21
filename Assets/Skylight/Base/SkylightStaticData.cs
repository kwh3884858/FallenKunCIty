using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylightStaticData : MonoBehaviour
{

	public enum LogicType
	{
		SceneInit,
		SceneOpen,
		SceneClose,
		SceneStart,

		PanelInit,
		PanelOpen,
		PanelClose,
		PanelStart,

		MainMenuInit,
		MainMenuOpen,
		MainMenuClose,
		MainMenuStart,

		SettingPanelInit,
		SettingpanelOpen,
		SettingPanelClose,

		DialogPlayerStart,
		DialogPlayerCallback,

	}

	public enum PollerType
	{
		CheeckCharacterEnter,
		CheeckCharacterExit,
	}

}
