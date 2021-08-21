using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;

public class UIDeathPanel : UIPanel
{

	public override void PanelInit ()
	{
		base.PanelInit ();
		AddButtonClick ("Retry", Retry);
		AddButtonClick ("ReturnMainMenu", ReturnMainMenu);
	}

	void Retry ()
	{
        UIManager.Instance().ClosePanel<UIDeathPanel>();


	}

	void ReturnMainMenu ()
	{
		UIManager.Instance ().ClosePanel<UIDeathPanel> ();
		UIManager.Instance ().ClosePanel<MenuButton> ();
		SceneManager.Instance ().CloseScene ();
		UIManager.Instance ().ShowPanel<MainMenu> ();
	}
}
