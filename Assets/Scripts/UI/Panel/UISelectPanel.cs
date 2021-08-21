using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelectPanel : Skylight.UIPanel
{

	GameObject m_selectionGo;

	Transform m_selectionTf;

	//Scene m_scene;
	// Use this for initialization
	//void InitVars ()
	//{
	//	m_scene = GameObject.Find ("Scene").GetComponent<Scene> ();
	//}

	// Update is called once per frame
	void Update ()
	{

	}

	public void InitSelect (List<string> selections)
	{
		//if (m_scene == null)
		//{
		//    InitVars();
		//}
		//if (GameObject.Find("Scene") != null)
		//{
		//    m_scene.isRunOrRation = true;//关闭时可以移动

		//    m_scene.NotOnDialogOrTalk = true;//关闭时可以打开背包
		//}
		//if (GameObject.Find("RigidBodyFPSController") != null)
		//{
		//    GameObject.Find("RigidBodyFPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().mouseLook.SetCursorLock(false);
		//}
		//if (Camera.main.GetComponent<RayCastDetection>() != null)
		//{
		//    Camera.main.GetComponent<RayCastDetection>().CloseDetection();
		//}

		m_selectionTf = transform.Find ("Select");
		m_selectionGo = m_selectionTf.gameObject;

		//m_selectionGo.name = selections[0];
		//m_selectionTf.Find("SelectText").GetComponent<Text>().text = selections[0];

		//if (selections.Count > 1)
		//{
		//    for (int i = 1; i < selections.Count; i++)
		//    {
		//        GameObject go = GameObject.Instantiate(m_selectionGo);

		//        go.transform.SetParent(m_selectionTf.parent);
		//        go.transform.localScale = new Vector3(1, 1, 1);
		//        //go.AddComponent<SendSelected>();
		//        go.GetComponent<RectTransform>().position = m_selectionTf.GetComponent<RectTransform>().position + new Vector3(0, 90, 0);
		//        go.transform.Find("SelectText").GetComponent<Text>().text = selections[i];
		//        go.name = selections[i];
		//    }
		//}
		//for (int i = 0; i < selections.Count; i++)
		//{
		//    AddButtonClick(selections[i], Select);
		//}


	}


	//    public void Select ()
	//	{
	//		m_scene.isRunOrRation = true;//关闭时可以移动

	//		m_scene.NotOnDialogOrTalk = true;//关闭时可以打开背包
	//		GameObject.Find ("RigidBodyFPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ().mouseLook.SetCursorLock (false);
	//		CancelInvoke ();
	//		Invoke ("CallDialogPlayer", 0.5f);

	//	}

	//	public void CallDialogPlayer ()
	//	{
	//		DialogPlayer.PlaySelection ();
	//	}
}
