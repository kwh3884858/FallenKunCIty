using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skylight;
public class GameRuntimeSetting : GameModule<GameRuntimeSetting>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ELanguage GetCurrentLanguage ()
	{
        return m_language;
	}

    public void ChangeLanguage(ELanguage language)
	{
		if (language != m_language) {
            m_language = language;
		}
	}
    public enum ELanguage
	{
        Chinese,
        Japanese,
	}
    private ELanguage m_language = ELanguage.Japanese;
}
