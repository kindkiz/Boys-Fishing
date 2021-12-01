using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingUIManager : MonoBehaviour
{
    public FishingManager fishingManager;
    public Image successArea;
    public GameObject fish;
    public GameObject bar;

    void OnEnable()
    {
        bar.SetActive(true);
        successArea.fillAmount = fishingManager.SuccessAreaSize / 500f;
    }

    void Update()
    {
        Vector3 cursorPosition = new Vector3(0, fishingManager.CursorPosition, 0);
        fish.transform.GetComponent<RectTransform>().anchoredPosition = cursorPosition;
    }

    void OnDisable()
    {
        bar.SetActive(false);
    }
}