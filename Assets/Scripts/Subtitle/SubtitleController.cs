using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                Destroy (gameObject);
			}
		}
    }

    public void StartSubtitile(string content, float delayTime)
	{
        m_countDown = delayTime;
        GetComponent<Text> ().text = content;
        m_isStartCountDown = true;
    }

    [SerializeField]
    private bool m_isStartCountDown = false;
    [SerializeField]
    private float m_countDown = 100.0f;

}
