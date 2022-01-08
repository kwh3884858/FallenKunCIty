using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Skylight
{
	public class LogicBase : MonoBehaviour
	{
		public delegate bool LogicEventHandler (LogicManager.LogicData vars = null);


		public LogicBase ()
		{
			mhtEvent = new Dictionary<int, ArrayList> ();
		}

		public void RegisterCallback (int nEventID, LogicEventHandler handler)
		{
			ArrayList events;
			if (!mhtEvent.TryGetValue (nEventID, out events)) {
				events = new ArrayList ();
				mhtEvent.Add (nEventID, events);
			}
			events.Add (handler);
		}

		public void DoEvent (int nEventID, LogicManager.LogicData vars = null)
		{
			ArrayList events;
			if (!mhtEvent.TryGetValue (nEventID, out events)) {
				return;
			}

			foreach (LogicEventHandler handle in events) {
				if (!handle (vars))
					break;
			}
		}


		virtual public void LogicInit () { }
		virtual public void LogicShow () { }
		virtual public void LogicClose () { }

		virtual public void Notify (int eventId, LogicManager.LogicData vars = null) { }

		virtual public void LogicStart (int eventId) { }

		Dictionary<int, ArrayList> mhtEvent;
	}
}