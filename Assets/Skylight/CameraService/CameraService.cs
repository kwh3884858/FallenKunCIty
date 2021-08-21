using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skylight
{
	public class CameraService : GameModule<CameraService>
	{
		public GameObject m_mainCamera;

		private GameObject m_cachedCamera;

		public override void SingletonInit ()
		{
			base.SingletonInit ();
			LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.MainMenuOpen, SetCamera);
			LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.MainMenuClose, CameraClose);
			LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.SceneOpen, SetCamera);
			LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.SceneClose, CameraClose);

		}

		public GameObject GetCamera ()
		{

			Camera [] cameras = Camera.allCameras;
			Debug.Log ("Camera Length:" + cameras.Length);
			for (int i = 0; i < cameras.Length; i++) {
				Debug.Log ("Camera" + i + " name: " + cameras [i].name);
				if (cameras [i].name != "AutoCamera") {
					return cameras [i].gameObject;
				}
			}
			Debug.Log ("This scene doean`t contain a camera!");
			return null;

		}

		private void SetCachedCamera (bool flag)
		{
			if (m_cachedCamera == null) {
				m_cachedCamera = AssetsManager.LoadPrefabs<GameObject> ("prefabs/AutoCamera");
				m_cachedCamera = Instantiate (m_cachedCamera);
				m_cachedCamera.transform.SetParent (transform);
			}

			m_cachedCamera.SetActive (flag);
		}

		public bool SetCamera (LogicManager.LogicData vars = null)
		{

			m_mainCamera = GetCamera ();
			if (m_mainCamera == null) {
				SetCachedCamera (true);

			}
			return true;
		}

		public bool CameraClose (LogicManager.LogicData vars = null)
		{
			Camera [] cameras = Camera.allCameras;
			Debug.Log ("Camera Length:" + cameras.Length);
			for (int i = 0; i < cameras.Length; i++) {
				Debug.Log ("Camera" + i + " name: " + cameras [i].name);
				//if (cameras [i].name != "AutoCamera") {
				//	return cameras [i].gameObject;
				//}
				cameras [i].gameObject.SetActive (false);
			}
			//if (m_mainCamera != null) {
			//	m_mainCamera.SetActive (false);
			//}
			//if (m_cachedCamera != null && m_cachedCamera.activeSelf == true) {
			//	m_cachedCamera.SetActive (false);
			//}
			m_mainCamera = null;

			return true;
		}

	}
}