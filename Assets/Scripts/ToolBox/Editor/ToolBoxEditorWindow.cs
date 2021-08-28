using Config.GameRoot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolBoxEditorWindow : EditorWindow
{
	private Scene m_currentGameScene = new Scene();

	private Vector2 _scrollPos = Vector2.zero;

	SceneLookupEnum m_enumStartScene;
	SceneLookupEnum m_enumStartSceneInConfig;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Tools/Tool Box %l")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		ToolBoxEditorWindow window = (ToolBoxEditorWindow)EditorWindow.GetWindow(typeof(ToolBoxEditorWindow));
		window.Show();

	}

	void OnGUI()
	{
		if (Application.isPlaying)
		{
			return;
		}

		_scrollPos = GUI.BeginScrollView(
			new Rect(0, 0, position.width, position.height),
			_scrollPos,
			new Rect(0, 0, 400, 800)
		);
		GUILayout.Label("Start From This Scene", EditorStyles.boldLabel);

		m_enumStartSceneInConfig = ConfigRoot.Instance.StartScene;
		m_enumStartScene = (SceneLookupEnum)EditorGUILayout.EnumPopup("Start Scene:", m_enumStartSceneInConfig);
		if (m_enumStartScene != m_enumStartSceneInConfig)
		{
			ConfigRoot.Instance.StartScene = m_enumStartScene;
			Debug.Log($"Set {m_enumStartScene.ToString()} as Start Scene");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		GUILayout.Space(20);
		if (GUILayout.Button("Save to Config"))
		{
			ConfigRoot.Instance.StartScene = m_enumStartScene;
			EditorUtility.SetDirty(ConfigRoot.Instance);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		GUILayout.Label("Info", EditorStyles.boldLabel);

		EditorGUILayout.BeginVertical();
		if (m_currentGameScene.name == null || m_currentGameScene.name == "")
		{
			m_currentGameScene = SceneManager.GetActiveScene();
		}
		EditorGUILayout.LabelField("Current Active Game Sccene:  " + m_currentGameScene.name);
		EditorGUILayout.EndVertical();

		GUI.EndScrollView();

	}
}
