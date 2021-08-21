using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skylight
{
	public class GameRoot : MonoSingleton<GameRoot>
	{
		// Use this for initialization
		void Start ()
		{
			//AddGameObject<NetService>();
			//DONT CHANGE ORDER
			//不要修改顺序，有相互依赖关系
			AddGameObject<LogicManager> ();
			AddGameObject<CameraService> ();


			AddGameObject<PollerService> ();
			AddGameObject<SceneManager> ();
			AddGameObject<SoundService> (); 			AddGameObject<UIManager> ();


			LogicManager.Instance ().LogicStart ((int)SkylightStaticData.LogicType.PanelStart);
		}

		// Update is called once per frame
		void Update ()
		{

		}


		public void AddSystemManger ()
		{


		}
	}
}
