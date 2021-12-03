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
    public Image gaugeFish;

    void OnEnable()
    {
        bar.SetActive(true);
        gaugeFish.sprite = fishingManager.fish.Image;
        successArea.fillAmount = fishingManager.SuccessAreaSize / (FishingManager.BAR_SIZE - 2 * FishingManager.BLANK);
    }

    void Update()
    {
        Vector3 cursorPosition = new Vector3(0, fishingManager.CursorPosition, 0);
        fish.transform.GetComponent<RectTransform>().anchoredPosition = cursorPosition;
    }

    void OnDisable()
    {
        if(bar) bar.SetActive(false);
    }
}