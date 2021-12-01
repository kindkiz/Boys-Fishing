using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingUIManager : MonoBehaviour
{
    public Image successArea;
    public GameObject fish;
    public GameObject bar;

    void Start()
    {
        bar.SetActive(true);
        successArea.fillAmount = FishingManager.Instance.SuccessAreaSize / 500f;
    }

    void Update()
    {
        Vector3 cursorPosition = new Vector3(0, FishingManager.Instance.CursorPosition, 0);
        fish.transform.GetComponent<RectTransform>().anchoredPosition = cursorPosition;
    }

    void OnDisable()
    {
        
    }
}