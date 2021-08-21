using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

namespace Skylight
{
	public class UIManager : GameModule<UIManager>
	{
		private GameObject m_dialog;
		private GameObject m_panel;
		private GameObject m_overlay;
		private GameObject m_box;

		private Stack<GameObject> m_dialogs = new Stack<GameObject> ();
		private UIDialog m_currentDialog = null;
		private UIBox m_currentBox = null;

		public override void SingletonInit ()
		{
			transform.SetParent (GameRoot.Instance ().transform);
			m_box = AddGameObject ("box");
			m_panel = AddGameObject ("panel");
			m_dialog = AddGameObject ("dialog");
			m_overlay = AddGameObject ("overlay");

		}

		public T ShowDialog<T> (Dictionary<string, object> varList = null) where T : UIDialog
		{
			if (m_currentDialog != null) {
				m_currentDialog.gameObject.SetActive (false);
				m_currentDialog = null;
			}

			string name = typeof (T).ToString ();
			GameObject uiObject;

			Transform dialogTran = m_panel.transform.Find (name);

			if (!dialogTran) {
				string perfbName = "UI/Dialog/" + typeof (T).ToString ();
				Debug.Log (perfbName);
				GameObject perfb = AssetsManager.LoadPrefabs<GameObject> (perfbName);
				if (perfb == null) {
					Debug.Log ("UIDialog Can`t Find Perfab");
				}
				uiObject = GameObject.Instantiate (perfb);
				uiObject.name = name;
				T t = uiObject.AddComponent<T> ();
				uiObject.transform.SetParent (m_dialog.transform);

				t.PanelInit ();

			} else {
				uiObject = dialogTran.gameObject;
			}

			if (uiObject) {
				T panel = uiObject.GetComponent<T> ();
				panel.PanelOpen ();
				if (varList != null)
					panel.m_userData = varList;

				m_currentDialog = panel;

				m_dialogs.Push (uiObject);

				uiObject.SetActive (true);

			}
			return uiObject.GetComponent<T> ();
		}

		public void CloseAllDialogs ()
		{
			while (m_dialogs.Count != 0) {
				CloseCurrentDialog ();
			}
		}
		public void CloseCurrentDialog ()
		{
			m_currentDialog.GetComponent<UIDialog> ().PanelClose ();

			m_currentDialog.gameObject.SetActive (false);
			m_currentDialog = null;
			if (m_dialogs.Count != 0) {
				GameObject uiDialog = m_dialogs.Pop ();
				uiDialog.SetActive (true);
				m_currentDialog = uiDialog.GetComponent<UIDialog> ();

			}
		}

		public T ShowOverlay<T> (Dictionary<string, object> varList = null) where T : UIOverlay
		{
			string name = typeof (T).ToString ();
			Debug.Log (name);
			var panelTran = m_panel.transform.Find (name);
			GameObject uiObject;
			T panel = null;
			if (panelTran == null) {
				string perfbName = "UI/Panel/" + typeof (T).ToString ();
				GameObject perfb = AssetsManager.LoadPrefabs<GameObject> (perfbName);
				if (perfb == null) {
					Debug.Log ("UIPanel Can`t Find Perfab");
				}
				uiObject = GameObject.Instantiate (perfb);
				uiObject.name = name;
				T t = uiObject.AddComponent<T> ();
				uiObject.transform.SetParent (m_panel.transform);

				t.PanelInit ();
			} else {
				uiObject = panelTran.gameObject;
			}
			if (uiObject) {
				panel = uiObject.GetComponent<T> ();
				panel.PanelOpen ();
				if (varList != null)
					panel.m_userData = varList;

				uiObject.SetActive (true);
			}
			return panel;
		}

		public T ShowPanel<T> (Dictionary<string, object> varList = null) where T : UIPanel
		{
			string name = typeof (T).ToString ();
			Debug.Log (name);
			Transform panelTran = m_panel.transform.Find (name);
			GameObject uiObject;
			T panel = null;
			if (panelTran == null) {
				string perfbName = "UI/Panel/" + typeof (T).ToString ();
				GameObject perfb = AssetsManager.LoadPrefabs<GameObject> (perfbName);
				if (perfb == null) {
					Debug.Log ("UIPanel Can`t Find Perfab");
				}
				uiObject = GameObject.Instantiate (perfb);
				uiObject.name = name;
				T t = uiObject.AddComponent<T> ();
				uiObject.transform.SetParent (m_panel.transform);

				t.PanelInit ();
			} else {
				uiObject = panelTran.gameObject;
			}
			if (uiObject) {
				panel = uiObject.GetComponent<T> ();
				panel.PanelOpen ();
				if (varList != null)
					panel.m_userData = varList;

				uiObject.SetActive (true);
			}

			return panel;
		}

		public void ClosePanel<T> () where T : UIPanel
		{
			string name = typeof (T).ToString ();
			Transform panelTran = m_panel.transform.Find (name);
			if (panelTran != null) {
				panelTran.GetComponent<T> ().PanelClose ();
				panelTran.gameObject.SetActive (false);
			}

		}

		public void ShowBox<T> (Dictionary<string, object> varList = null) where T : UIBox
		{
			string name = typeof (T).ToString ();

			var panelTran = m_box.transform.Find (name);
			GameObject uiObject;
			if (panelTran == null) {
				string perfbName = "UI/Box/" + typeof (T).ToString ();
				GameObject perfb = AssetsManager.LoadPrefabs<GameObject> (perfbName);
				uiObject = GameObject.Instantiate (perfb);
				uiObject.name = name;
				T t = uiObject.AddComponent<T> ();
				uiObject.transform.SetParent (m_box.transform);
				t.PanelInit ();
			} else {
				uiObject = panelTran.gameObject;
			}
			if (uiObject) {
				T box = uiObject.GetComponent<T> ();
				box.PanelOpen ();
				if (varList != null)
					box.m_userData = varList;

				if (m_currentBox)
					m_currentBox.gameObject.SetActive (false);

				uiObject.SetActive (true);
				m_currentBox = box;
			}
		}

		public void CloseBox ()
		{
			if (m_currentBox)
				m_currentBox.gameObject.SetActive (false);

			m_currentBox = null;
		}
	}
}