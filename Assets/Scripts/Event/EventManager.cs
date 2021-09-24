using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using StarPlatinum.Base;

namespace StarPlatinum.EventManager
{
	/// <summary>
	/// 逻辑类型，用于EventManager
	/// </summary>

	public class EventManager : Singleton<EventManager>
	{
		//LogicBase m_currentLogic;
		private Dictionary<Type, Delegate> m_allEvent = new Dictionary<Type, Delegate> ();
		public void AddEventListener<T> (EventHandler<T> handler) where T : EventArgs
		{
			Delegate d;
			if (m_allEvent.TryGetValue (typeof (T), out d)) {
				m_allEvent [typeof (T)] = Delegate.Combine (d, handler);
			} else {
				m_allEvent [typeof (T)] = handler;
			}
		}
		public void RemoveEventListener<T> (EventHandler<T> handler) where T : EventArgs
		{
			Delegate d;
			if (m_allEvent.TryGetValue (typeof (T), out d)) {
				Delegate currentDel = Delegate.Remove (d, handler);

				if (currentDel == null) {
					m_allEvent.Remove (typeof (T));
				} else {
					m_allEvent [typeof (T)] = currentDel;
				}
			}
		}

		//Examlpe:
		//EventManager.Instance.SendEvent<SceneLoadedEvent>(new SceneLoadedEvent (sceneName));

		public void SendEvent<T> (T message) where T : EventArgs
		{
			if (message == null) {
				throw new ArgumentNullException ("e");
			}
			Delegate d;
			if (m_allEvent.TryGetValue (typeof (T), out d)) {
				(d as EventHandler<T>)?.Invoke (this, message);
			}
		}

		//事件委托.
		//public delegate void EventDelegate<T> (T e) where T : EventArgs;

	}

}