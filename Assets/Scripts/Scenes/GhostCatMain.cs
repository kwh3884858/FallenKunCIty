using Skylight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCatMain : BaseScene
{

    public override void SceneShow()
    {
        base.SceneShow();
        UIManager.Instance().ShowPanel<UISubtitilePanel>();
        UIManager.Instance().ShowPanel<GhostCatHUD>();

        StoryManager.Instance().Init();
        StoryManager.Instance().AddAction(StoryManager.ETrackType.Track1, "mainStory.csv");

    }
    public override void SceneClose()
    {
        base.SceneClose();
        UIManager.Instance().ClosePanel<UISubtitilePanel>();
    }
}

