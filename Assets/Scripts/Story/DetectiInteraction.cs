using Skylight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LogicManager.Instance().Notify((int)SkylightStaticData.LogicType.InteractionIconExit);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isInteraction)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_isInteraction.GetComponentInParent<Interaction>().PlayAnimation();
            }
        }   
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interaction")
        {
            Vector3 screenPos = CameraService.Instance().GetMainCamera().GetComponent<Camera>().WorldToScreenPoint(collision.transform.position);
            LogicManager.Instance().Notify((int)SkylightStaticData.LogicType.InteractionIconShow, new LogicManager.LogicData { m_uiScreenPosition = screenPos });
            m_isInteraction = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interaction")
        {
            LogicManager.Instance().Notify((int)SkylightStaticData.LogicType.InteractionIconExit);
            m_isInteraction = null;
        }
    }

    GameObject m_isInteraction = null;
}
