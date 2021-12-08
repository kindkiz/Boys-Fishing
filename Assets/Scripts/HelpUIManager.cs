using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUIManager : MonoBehaviour
{
    [SerializeField] GameObject helpPanel;
    public void OnClickHelpBtn()
    {
        helpPanel.SetActive(true);
    }

    public void OnClickExit()
    {
        helpPanel.SetActive(false);
    }
}
