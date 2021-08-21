using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class PlayerData 
{
    public int scenceID;
    public string key;
    public string objName;
    public Vector3 playerPos;
    public Vector3 playerEul;
    public Vector3 playerSca;
    
    public PlayerData()
    {

        scenceID = 0;
    }
}

public class PlayerDataLS : MonoBehaviour
{
    public PlayerData data;
    public GameDataManage gameDataManage;

    public void Start()
    {
        data = new PlayerData();
        gameDataManage = GameObject.Find("SavePoint").GetComponent<GameDataManage>();
        GetData();
        if (Globe.isLoad)
        {
            if(Load())
            {
                Globe.isLoad = false ;
            }
            else
            {
                Globe.isLoad = false;
                SceneManager.LoadScene(1);
            }
        }
        Debug.Log(data.scenceID);
    }


    public bool Load()
    {
        if (data.scenceID != 0)
        {
            transform.localPosition = data.playerPos;
            transform.localEulerAngles = data.playerEul;
            transform.localScale = new Vector3(Mathf.Abs( data.playerSca.x),data.playerSca.y,data.playerSca.z);
           
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Save()
    {
        data.objName = transform.name;
        data.scenceID = SceneManager.GetActiveScene().buildIndex;
        data.playerPos = transform.localPosition;
        data.playerEul = transform.localEulerAngles;
        data.playerSca = transform.localScale;
        gameDataManage.Save(data);
    }

    private void GetData()
    {
        gameDataManage.Load();
        data = gameDataManage.cloneData;
    }
}
