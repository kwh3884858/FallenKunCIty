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

public class StoryManager : MonoSingleton<StoryManager>
{
    public class ActionEvent : MonoBehaviour
    {

    }

    public class StoryTrack
    {
        public void Update()
        {
            foreach (ActionEvent item in m_actionEventList)
            {
                if (!item.IsFinished())
                {
                    item.Update();
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

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsTrue (m_teacher != null);
        Assert.IsTrue (m_girl != null);
        Assert.IsTrue (m_desk != null);
        Assert.IsTrue (m_chair != null);
        Assert.IsTrue (m_window != null);

        m_actors.Add(EActor.Teacher, m_teacher);
        m_actors.Add(EActor.Girl, m_girl);

        m_location.Add(ELocation.Desk, m_desk);
        m_location.Add(ELocation.Chair, m_chair);
        m_location.Add(ELocation.Window, m_window);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_track2.IsNeedUpdate())
        {
            m_track2.Update();
        }
        else
        {
            m_track1.Update ();
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

    StoryTrack m_track1;
    StoryTrack m_track2;

    private Dictionary<EActor, GameObject> m_actors;
    private Dictionary<ELocation, GameObject> m_location;

    [Header ("Actor")]
    public GameObject m_teacher;
    public GameObject m_girl;
    [Header ("Location")]
    public GameObject m_desk;
    public GameObject m_chair;
    public GameObject m_window;
}
