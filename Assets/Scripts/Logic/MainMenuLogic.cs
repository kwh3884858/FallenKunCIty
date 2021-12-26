using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;

public class MainMenuLogic : LogicBase
{

	public override void LogicStart (int eventId)
	{
		switch (eventId) {
		case (int)SkylightStaticData.LogicType.PanelStart:
			UIManager.Instance ().ShowPanel<GhostCatMainMenu> ();

			break;
		}
	}

}
