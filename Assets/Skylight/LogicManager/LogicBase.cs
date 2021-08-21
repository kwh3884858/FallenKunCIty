using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Skylight
{
	// 此类只用来编写逻辑和数据，不用来操作非Logic的代码。
	// 需要和外部交互的话，需要生成事件交给外部来自己注册感兴趣的事件
	// 这边暂时使用 MonoBehaviour 方便调试 后续会把他删除掉的
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