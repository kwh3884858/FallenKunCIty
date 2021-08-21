using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Skylight
{
	public class LogicManager : GameModule<LogicManager>
	{
		LogicBase m_currentLogic;

		public override void SingletonInit ()
		{
			base.SingletonInit ();
			OpenLogic<MainMenuLogic> ();
		}

		public void LogicStart (int eventId)
		{
			m_currentLogic.LogicStart (eventId);
		}

		public T AddLogic<T> () where T : LogicBase
		{
			var go = new GameObject ();
			go.name = typeof (T).ToString ();
			T t = go.AddComponent<T> ();
			m_currentLogic = t;
			go.transform.SetParent (transform);

			t.LogicInit ();
			t.LogicShow ();
			return t;
		}

		public T GetLogic<T> () where T : LogicBase
		{
			Transform logicTran = transform.Find (typeof (T).ToString ());
			if (logicTran != null) {
				T t = logicTran.GetComponent<T> ();
				m_currentLogic = t;
				t.LogicShow ();
				return t;
			} else {
				return null;
			}
		}

		public T OpenLogic<T> () where T : LogicBase
		{
			if (m_currentLogic != null) {
				m_currentLogic.LogicClose ();
				m_currentLogic = null;
			}

			T t = null;
			t = GetLogic<T> ();
			if (t == null) {
				t = AddLogic<T> ();
			}

			return t;
		}

		public void Notify (int eventId, LogicData vars = null)
		{
			Debug.Log ((SkylightStaticData.LogicType)eventId);
			m_currentLogic.Notify (eventId, vars);
			m_currentLogic.DoEvent (eventId, vars);
		}

		public void RegisterCallback (int nEventID, LogicBase.LogicEventHandler handler)
		{
			m_currentLogic.RegisterCallback (nEventID, handler);
		}

		public class LogicData
		{
			public string m_name;

		}
	}

}