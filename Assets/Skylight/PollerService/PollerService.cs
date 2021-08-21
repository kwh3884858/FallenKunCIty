using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skylight
{
	public class PollerService : GameModule<PollerService>
	{

		public delegate bool Poller ();
		public float m_interval;

		private List<int> m_allowList;
		private bool m_isDoEvent;

		private float m_recorder;

		Dictionary<int, List<Poller>> m_pollers;

		public override void SingletonInit ()
		{
			base.SingletonInit ();
			m_pollers = new Dictionary<int, List<Poller>> ();
			m_allowList = new List<int> ();
			m_recorder = Time.time;
			m_interval = 0.5f;
			m_isDoEvent = true;
		}

		private void FixedUpdate ()
		{
			if (!m_isDoEvent) return;

			m_recorder += Time.fixedDeltaTime;
			if (m_recorder > m_interval) {

				DoEvent ();

				m_recorder = 0;
			}
		}

		public void OpenPollerList (int openId)
		{
			Debug.Log ("Open " + (SkylightStaticData.PollerType)openId + " Poller List");
			if (m_allowList.Contains (openId)) {
				return;
			}
			m_allowList.Add (openId);
		}

		public void ClosePollerList (int closeId)
		{
			Debug.Log ("Close " + (SkylightStaticData.PollerType)closeId + " Poller List");

			if (!m_allowList.Contains (closeId)) {
				return;
			}
			m_allowList.Remove (closeId);
		}

		public void RegisterPoller (int pollerId, Poller poller)
		{
			if (!m_pollers.ContainsKey (pollerId)) {
				m_pollers.Add (pollerId, new List<Poller> ());
				//m_pollers [pollerId].Add (poller);
			}
			//else {
			//	//Debug.Log ("Already Exist This Callback Poller");

			//}

			//if (m_pollers [pollerId].Count == 0)
			//m_pollers [pollerId].Add (poller);

			if (!m_pollers [pollerId].Contains (poller)) {
				m_pollers [pollerId].Add (poller);
			} else {
				Debug.Log ("Already Exist This Callback Poller");
			}
		}

		public void UnsignPoller (int pollerId, Poller poller)
		{

			if (m_pollers.ContainsKey (pollerId)) {
				for (int i = 0; i < m_pollers [pollerId].Count; i++) {
					if (m_pollers [pollerId] [i] == poller) {
						m_pollers [pollerId].RemoveAt (i);
					}
				}

			}
		}

		private void DoEvent ()
		{
			for (int i = 0; i < m_allowList.Count; i++) {
				foreach (Poller poller in m_pollers [m_allowList [i]]) {
					if (!(poller ())) {
						return;
					}
				}
			}

		}

		public void StopDoEvent ()
		{
			m_isDoEvent = false;
		}

		public void StartDoEvent ()
		{
			m_isDoEvent = true;
		}
	}

}
