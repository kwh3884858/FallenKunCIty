using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;
using Config.GameRoot;

public class MainMenu : UIPanel
{
	public override void PanelInit ()
	{
		base.PanelInit ();
		AddButtonClick ("GameStart", GameStart);

		AddButtonClick ("About", GameAbout);
		AddButtonClick ("Quit", GameQuit);
		LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.MainMenuInit);
		EventTriggerListener.Get (transform.Find ("GameStart")).OnPreHover (1.2f);
		EventTriggerListener.Get (transform.Find ("Setting")).OnPreHover (1.2f);
		EventTriggerListener.Get (transform.Find ("About")).OnPreHover (1.2f);
		EventTriggerListener.Get (transform.Find ("Quit")).OnPreHover (1.2f);
	}

	public override void PanelOpen ()
	{
		base.PanelOpen ();
		LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.MainMenuOpen);
	}

	public override void PanelClose ()
	{
		base.PanelClose ();
		LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.MainMenuClose);
	}


	void GameStart ()
	{
		UIManager.Instance ().ClosePanel<MainMenu> ();
		SoundService.Instance ().StopMusic ();

		SoundService.Instance ().PlayMusic ("GameBgm", true);
		SoundService.Instance ().PlayEffect ("WaterDrop", true, 0.3f);
		SceneLookupEnum startScene = ConfigRoot.Instance.StartScene;
        switch (startScene)
        {
            case SceneLookupEnum.GameRoot:
                break;
            case SceneLookupEnum.LegoGameDesignerGym:
				SceneManager.Instance().ShowScene<LegoGameDesignerGym>();
				break;
            case SceneLookupEnum.LegoGhostCat:
                break;
            case SceneLookupEnum.SampleScene:
                break;
            case SceneLookupEnum.SceneCave:
				SceneManager.Instance().ShowScene<SceneCave>();
				break;
            default:
                break;
        }

	}

	void GameSetting ()
	{

	}

	void GameAbout ()
	{
		UIManager.Instance ().ShowPanel<UIGameInfoPanel> ();
	}

	void GameQuit ()
	{
		Application.Quit ();

	}
}
