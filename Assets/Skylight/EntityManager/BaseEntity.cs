using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skylight
{

	public class BaseEntity : MonoBehaviour
	{
		//所链接的实体游戏对象
		//public GameObject m_GameObject;
		public List<BaseComponent> m_components;

		public virtual void Init ()
		{
			//m_GameObject = gameObject;
			m_components = new List<BaseComponent> ();
			GetComponents<BaseComponent> (m_components);
			//Debug.Log (m_components);
			//如果没有组件已经挂在上面则不需要操作
			if (m_components.Count == 0) {
				return;
			}
			foreach (BaseComponent comp in m_components) {
				//Debug.Log ("save " + comp.m_componentType + " type to ComponentManager");
				ComponentManager.Instance.RegisterEntity (comp.m_componentType, this);
			}
		}

		//添加某一类型的组件
		public BaseComponent AddComponent (ComponentType type)
		{
			if (ExistSpecialComponent (type)) {
				//Debug.Log("Exist same component Type" + type);
				return GetSpecicalComponent (type);

			}
			return null;
			//Debug.Log ("Add component" + type + " in " + gameObject);

			//PropertyComponent property = GetSpecicalComponent<PropertyComponent> ();
			//StateComponent stateComponent = GetSpecicalComponent<StateComponent> ();

			//switch (type) {

			//case ComponentType.Property:
			//	PropertyComponent propertyComp = gameObject.AddComponent<PropertyComponent> ();
			//	propertyComp.Init (ComponentType.Property, this);
			//	m_components.Add (propertyComp);
			//	return propertyComp;

			//case ComponentType.Dead:
			//	DeadComponent deadComp = gameObject.AddComponent<DeadComponent> ();
			//	deadComp.Init (ComponentType.Dead, this);
			//	deadComp.m_hp = property.m_HP;
			//	deadComp.m_san = property.m_San;
			//	m_components.Add (deadComp);

			//	return deadComp;

			//case ComponentType.State:
			//	StateComponent stateComp = gameObject.AddComponent<StateComponent> ();
			//	stateComp.Init (ComponentType.State, this);
			//	m_components.Add (stateComp);
			//	stateComp.m_actionPoint = property.m_AP;
			//	return stateComp;

			//case ComponentType.Ability:
			//	AbilityComponent abilityComp = gameObject.AddComponent<AbilityComponent> ();
			//	abilityComp.Init (ComponentType.Ability, this);
			//	m_components.Add (abilityComp);
			//	return abilityComp;

			//case ComponentType.Hide:
			//	HideComponent hideComp = gameObject.AddComponent<HideComponent> ();
			//	hideComp.Init (ComponentType.Hide, this);
			//	m_components.Add (hideComp);
			//	return hideComp;

			//case ComponentType.Move:
			//	MoveComponent moveComp = gameObject.AddComponent<MoveComponent> ();
			//	moveComp.Init (ComponentType.Move, this);
			//	m_components.Add (moveComp);
			//	moveComp.m_moveSpeed = property.m_moveSpd;
			//	moveComp.m_turnTime = property.m_turnTime;
			//	moveComp.m_SPD = property.m_SPD;
			//	if (stateComponent != null)
			//		moveComp.m_SPD += stateComponent.m_speedBuff;

			//	return moveComp;

			//case ComponentType.RoleMove:
			//	RoleMoveComponent roleMoveComp = gameObject.AddComponent<RoleMoveComponent> ();
			//	roleMoveComp.Init (ComponentType.RoleMove, this);
			//	m_components.Add (roleMoveComp);
			//	roleMoveComp.moveSpeed = property.m_moveSpd;
			//	roleMoveComp.SPD = property.m_SPD;
			//	if (stateComponent != null)
			//		roleMoveComp.SPD += stateComponent.m_speedBuff;
			//	roleMoveComp.m_turnTime = property.m_turnTime;
			//	return roleMoveComp;
			//case ComponentType.Attack:
			//	AttackComponent attackComp = gameObject.AddComponent<AttackComponent> ();
			//	attackComp.Init (ComponentType.Attack, this);
			//	m_components.Add (attackComp);
			//	attackComp.m_STR = property.m_STR;
			//	if (stateComponent != null) {
			//		attackComp.m_STR += stateComponent.m_attackBuff;
			//	}
			//	attackComp.m_attackRange = property.attckRange;
			//	return attackComp;

			//case ComponentType.Input:
			//	InputComponent inputComp = gameObject.AddComponent<InputComponent> ();
			//	inputComp.Init (ComponentType.Input, this);
			//	m_components.Add (inputComp);
			//	return inputComp;

			//case ComponentType.CheerUp:
			//	CheerUpComponent cheerUpComp = gameObject.AddComponent<CheerUpComponent> ();
			//	cheerUpComp.Init (ComponentType.CheerUp, this);
			//	m_components.Add (cheerUpComp);
			//	return cheerUpComp;

			//case ComponentType.Monitor:
			//	MonitorComponent monitorComp = gameObject.AddComponent<MonitorComponent> ();
			//	monitorComp.Init (ComponentType.Monitor, this);
			//	m_components.Add (monitorComp);
			//	monitorComp.m_SightArea = property.m_HRZ;
			//	return monitorComp;

			//case ComponentType.Knock:
			//	KnockComponent knockComp = gameObject.AddComponent<KnockComponent> ();
			//	knockComp.Init (ComponentType.Knock, this);
			//	m_components.Add (knockComp);
			//	knockComp.m_ridus = property.m_HRZ;
			//	return knockComp;

			//case ComponentType.Item:
			//	ItemComponent itemComp = gameObject.AddComponent<ItemComponent> ();
			//	itemComp.Init (ComponentType.Item, this);
			//	itemComp.item = property.m_item;
			//	itemComp.numLimit = property.m_itemLimit;
			//	m_components.Add (itemComp);
			//	return itemComp;

			//case ComponentType.AI:
			//	AIComponent aiComp = gameObject.AddComponent<AIComponent> ();
			//	aiComp.Init (ComponentType.AI, this);
			//	aiComp.m_actionInterval = property.m_actionInterval;
			//	aiComp.m_moveInterval = property.m_moveInterval;
			//	m_components.Add (aiComp);
			//	return aiComp;
			//case ComponentType.UI:
			//	UIComponent uiComp = gameObject.AddComponent<UIComponent> ();
			//	uiComp.Init (ComponentType.UI, this);
			//	m_components.Add (uiComp);
			//	return uiComp;
			//case ComponentType.Animation:
			//	AnimationComponent animationComp = gameObject.AddComponent<AnimationComponent> ();
			//	animationComp.Init (ComponentType.Animation, this);
			//	m_components.Add (animationComp);
			//	return animationComp;

			//case ComponentType.Buff:
			//	BuffComponent buffComponent = gameObject.AddComponent<BuffComponent> ();
			//	buffComponent.Init (ComponentType.Buff, this);
			//	m_components.Add (buffComponent);
			//	return buffComponent;

			//case ComponentType.Deck:
			//	DeckComponent deckComponent = gameObject.AddComponent<DeckComponent> ();
			//	deckComponent.Init (ComponentType.Deck, this);
			//	deckComponent.m_characterActionPoint = property.m_AP;
			//	m_components.Add (deckComponent);
			//	return deckComponent;
			//case ComponentType.LandItem:
			//	LandItemComponent landItemComponent = gameObject.AddComponent<LandItemComponent> ();
			//	landItemComponent.Init (ComponentType.LandItem, this);
			//	m_components.Add (landItemComponent);
			//	return landItemComponent;
			//default:
			//	return null;
			//}

		}
		//移除某个组件
		public void RemoveComponent (ComponentType type)
		{
			if (m_components.Count == 0) {
				//Debug.Log ("there is no component to delete");
				return;
			}
			//Debug.Log ("Before remove component number is:" + m_components.Count);
			for (int i = 0; i < m_components.Count; i++) {
				//Debug.Log ("e.m_componentType" + m_components [i].m_componentType);
				if (m_components [i].m_componentType == type) {
					ComponentManager.Instance.LogoutEntity (m_components [i].m_componentType, this);
					DestroyImmediate (GetSpecicalComponent (type));
					m_components.RemoveAt (i);
					//Debug.Log ("delete " + type + "in " + this.gameObject.name + " success");
					return;
				}
			}
			//Debug.Log ("remove fail: cant find" + type + " component in entity!");
		}
		public void RemoveAllComponent ()
		{
			for (int i = 0; i < m_components.Count; i++) {
				ComponentManager.Instance.LogoutEntity (m_components [i].m_componentType, this);
				Destroy (GetSpecicalComponent (m_components [i].m_componentType));

			}
			m_components.Clear ();
		}
		//获取特定的组件
		public BaseComponent GetSpecicalComponent (ComponentType type)
		{
			int componentCount = m_components.Count;
			for (int i = 0; i < componentCount; i++) {
				if (m_components [i].m_componentType == type) {
					return m_components [i];
				}
			}
			Debug.Log ("cant find this component in entity!");
			return null;
		}
		//public BasicComponent GetSpecicalComponent<T>() where T : BasicComponent{
		//	string componentName = typeof (T).ToString ();
		//	int componentCount = m_components.Count;
		//	for (int i = 0; i < componentCount; i++){
		//		if(m_components[i].m_componentType == (ComponentType)componentName){
		//			return m_components [i];
		//		}
		//	}
		//	foreach (BasicComponent e in m_components) {
		//		//Debug.Log ("get " + e.m_componentType + " component");

		//		if (typeof(e) == componenname) {
		//			return e;
		//		}
		//	}
		//	//Debug.Log ("cant find component in entity!");
		//	return null;
		//}

		//是否存在某个组件
		public bool ExistSpecialComponent (ComponentType type)
		{

			int componentCount = m_components.Count;
			for (int i = 0; i < componentCount; i++) {
				if (m_components [i].m_componentType == type) {
					return true;
				}
			}
			return false;
		}

	}

}
