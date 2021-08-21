using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;

public class MenuButton : UIPanel
{

	public override void PanelInit ()
	{
		base.PanelInit ();
		AddButtonClick ("Esc", ShowSettingMenu);

		EventTriggerListener.Get (transform.Find ("Esc")).OnPreHover (1.2f);

	}

	void ShowSettingMenu ()
	{
		UIManager.Instance ().ShowPanel<SettingPanel> ();

	}


}
