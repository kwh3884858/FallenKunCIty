using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;

public class UIGameInfoPanel : UIPanel
{



	public override void PanelInit ()
	{
		base.PanelInit ();
		AddButtonClick ("Cancel", Cancel);

	}


	void Cancel ()
	{
		UIManager.Instance ().ClosePanel<UIGameInfoPanel> ();
	}
}
