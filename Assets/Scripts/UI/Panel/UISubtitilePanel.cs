using System;
using System.Collections;
using System.Collections.Generic;
using Skylight;
using UnityEngine;
using UnityEngine.Assertions;

public class UISubtitilePanel : UIPanel
{
	// Start is called before the first frame update
	public override void PanelInit ()
	{
		base.PanelInit ();

        m_subtitileContent = transform.Find("Content").gameObject;
        m_originalSubtitile = transform.Find("Content/Subtitile").gameObject;
        m_originalSubtitile.SetActive(false);

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
        subtitile.SetActive(true);
        subtitile.name = content;
        subtitile.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); //I don`t know why the scale will change after instantiate
        subtitile.transform.SetParent (m_subtitileContent.transform);
		SubtitleController controller = subtitile.GetComponent<SubtitleController> ();
        Assert.IsNotNull(controller, "Check whether the original subtitle object is lack of Component");
		controller.StartSubtitile (content, delayTime);
	}

	GameObject m_subtitileContent;
    GameObject m_originalSubtitile;

}
