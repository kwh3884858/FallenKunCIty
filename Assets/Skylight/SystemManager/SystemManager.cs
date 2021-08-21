using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skylight
{
	public class SystemManager : MonoSingleton<SystemManager>
	{
		//玩家队列
		//private List<BasicEntity> PlayerList;
		//敌人队列
		//private List<BasicEntity> EnemyList;
		//系统队列
		private List<BaseSystem> basicSystems;

		private void Awake ()
		{


			//PlayerList = new List<BasicEntity> ();
			//EnemyList = new List<BasicEntity> ();
			basicSystems = new List<BaseSystem> ();

			//找到对应game object动态加载实体

			////动态加载模块
			//AttackComponent attackComponent = GameObject.Find ("HealthyCube").AddComponent<AttackComponent> ();
			////必须执行，依次加载组件的初始化，动态加载的组件会在组件管理器中注册
			//attackComponent.Init (ComponentType.Attack, cube);

			////必须执行，最后初始化实体，它会把直接挂载在game object上的组件一起注册
			//cube.Init ();

			//PlayerList.Add (cube);

			//DeadSystem deadSystem = new DeadSystem ();
			//deadSystem.M_LinkedType = ComponentType.Dead;

			//HideSystem hideSystem = new HideSystem ();
			//hideSystem.M_LinkedType = ComponentType.Hide;

			//MoveSystem moveSystem = new MoveSystem ();
			//moveSystem.M_LinkedType = ComponentType.Move;

			//RoleMoveSystem roleMoveSystem = new RoleMoveSystem ();
			//roleMoveSystem.M_LinkedType = ComponentType.RoleMove;

			//LandItemSystem landItemSystem = new LandItemSystem ();
			//landItemSystem.M_LinkedType = ComponentType.LandItem;

			//AttackSystem attackSystem = new AttackSystem ();
			//attackSystem.M_LinkedType = ComponentType.Attack;

			//InputSystem inputSystem = new InputSystem ();
			//inputSystem.M_LinkedType = ComponentType.Input;

			//UISystem uiSystem = new UISystem ();
			//uiSystem.M_LinkedType = ComponentType.UI;

			//CheerUpSystem cheerUpSystem = new CheerUpSystem ();
			//cheerUpSystem.M_LinkedType = ComponentType.CheerUp;

			//MonitorSystem monitorSystem = new MonitorSystem ();
			//monitorSystem.M_LinkedType = ComponentType.Monitor;

			//KnockSystem knockSystem = new KnockSystem ();
			//knockSystem.M_LinkedType = ComponentType.Knock;

			//ItemSystem itemSystem = new ItemSystem ();
			//itemSystem.M_LinkedType = ComponentType.Item;

			//AISystem aiSystem = new AISystem ();
			//aiSystem.M_LinkedType = ComponentType.AI;

			//AnimationSystem animationSystem = new AnimationSystem ();
			//animationSystem.M_LinkedType = ComponentType.Animation;

			//BuffSystem buffSystem = new BuffSystem ();
			//buffSystem.M_LinkedType = ComponentType.Buff;

			//DeckSystem deckSystem = new DeckSystem ();
			//deckSystem.M_LinkedType = ComponentType.Deck;

			//basicSystems.Add (deadSystem);
			//basicSystems.Add (hideSystem);
			//basicSystems.Add (moveSystem);
			//basicSystems.Add (roleMoveSystem);
			//basicSystems.Add (landItemSystem);
			//basicSystems.Add (attackSystem);
			//basicSystems.Add (inputSystem);
			//basicSystems.Add (uiSystem);
			//basicSystems.Add (cheerUpSystem);
			//basicSystems.Add (monitorSystem);
			//basicSystems.Add (knockSystem);
			//basicSystems.Add (itemSystem);
			//basicSystems.Add (aiSystem);
			//basicSystems.Add (animationSystem);
			//basicSystems.Add (buffSystem);
			//basicSystems.Add (deckSystem);

		}


		private void Start ()
		{
			//现在game object中只有property和entity
			GameObject [] objects = GameObject.FindGameObjectsWithTag ("Character");
			foreach (GameObject obj in objects) {
				BaseEntity entity = obj.GetComponent<BaseEntity> ();

				////能力组件是添加其他所有组件的入口
				//AbilityComponent abilityComp = (AbilityComponent)entity.AddComponent (ComponentType.Ability);
				//abilityComp.Init (ComponentType.Ability, entity);

				entity.Init ();
			}

			////初始化系统组件，把现在的所有实体传入
			//StateSystem stateSystem = new StateSystem ();
			//stateSystem.M_LinkedType = ComponentType.State;
			//List<BasicEntity> ent = stateSystem.Fliter (ComponentType.Property);
			////初始化所有需要的组件
			//if (ent != null) {
			//	stateSystem.Init (ent);
			//}


			//所有系统的初始化函数
			foreach (BaseSystem basicSystem in basicSystems) {
				Debug.Log ("init " + basicSystem.M_LinkedType + " System");
				List<BaseEntity> entities = ComponentManager.Instance.GetSpecialEntity (basicSystem.M_LinkedType);
				if (entities != null) {
					basicSystem.Init (entities);
				}
			}
			//最后才加入系统表，防止二次初始化
			//basicSystems.Add (stateSystem);
		}

		private void Update ()
		{
			int systemCount = basicSystems.Count;
			for (int i = 0; i < systemCount; i++) {

			}
			foreach (BaseSystem basicSystem in basicSystems) {
				//Debug.Log ("Update " + basicSystem.M_LinkedType + " System");
				List<BaseEntity> entities = ComponentManager.Instance.GetSpecialEntity (basicSystem.M_LinkedType);
				if (entities != null) {
					//Console.Log ("execute system:" + basicSystem.M_LinkedType + "system");
					basicSystem.Execute (entities);
				}
			}

			//回合制

		}

		private BaseSystem GetSystem (ComponentType type)
		{
			foreach (BaseSystem sys in basicSystems) {
				if (sys.M_LinkedType == type) {
					return sys;
				}
			}
			Debug.Log ("Cant Find System Type: " + type);
			return null;
		}
	}




}

