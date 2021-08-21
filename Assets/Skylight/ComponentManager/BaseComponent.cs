using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Skylight
{
	/// <summary>
	/// 组件类型
	/// 如果需要添加新的组件，请确认
	/// 1.componentType和对应的component，system已经完善
	/// 2.在basicEntity中加入对应的构造函数
	/// 3.在SystemManager中加入system的初始化
	/// </summary>
	public enum ComponentType
	{
		Null,
		Property,   //属性组件，直接挂载game object上，长期存在的。属性值，实体类型，其他定义
		BlockInfo,  //方块组件，直接挂载，通过编辑器生成的固有物体
					//通用
		Dead,       //死亡组件，长期组件，判断生命数值，san值
		State,      //状态组件，长期组件，行动点
		Ability,    //能力组件
		Animation,  //动画组件，长期组件，用于播放动画
		LandItem,   //地表物品组件，管理地表物品，如陷阱。

		//玩家通用
		Hide,       //躲藏在掩体后，长期组件
		Vision,     //玩家视野
		Deck,       //套牌系统，管理玩家的套牌，手牌，弃牌

		//敌人通用
		Monitor,    //监视，长期组件，会被遮挡的视野，看到玩家时会进入警觉状态


		//通用临时

		Attack,     //攻击组件，临时组件
		Input,      //输入组件，临时组件
		UI,         //临时组件
		Buff,       //buff组件，提供一些接口供其他组件调用

		//玩家临时
		CheerUp,    //鼓舞，临时组件
		Drone,      //无人机，临时组件
		Hack,       //骇入，临时组件
		Knock,      //发出声音吸引敌人
		Item,       //使用物品
		RoleMove,   //角色移动组件
					//敌人临时
		vigilance,  //警觉状态，短期组件，会记录玩家最后出现的位置，一段时间后解除警觉
		AI,         //短期组件，系统长期运行
		Move,       //移动组件，临时组件
	}


	//向component管理器注册当前组件
	public class BaseComponent : MonoBehaviour
	{
		//组件类型
		public ComponentType m_componentType = ComponentType.Null;
		//组件所对应的实体
		public BaseEntity m_entity;

		//必须执行Init作为初始化
		public virtual void Init (ComponentType componentType, BaseEntity basicEntity)
		{
			m_entity = basicEntity;
			m_componentType = componentType;
			//在管理器上注册
			ComponentManager.Instance.RegisterEntity (m_componentType, m_entity);
		}
		//我在尝试让接口变得更简单，查看是否有方法能尽量屏蔽过程
		public virtual void Init (BaseEntity basicEntity)
		{
			m_entity = basicEntity;
			if (m_componentType == ComponentType.Null) {
				Debug.Log ("该类型还未被定义，请先确保m_componentType定义完备");
				return;
			}
			//在管理器上注册
			ComponentManager.Instance.RegisterEntity (m_componentType, m_entity);
		}
		public bool IsType (ComponentType type)
		{
			return m_componentType == type;
		}
	}


}
