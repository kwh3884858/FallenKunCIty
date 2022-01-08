using Skylight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : UIPanel
{
    public override void PanelInit()
    {
        AddButtonClick("Return", Return);

        m_languageDropdown = transform.Find("Language/Dropdown").GetComponent<Dropdown>();

        m_languageDropdown.value =(int)GameRuntimeSetting.Instance().GetCurrentLanguage();
        m_languageDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_languageDropdown);
        });
    }

    //every open will call
    public override void PanelOpen()
    {

    }
    //every close will call
    public override void PanelClose()
    {

    }

    void Return()
    {
        UIManager.Instance().ClosePanel<ConfigPanel>();
    }

    void DropdownValueChanged(Dropdown change)
    {
        GameRuntimeSetting.ELanguage language = (GameRuntimeSetting.ELanguage)change.value;
        if (language != GameRuntimeSetting.Instance().GetCurrentLanguage())
        {
            GameRuntimeSetting.Instance().ChangeLanguage(language);
        }
    }

    Dropdown m_languageDropdown;
}
