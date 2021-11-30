using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingUIManager : MonoBehaviour
{
    public Image successArea;
    public GameObject fish;

    void Start()
    {
        successArea.fillAmount = FishingManager.Instance.SuccessAreaSize;
    }

    void Update()
    {
        Vector3 cursorPosition = new Vector3(0, FishingManager.Instance.CursorPosition, 0);
        fish.transform.GetComponent<RectTransform>().anchoredPosition = cursorPosition;
    }
}