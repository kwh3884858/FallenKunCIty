#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;
using Skylight;

namespace StarPlatinum.Base
{

    public abstract class ConfigSingleton<T> : ScriptableObject where T : ScriptableObject
    {

        public static ref T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = LoadConfig();
                }
                return ref m_instance;
            }
        }

        public static void Preload()
        {
            LoadConfig();
        }

        private static T LoadConfig()
        {
            m_instance = null;

            //Is loading
            if (m_isLoading)
            {
                return null;
            }

            if (Application.isEditor)
            {
#if UNITY_EDITOR
                m_isLoading = true;
                string loadPath = $"Assets/Resources/Config/{typeof(T).Name}.asset";

                m_instance = AssetDatabase.LoadAssetAtPath<T>(loadPath);
                if (m_instance == null) { Debug.LogError($"{loadPath} doesn`t exist {typeof(T).Name}"); return null; }

                Debug.Log($"======Resource加载完成:{typeof(T).Name}, path:{loadPath}, config:{m_instance}=====");
                m_isLoading = false;
                return m_instance;
#else
                return null;
#endif
            }
            else
            {
                m_isLoading = true;
                string perfbName = "Config/" + typeof(T).ToString();
                m_instance = AssetsManager.Load<T>(perfbName);
                Debug.Log($"===========Resource加载完成:{typeof(T).Name}, path:{perfbName}, config:{m_instance}=====");
                m_isLoading = false;
                return m_instance;
            }
        }

        private static T m_instance = null;
        private static bool m_isLoading = false;
    }

}