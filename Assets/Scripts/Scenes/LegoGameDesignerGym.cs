using Skylight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoGameDesignerGym : BaseScene
{
	public override void SceneInit()
	{
	}

	public override void SceneShow()
	{
		UIManager.Instance ().ShowPanel<UISubtitilePanel> ();

	}

	public override void SceneClose()
	{
		UIManager.Instance ().ClosePanel<UISubtitilePanel> ();

	}

}
