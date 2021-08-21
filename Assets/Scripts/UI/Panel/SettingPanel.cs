using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;
public class SettingPanel : UIPanel
{
	public override void PanelInit ()
	{
		base.PanelInit ();
		AddButtonClick ("Return", Return);
		AddButtonClick ("Reload", Reload);
		AddButtonClick ("ReturnMainPanel", ReturnMainPanel);


	}

	public override void PanelOpen ()
	{
		base.PanelOpen ();

		LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.SettingpanelOpen);

	}


	public override void PanelClose ()
	{
		base.PanelClose ();
		LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.SettingPanelClose);

	}

	void Return ()
	{
		UIManager.Instance ().ClosePanel<SettingPanel> ();
	}

	void Reload ()
	{
	}

	void ReturnMainPanel ()
	{
		UIManager.Instance ().ClosePanel<SettingPanel> ();
		UIManager.Instance ().ClosePanel<MenuButton> ();
		SceneManager.Instance ().CloseScene ();
		UIManager.Instance ().ShowPanel<MainMenu> ();
	}
}
