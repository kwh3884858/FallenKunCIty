using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;

public class StoryTrigger : MonoBehaviour
{

	Transform m_mainCharacterTran;
	public float m_checkDistance = 1f;

	public void Show ()
	{
		m_mainCharacterTran = GameObject.Find ("SceneCave/Character").transform;
		PollerService.Instance ().RegisterPoller ((int)SkylightStaticData.PollerType.CheeckCharacterEnter, CheckCharacterEnter);
		PollerService.Instance ().OpenPollerList ((int)SkylightStaticData.PollerType.CheeckCharacterEnter);
		//对话系统播放结束的回调
		LogicManager.Instance ().RegisterCallback ((int)SkylightStaticData.LogicType.DialogPlayerCallback, HandleLogicEventHandler);
	}

	public void ExitWhenStoryFinish ()
	{

	}

	bool HandleLogicEventHandler (LogicManager.LogicData vars = null)
	{
		string _name = vars.m_name;
		if (_name == name) {

			//注册开始检查玩家离开回调
			PollerService.Instance ().RegisterPoller ((int)SkylightStaticData.PollerType.CheeckCharacterExit, CheeckCharacterExit);
			//切换成观察离开时间
			PollerService.Instance ().ClosePollerList ((int)SkylightStaticData.PollerType.CheeckCharacterEnter);
			PollerService.Instance ().OpenPollerList ((int)SkylightStaticData.PollerType.CheeckCharacterExit);
			//开始循环
			PollerService.Instance ().StartDoEvent ();

			return true;
		}
		return true;
	}


	private void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (transform.position, m_checkDistance);
	}

	bool CheeckCharacterExit ()
	{
		float dis = QuickDistance (transform.position, m_mainCharacterTran.transform.position);

		//Debug.Log (dis);

		if (dis > m_checkDistance + 1) {

			Recover ();

		}
		return true;
	}

	bool CheckCharacterEnter ()
	{
		float dis = QuickDistance (transform.position, m_mainCharacterTran.transform.position);

		//Debug.Log (dis);

		if (dis < m_checkDistance) {
			PollerService.Instance ().StopDoEvent ();

			PlayeStory ();

		}
		return true;
	}

	float QuickDistance (Vector2 source, Vector2 destination)
	{
		return (source.x - destination.x) * (source.x - destination.x) +
			(source.y - destination.y) * (source.y - destination.y);
	}

	private void PlayeStory ()
	{
		//移除这一次的观察
		PollerService.Instance ().UnsignPoller ((int)SkylightStaticData.PollerType.CheeckCharacterExit, CheeckCharacterExit);

		Debug.Log ("On Trigger " + name);
		UIBlackCurtain blackCurtain = UIManager.Instance ().ShowPanel<UIBlackCurtain> ();
		SoundService.Instance ().PlayEffect ("strange_wave");
		blackCurtain.PlayFadeIn ();

		LogicManager.Instance ().Notify ((int)SkylightStaticData.LogicType.DialogPlayerStart);
		DialogPlayer.Load (name);
	}

	private void Recover ()
	{

		//切换
		PollerService.Instance ().ClosePollerList ((int)SkylightStaticData.PollerType.CheeckCharacterExit);
		PollerService.Instance ().OpenPollerList ((int)SkylightStaticData.PollerType.CheeckCharacterEnter);

		//开始循环
		PollerService.Instance ().StartDoEvent ();
	}

	//GameObject.Destroy(gameObject);

	////创建渐入之后的回调
	//UIBlackCurtain blackCurtain = UIManager.Instance ().ShowUI<UIBlackCurtain> ();
	//	blackCurtain.AddCallback(() => {
	//			UIManager.Instance().CloseUI ();
	//	SoundService.Instance().PlayEffect ("strange_wave");
	//});
	//	blackCurtain.PlayFadeIn();
	//}
}
