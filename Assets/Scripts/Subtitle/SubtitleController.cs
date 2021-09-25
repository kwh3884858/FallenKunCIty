using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SubtitleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (m_isStartCountDown) {
            m_countDown -= Time.deltaTime;

			if (m_countDown < 0) {
                m_isStartCountDown = false;
                Destroy(gameObject);
			}
		}
    }

    public void StartSubtitile(string who, string content, float delayTime, bool isKeyTalk)
	{
        m_countDown = delayTime;
        Transform nameGO = transform.Find("Name");
        Transform contentGO = transform.Find("Content");

        Assert.IsNotNull(nameGO);
        Assert.IsNotNull(contentGO);

        nameGO.GetComponent<Text> ().text = who;
        contentGO.GetComponent<Text>().text = content;

        if (isKeyTalk)
        {
            contentGO.GetComponent<Text>().color = Color.red;
        }

        m_isStartCountDown = true;
    }

    public float m_crossFadeDuration = 0.45f;
    [SerializeField]
    private bool m_isStartCountDown = false;
    [SerializeField]
    private float m_countDown = 100.0f;

}
