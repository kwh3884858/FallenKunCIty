using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Skylight
{
	public class BaseScene : MonoBehaviour
	{
		// Use this for initialization
		public Dictionary<string, object> m_UserData = null;
		void Start ()
		{
		}

		public virtual void AddGameObject (GameObject go)
		{
			go.transform.SetParent (transform);
		}

		public virtual void SceneInit ()
		{
			LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.SceneInit);
		}
		public virtual void SceneShow ()
		{
			LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.SceneOpen);

		}
		public virtual void SceneClose ()
		{
			LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.SceneClose);

		}
	}
}
