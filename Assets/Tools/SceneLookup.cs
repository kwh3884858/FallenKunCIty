//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Skylight.Editor.SceneLookupGenerator
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using UnityEngine.Assertions;

[System.Serializable]
public enum SceneLookupEnum
{
GameRoot = 0,
GhostCatMain = 1,
LegoGameDesignerGym = 2,
LegoGhostCat = 3,
SampleScene = 4,
SceneCave = 5
}

public static class SceneLookup {

public const int GameRoot = 0;
public const int GhostCatMain = 1;
public const int LegoGameDesignerGym = 2;
public const int LegoGhostCat = 3;
public const int SampleScene = 4;
public const int SceneCave = 5;


    private const int TotalComponents = 6;

    private static readonly string[] m_sceneLists = {
"GameRoot",
"GhostCatMain",
"LegoGameDesignerGym",
"LegoGhostCat",
"SampleScene",
"SceneCave"
    };

	public static string GetString (SceneLookupEnum eScene)
	{
		return m_sceneLists [(int)eScene];
	}

	public static string [] GetAllSceneString ()
	{
		return m_sceneLists;
	}

	public static SceneLookupEnum GetEnum (string sceneName, bool matchCase = false)
	{
		Assert.IsTrue (IsSceneExist (sceneName, matchCase), "Scene Name DO NOT exist");

		for (int i = 0; i < m_sceneLists.Length; i++) {
			if (matchCase) {
				if (m_sceneLists [i] == sceneName) {
					return (SceneLookupEnum)i;
				}
			} else {
				if (m_sceneLists [i].ToLower () == sceneName.ToLower ()) {
					return (SceneLookupEnum)i;
				}
			}
		}

		//Never be executed 
		throw new InvalidOperationException ("Scene Name DO NOT exist");
	}

	public static SceneLookupEnum GetEnumNoMatchCase (string sceneName)
	{
		return GetEnum (sceneName, false);
	}

	public static bool IsSceneExist (string sceneName, bool matchCase = false)
	{
		foreach (var existName in m_sceneLists) {
			if (matchCase) {
				if (existName == sceneName) {
					return true;
				}
			} else {
				if (existName.ToLower () == sceneName.ToLower ()) {
					return true;
				}
			}

		}
		return false;
	}
    public static bool IsSceneExist(SceneLookupEnum sceneEnum)
    {
        return IsSceneExist(sceneEnum.ToString());
    }

	public static bool IsSceneExistNoMatchCase (string sceneName)
	{
		return IsSceneExist (sceneName, false);
	}
}

