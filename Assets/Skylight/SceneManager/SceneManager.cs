using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace Skylight
{
	public class SceneManager : GameModule<SceneManager>
	{

		Dictionary<string, GameObject> mAllScenes = new Dictionary<string, GameObject> ();
		private BaseScene mCurrentScene = null;

		public BaseScene m_currentScene {
			get {
				return mCurrentScene;
			}
		}


		public void ShowScene<T> (Dictionary<string, object> varList = null) where T : BaseScene
		{
			if (mCurrentScene != null) {

				mCurrentScene.gameObject.SetActive (false);

				Debug.Log ("destroy");
				mCurrentScene = null;
			}

			string name = typeof (T).ToString ();
			GameObject uiObject;
			if (!mAllScenes.TryGetValue (name, out uiObject)) {
				string perfbName = "Scenes/" + typeof (T).ToString ();
				//Debug.Log ("Loaded Perfab : " + perfbName);
				GameObject perfb = AssetsManager.LoadPrefabs<GameObject> (perfbName);
				uiObject = GameObject.Instantiate (perfb);
				uiObject.name = name;
				T t = uiObject.AddComponent<T> ();
				uiObject.transform.SetParent (transform);
				t.SceneInit ();

				mAllScenes.Add (name, uiObject);
			} else {

				uiObject.SetActive (true);

			}

			if (uiObject) {
				T scene = uiObject.GetComponent<T> ();
				scene.SceneShow ();
				if (varList != null)
					scene.m_UserData = varList;

				mCurrentScene = scene;

				uiObject.SetActive (true);
			}
		}

		public void CloseScene ()
		{
			if (mCurrentScene) {
				mCurrentScene.GetComponent<BaseScene> ().SceneClose ();
				mCurrentScene.gameObject.SetActive (false);
				mCurrentScene = null;
			}
		}
	}
}