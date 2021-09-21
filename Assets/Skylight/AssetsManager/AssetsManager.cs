using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using AssetBundles;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Skylight
{
	public class AssetsManager : GameModule<AssetsManager>
	{

		//AssetBundle打包路径/assets/StreamingAssets
		//public static string ASSETBUNDLE_PATH = Application.dataPath + "/StreamingAssets/AssetBundle/";

		//资源地址/assets
		//public static string APPLICATION_PATH = Application.dataPath + "/";

		//工程地址/
		//public static string PROJECT_PATH = APPLICATION_PATH.Substring (0, APPLICATION_PATH.Length - 7);

		//AssetBundle存放的文件夹名
		public static string ASSETBUNDLE_FILENAME = "AssetBundle";

		//AssetBundle打包的后缀名
		public static string SUFFIX = ".unity3d";

		// 已有的资源类型
		public const string UI = "ui.unity3d";
		public const string CSV = "csv.unity3d";
		public const string SCENE = "scene.unity3d";
		public const string SOUND = "sound.unity3d";
		public const string MODEL = "model.unity3d";
		//static bool isUseBundle = false;

		/// <summary>
		/// AssetBundle打包路径/assets/StreamingAssets/AssetBundle
		/// </summary>
		/// <value>The assetbundle path.</value>
		public static string ASSETBUNDLE_PATH {
			get {
				string dataPath = Application.dataPath;
				if (Application.platform == RuntimePlatform.IPhonePlayer) {
					return dataPath + "/Raw/AssetBundle/";
				}

				if (Application.platform == RuntimePlatform.Android) {
					return Application.streamingAssetsPath + "/";
					//return "jar:file//" + dataPath + "!/assets/";
				}

				if (Application.platform == RuntimePlatform.OSXPlayer) {
					return dataPath + "/Resources/Data/StreamingAssets/AssetBundle/";
				}

				if (Application.platform == RuntimePlatform.WindowsPlayer) {
					return dataPath + "/StreamingAssets/AssetBundle/";
				}

				return dataPath + "/StreamingAssets/AssetBundle/";
			}
		}

		/// <summary>
		/// 资源地址/assets
		/// </summary>
		/// <value>The application path.</value>
		public static string APPLICATION_PATH {
			get {
				string dataPath = Application.dataPath;
				if (Application.platform == RuntimePlatform.IPhonePlayer) {
					return dataPath + "/Raw/";
				}

				if (Application.platform == RuntimePlatform.Android) {
					return "jar:file//" + dataPath + "!/";
				}

				if (Application.platform == RuntimePlatform.OSXPlayer) {
					return dataPath + "/Resources/Data/";
				}

				if (Application.platform == RuntimePlatform.WindowsPlayer) {
					return dataPath + "/";
				}

				return dataPath + "/";
			}
		}

		/// <summary>
		/// 工程地址/
		/// </summary>
		/// <value>The project path.</value>
		public static string PROJECT_PATH {
			get {
				string dataPath = Application.dataPath.Substring (0, Application.dataPath.Length - 6);
				if (Application.platform == RuntimePlatform.IPhonePlayer) {
					return dataPath;
				}

				if (Application.platform == RuntimePlatform.Android) {
					return "jar:file//" + dataPath;
				}

				if (Application.platform == RuntimePlatform.OSXPlayer) {
					return dataPath;
				}

				if (Application.platform == RuntimePlatform.WindowsPlayer) {
					return dataPath;
				}

				return dataPath;
			}
		}

		public static T LoadPrefabs<T> (string path) where T : UnityEngine.Object
		{
			if (Application.isEditor)
			{
                //string strName = "Assets/Prefabs/" + path + ".prefab";
                //T go = AssetDatabase.LoadAssetAtPath<T> (strName);
                //			Debug.Log (path);
                //path = path.ToLower();
                //T go = AssetBundleLoad.LoadGameObject(path) as T;
#if UNITY_EDITOR
                string strName = "Assets/" + path + ".prefab";
                T go = AssetDatabase.LoadAssetAtPath<T>(strName);
                
                return go;
#else
                return null;
#endif
            }
            else
			{
				//string strName = ASSETBUNDLE_PATH + path;
				//Debug.Log(strName);
				//GameObject.Find ("Console").GetComponent <Text>().text += "\n" + strName;
				path = path.ToLower();
				T go = AssetBundleLoad.LoadGameObject(path) as T;
				return go;
			}
		}

		public static T LoadAnimationController<T> (string path) where T : UnityEngine.Object
		{
			if (Application.isEditor)
			{
#if UNITY_EDITOR
				string strName = "Assets/Prefabs/" + path + ".prefab";
				T go = AssetDatabase.LoadAssetAtPath<T>(strName);
				return go;
#else
                return null;
#endif
			}
			else
			{
				string strName = ASSETBUNDLE_PATH + path;
				Debug.Log(strName);
				AssetBundle bundle = AssetBundle.LoadFromFile(strName);
				T go = bundle.LoadAllAssets<T>()[0];
				return go;
			}
		}

		public static T Load<T> (string path) where T : UnityEngine.Object
		{
			if (Application.isEditor)
			{
				//string strName = "Assets/" + path;
				//T go = AssetDatabase.LoadAssetAtPath<T> (strName);
				T go = Resources.Load<T> (path) as T;
			return go;
			}
			else
			{

				//string strName = ASSETBUNDLE_PATH + path;
				//Console.Log(path);
				T go = Resources.Load (path, typeof(T)) as T;
			//AssetBundle bundle = AssetBundle.LoadFromFile(strName);
			//T go = bundle.LoadAllAssets<T>()[0];
			return go;
			}
		}


		//private IEnumerator Start ()
		//{
		//	AssetBundleManager.SetDevelopmentAssetBundleServer ();

		//	var request = AssetBundleManager.Initialize ();

		//	if (request != null)
		//		yield return StartCoroutine (request);

		//	//AssetBundleLoadAssetOperation loadRequest = AssetBundleManager.LoadAssetAsync("prefab", "Cube", typeof(GameObject));
		//	//if (loadRequest == null)
		//	//    yield break;

		//	//yield return StartCoroutine(loadRequest);

		//	//GameObject prefab = loadRequest.GetAsset();
		//	////如果讀取成功, 則創建實體
		//	//if (prefab != null)
		//	//    GameObject.Instantiate(prefab);

		//	//yield return new WaitForSeconds(5f);
		//	////釋放"prefab"這個bundle
		//	//AssetBundleManager.UnloadAssetBundle("prefab");
		//}
	}
}
