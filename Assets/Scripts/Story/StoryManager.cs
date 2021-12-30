using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Skylight;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public enum EActor
{
    Null,
    Teacher,
    Girl,
}

public enum ELocation
{
    Null,
    Desk,
    Chair,
    Window,
}

public enum EInteraction
{
    Null,
    Paper,
    Window,
    Phone,
}

public class StoryManager : MonoSingleton<StoryManager>
{
    public class StoryTrack
    {
        public StoryTrack()
        {
            m_actionEventList = new Queue<ActionEvent>();
        }

        public void Update()
        {
            foreach (ActionEvent item in m_actionEventList)
            {
                if (!item.IsFinished())
                {
                    item.InternalUpdate();
                }
            }
        }

        public void AddActionEvent(ActionEvent actionEvent)
        {
            if (!m_actionEventList.Contains(actionEvent))
            {
                m_actionEventList.Enqueue(actionEvent);
            }
        }

        public bool IsNeedUpdate()
        {
            bool needUpdate = false;
            foreach (ActionEvent item in m_actionEventList)
            {
                if (!item.IsFinished())
                {
                    needUpdate = true;
                }
            }
            return needUpdate;
        }

        Queue<ActionEvent> m_actionEventList;
    }

    //public override void SingletonInit()
    //{
    //    m_teacher = null;
    //    m_girl = null;
    //    m_desk = null;
    //    m_chair = null;
    //    m_window = null;
    //}

    // Start is called before the first frame update
    public void Init()
    {
        Assert.IsTrue (m_teacher != null);
        Assert.IsTrue (m_girl != null);
        Assert.IsTrue (m_desk != null);
        Assert.IsTrue (m_chair != null);
        Assert.IsTrue (m_window != null);
        Assert.IsTrue(m_windowsAnimator != null);
        Assert.IsTrue(m_paperAnimator != null);

        m_actors = new Dictionary<EActor, GameObject>();
        m_actors.Add(EActor.Teacher, m_teacher);
        m_actors.Add(EActor.Girl, m_girl);

        m_location = new Dictionary<ELocation, GameObject>();
        m_location.Add(ELocation.Desk, m_desk);
        m_location.Add(ELocation.Chair, m_chair);
        m_location.Add(ELocation.Window, m_window);

        m_interactions = new Dictionary<EInteraction, GameObject>();
        m_interactions.Add(EInteraction.Paper, m_paperAnimator);
        m_interactions.Add(EInteraction.Window, m_windowsAnimator);

        m_track1 = new StoryTrack();
        m_track2 = new StoryTrack();
        m_dataIsSeted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_dataIsSeted)
        {
            return;
        }

        if (m_track2.IsNeedUpdate())
        {
            m_track2.Update();
        }
        else
        {
            m_track1.Update ();
        }
    }

    public enum ETrackType
    {
        Track1,
        Track2
    }
    public void AddAction(ETrackType trackType, string CSVName)
    {
        ActionEvent newEvent = gameObject.AddComponent<ActionEvent>();
        newEvent.LoadCSV(CSVName);
        switch (trackType)
        {
            case ETrackType.Track1:
                m_track1.AddActionEvent(newEvent);
                break;
            case ETrackType.Track2:
                m_track2.AddActionEvent(newEvent);
                break;
            default:
                break;
        }
    }
    public GameObject GetActorGameobjectByEActor(EActor actor)
	{
        return m_actors [actor];
	}

    public GameObject GetLocationGameobjectByELocation(ELocation location)
	{
        return m_location [location];
	}

    [SerializeField]
    StoryTrack m_track1;
    [SerializeField]
    StoryTrack m_track2;

    private Dictionary<EActor, GameObject> m_actors;
    private Dictionary<ELocation, GameObject> m_location;
    private Dictionary<EInteraction, GameObject> m_interactions;

    private bool m_dataIsSeted = false;
    [Header ("Actor")]
    public GameObject m_teacher;
    public GameObject m_girl;
    [Header ("Location")]
    public GameObject m_desk;
    public GameObject m_chair;
    public GameObject m_window;
    [Header("Interaction")]
    public GameObject m_paperAnimator;
    public GameObject m_windowsAnimator;
    [Header("Story")]
    public float m_delayPerWord = 0.5f;

}
