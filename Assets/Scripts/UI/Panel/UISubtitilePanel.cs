using System;
using System.Collections;
using System.Collections.Generic;
using Skylight;
using UnityEngine;

public class UISubtitilePanel : UIPanel
{
	// Start is called before the first frame update
	public override void PanelInit ()
	{
		base.PanelInit ();

		m_originalSubtitile = transform.Find ("Content/Subtitile").gameObject;
		StarPlatinum.EventManager.EventManager.Instance.AddEventListener<TalkEvent> (OnTalkEvent);
	}

	private void OnTalkEvent (object sender, TalkEvent e)
	{
		AddSubtitile (e.m_content, e.m_delayTime);
	}

	public override void PanelOpen ()
	{
		base.PanelOpen ();
	}

	public override void PanelClose ()
	{
		base.PanelClose ();
	}

	public void AddSubtitile (string content, float delayTime)
	{
		GameObject subtitile = Instantiate (m_originalSubtitile);
		subtitile.name = content;
		subtitile.transform.SetParent (this.transform);
		SubtitleController controller = subtitile.GetComponent<SubtitleController> ();
		controller.StartSubtitile (content, delayTime);
	}

	GameObject m_originalSubtitile;

}
