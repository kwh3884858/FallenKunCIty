using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skylight
{
	public class ComponentManager : TSingleton<ComponentManager>

	{
		Dictionary<ComponentType, List<BaseEntity>> allComponentTypeEntites;


		private ComponentManager ()
		{
			allComponentTypeEntites = new Dictionary<ComponentType, List<BaseEntity>> ();
		}

		//获取包含特定的组件类型的所有实体
		public List<BaseEntity> GetSpecialEntity (ComponentType componentType)
		{
			if (allComponentTypeEntites.ContainsKey (componentType)) {
				return allComponentTypeEntites [componentType];

			} else {
				return new List<BaseEntity> ();
			}
		}
		//将类型和所对的组件注册
		public void RegisterEntity (ComponentType componentType, BaseEntity entity)
		{
			if (!allComponentTypeEntites.ContainsKey (componentType)) {
				//Debug.Log ("Is creating " + componentType + " Type in ComponentManager");

				allComponentTypeEntites.Add (componentType, new List<BaseEntity> ());

			}
			bool isExit = false;
			foreach (BaseEntity comp in allComponentTypeEntites [componentType]) {
				if (comp == entity) {
					Debug.Log (comp.gameObject.name + " entity is exited!");
					isExit = true;
				}
			}
			if (!isExit) {
				allComponentTypeEntites [componentType].Add (entity);
			}
		}
		//将某一些实体登出
		public void LogoutEntity (ComponentType componentType, BaseEntity entity)
		{
			if (allComponentTypeEntites.ContainsKey (componentType)) {

				for (int i = 0; i < allComponentTypeEntites [componentType].Count; i++) {
					if (allComponentTypeEntites [componentType] [i] == entity) {
						//Debug.Log ("Remove " + componentType + " : " + allComponentTypeEntites [componentType] [i].name + " from component manager");

						allComponentTypeEntites [componentType].RemoveAt (i);

					}
				}
			}
			//Debug.Log ("allComponents[componentType].Count" + allComponentTypeEntites [componentType].Count);
			if (allComponentTypeEntites [componentType].Count == 0) {
				allComponentTypeEntites.Remove (componentType);
			}
		}
	}

}


