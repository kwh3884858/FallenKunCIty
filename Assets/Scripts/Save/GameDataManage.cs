using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Xml;
using System.Security.Cryptography;

//管理数据储存的类//
public class GameDataManage : MonoBehaviour
{
    public static GameDataManage _instance;

    private string dataFileName = "save.data";//存档文件的名称,自己定//
    private XmlSaver xs = new XmlSaver();

    public PlayerData cloneData;
    public PlayerDataLS player;

	private Collider2D[] _colliders;

	public LayerMask playerMask;
    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerDataLS>();
        cloneData = new PlayerData();
        _instance = this;
        //设定密钥，根据具体平台设定//
        cloneData.key = SystemInfo.deviceUniqueIdentifier;
		//
		_colliders = GetComponents<Collider2D> ();
    }

    //存档时调用的函数//
    public void Save(PlayerData playerData)
    {
        string playerDataFile = GetDataPath() + "/" + dataFileName;
        //设定密钥，根据具体平台设定//
        playerData.key = SystemInfo.deviceUniqueIdentifier;

        string dataString = xs.SerializeObject(playerData, typeof(PlayerData));
        xs.CreateXML(playerDataFile, dataString);
    }

    //读档时调用的函数//
    public void Load()
    {
        string playerDataFile = GetDataPath() + "/" + dataFileName;
        if (xs.hasFile(playerDataFile))
        {
            string dataString = xs.LoadXML(playerDataFile);
            PlayerData playerDataFromXML = xs.DeserializeObject(dataString, typeof(PlayerData)) as PlayerData;

            //是合法存档//
            if (playerDataFromXML.key == cloneData.key)
            {
                cloneData = playerDataFromXML;
             
            }
            //是非法拷贝存档//
            else
            {
                //留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
            }
        }
        else
        {
            Debug.Log("Save");
            //存档文件不在时创建空文档
            if (cloneData != null)
            {
                Save(cloneData);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
		Debug.Log (player.data.scenceID);
        if (col.name == "Player")
        {
            player.Save();
            
        }
    }

	private void Update ()
	{
		foreach(Collider2D _collider in _colliders){
			int HorizontalRayCount = 10;

			RaycastHit2D [] hits = new RaycastHit2D [HorizontalRayCount];
			float rayLenth = 1;
			float HorizontalRaySpacing = _collider.bounds.size.y / (HorizontalRayCount - 1);

			Vector2 rayOrigin;
			for (int i = 0; i < HorizontalRayCount; i++) {
				rayOrigin = (Vector2)_collider.bounds.max + Vector2.down * HorizontalRaySpacing *i ;
				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right, rayLenth, playerMask);
				hits [i] = hit;
				Debug.DrawRay (rayOrigin, Vector2.right, Color.red);
			}

			for (int i = 0; i < HorizontalRayCount; i++) {
				if (!hits [i]) {
					continue;
				}
				if (hits [i].transform.tag == "Player") {
					Debug.Log ("Yes!Save!");
					player.Save ();
				}
			}
		}

	}

	//获取路径//
	private static string GetDataPath()
    {
        // Your game has read+write access to /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents
        // Application.dataPath returns ar/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data             
        // Strip "/Data" from path
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            // Strip application name
            path = path.Substring(0, path.LastIndexOf('/'));
            return path + "/Documents";
        }
        else
            //    return Application.dataPath + "/Resources";
            return Application.dataPath;
    }
    

}