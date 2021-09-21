using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class StoryManager : MonoBehaviour
{
    public class ActionEvent : MonoBehaviour
    {
        private string m_id;
        private bool m_isLoaded = false;
        private int m_currentIndex = 0;
        public string[][] m_ArrayData;

        ActionEvent()
        {
            StartCoroutine(LoadCSV("mainStory.csv", OnFileLoaded));
        }

        public void OnFileLoaded()
        {
            m_isLoaded = true;
        }

        IEnumerator LoadCSV(string fileName, UnityAction CompleteAction)
        {
            string sPath = Application.streamingAssetsPath + "/" + fileName;
            Debug.Log("sPath:" + sPath);
            WWW www = new WWW(sPath);
            while (!www.isDone)
            {
                yield return null;
            }
            Debug.Log("content2:" + www.text);
            File.WriteAllText(Application.persistentDataPath + "/" + fileName, www.text, Encoding.GetEncoding("utf-8"));
            LoadFile(Application.persistentDataPath, fileName);
            CompleteAction();
        }

        private void LoadFile(string path, string fileName)
        {
            m_ArrayData = new string[0][];
            string fillPath = path + "/" + fileName;

            string[] lineArray;
            try
            {
                lineArray = File.ReadAllLines(fillPath, Encoding.GetEncoding("utf-8"));
                Debug.Log("file finded!");
            }
            catch
            {
                Debug.Log("file do not find!");
                return;
            }

            m_ArrayData = new string[lineArray.Length][];
            for (int i = 0; i < lineArray.Length; i++)
            {
                m_ArrayData[i] = lineArray[i].Split(',');
            }
        }

        private string GetVaule(int row, int col)
        {
            return m_ArrayData[row][col];
        }

        public bool IsFinished()
        {
            return m_isLoaded && m_currentIndex == m_ArrayData.Length;
        }

        public void Update()
        {
            if (!m_isLoaded)
            {
                return;
            }

            foreach (var command in m_ArrayData)
            {
                switch (command[0])
                {
                    case "move":
                        {
                            Assert.IsTrue(command.Length == 3);
                            string who = command[1];
                            string where = command[2];
                        }

                        break;
                    case "interact":
                        {
                            Assert.IsTrue(command.Length == 3);

                            string who = command[1];
                            string what = command[2];
                        }

                        break;
                    case "talk":
                        {
                            Assert.IsTrue(command.Length > 3);
                        }

                        break;
                    case "talkKey":
                        {
                            Assert.IsTrue(command.Length > 3);
                        }

                        break;
                    default:
                        break;
                }
            }
        }
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

        public bool isNeedUpdate()
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_track2.isNeedUpdate())
        {
            m_track2.Update();
        }
    }

    StoryTrack m_track1;
    StoryTrack m_track2;
}
