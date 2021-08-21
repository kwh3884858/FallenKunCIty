using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skylight
{
	public enum TableType
	{
		TT_Example,
		TT_Model,
		TT_Soldier,
		TT_Amount
	};

	public enum Model_Type
	{
		ARCHER	=	1,//������
		CATAPULT	=	2,//Ͷʯ��
		CAVALRY	=	3,//���
		HEAVY_INFANTRY	=	4,//����
		LIGHT_INFANTRY	=	5,//ǹ��
	};

	public class DBData_Example : DBRecord
	{
		public override void Parse(string[] cols)
		{
			fileds = new object[cols.Length];
			fileds[0] = int.Parse(cols[0]);
			fileds[1] = cols[1];
			fileds[2] = int.Parse(cols[2]);
			fileds[3] = float.Parse(cols[3]);
		}
		public int ID { get { return (int)fileds[0]; } }//���
		public string strValue { get { return (string)fileds[1]; } }//�ַ�ֵ
		public int IntValue { get { return (int)fileds[2]; } }//����ֵ
		public float FloatValue { get { return (float)fileds[3]; } }//����ֵ
	};

	public class DBData_Model : DBRecord
	{
		public override void Parse(string[] cols)
		{
			fileds = new object[cols.Length];
			fileds[0] = int.Parse(cols[0]);
			fileds[1] = cols[1];
			fileds[2] = cols[2];
			fileds[3] = cols[3];
			fileds[4] = cols[4];
			fileds[5] = cols[5];
			fileds[6] = cols[6];
			fileds[7] = cols[7];
			fileds[8] = cols[8];
			fileds[9] = cols[9];
			fileds[10] = cols[10];
			fileds[11] = cols[11];
			fileds[12] = cols[12];
		}
		public int ID { get { return (int)fileds[0]; } }//���
		public string Name { get { return (string)fileds[1]; } }//����
		public string Fbx { get { return (string)fileds[2]; } }//ģ��
		public string Idle { get { return (string)fileds[3]; } }//����
		public string FightIdle { get { return (string)fileds[4]; } }//ս������
		public string Walk { get { return (string)fileds[5]; } }//��·
		public string FightWalk { get { return (string)fileds[6]; } }//ս����·
		public string Run { get { return (string)fileds[7]; } }//�ܲ�
		public string FightRun { get { return (string)fileds[8]; } }//ս���ܲ�
		public string AttackA { get { return (string)fileds[9]; } }//����A
		public string AttackB { get { return (string)fileds[10]; } }//����B
		public string DeathA { get { return (string)fileds[11]; } }//����A
		public string DeathB { get { return (string)fileds[12]; } }//����B
	};

	public class DBData_Soldier : DBRecord
	{
		public override void Parse(string[] cols)
		{
			fileds = new object[cols.Length];
			fileds[0] = int.Parse(cols[0]);
			fileds[1] = int.Parse(cols[1]);
			fileds[2] = int.Parse(cols[2]);
			fileds[3] = float.Parse(cols[3]);
			fileds[4] = float.Parse(cols[4]);
			fileds[5] = int.Parse(cols[5]);
			fileds[6] = int.Parse(cols[6]);
			fileds[7] = float.Parse(cols[7]);
			fileds[8] = float.Parse(cols[8]);
		}
		public int ID { get { return (int)fileds[0]; } }//���
		public int type { get { return (int)fileds[1]; } }//ģ������
		public int hp { get { return (int)fileds[2]; } }//Ѫ��
		public float range { get { return (float)fileds[3]; } }//�������
		public float range2 { get { return (float)fileds[4]; } }//ʿ�����
		public int attack { get { return (int)fileds[5]; } }//������
		public int defend { get { return (int)fileds[6]; } }//������
		public float speed { get { return (float)fileds[7]; } }//�ƶ��ٶ�
		public float cdtime { get { return (float)fileds[8]; } }//�������
	};


};

