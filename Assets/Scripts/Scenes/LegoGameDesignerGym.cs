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

        StoryManager.Instance().Init();
        StoryManager.Instance().AddAction(StoryManager.ETrackType.Track1, "mainStory.csv");

        //StoryManager.Instance().m_teacher = transform.Find("Teacher").gameObject;
        //StoryManager.Instance().m_girl = transform.Find("Girl").gameObject;
        //StoryManager.Instance().m_desk = transform.Find("Desk").gameObject;
        //StoryManager.Instance().m_chair = transform.Find("Chair").gameObject;
        //StoryManager.Instance().m_window = transform.Find("Window").gameObject;
        //StoryManager.Instance().Initialize();
    }

	public override void SceneClose()
	{
		UIManager.Instance ().ClosePanel<UISubtitilePanel> ();

	}

}
