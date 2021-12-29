using Skylight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCatMain : BaseScene
{
    public override void SceneInit()
    {
    }

    public override void SceneShow()
    {
        UIManager.Instance().ShowPanel<UISubtitilePanel>();

        StoryManager.Instance().Init();
        StoryManager.Instance().AddAction(StoryManager.ETrackType.Track1, "mainStory.csv");

    }
    public override void SceneClose()
    {
        UIManager.Instance().ClosePanel<UISubtitilePanel>();

    }
}

