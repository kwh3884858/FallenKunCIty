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
                Destroy (this);
			}
		}
    }

    public void StartSubtitile(string content)
	{
        m_countDown = m_delayPerWord * content.Length;
        GetComponent<Text> ().text = content;
        m_isStartCountDown = true;
    }
    public float m_delayPerWord = 0.5f;

    private float m_countDown = 100.0f;
    private bool m_isStartCountDown = false;
}
