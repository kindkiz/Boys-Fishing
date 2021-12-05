using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingUIManager : MonoBehaviour
{
    public FishingManager fishingManager;
    public Image successArea;
    public GameObject fish;
    public GameObject bar;
    public Image timeBar;
    public Transform alert;

    void OnEnable()
    {
        bar.SetActive(true);
        fish.GetComponent<Image>().sprite = fishingManager.fish.Image;
        successArea.fillAmount = fishingManager.SuccessAreaSize / (FishingManager.BAR_SIZE - 2 * FishingManager.BLANK);
    }

    void Update()
    {
        Vector3 cursorPosition = new Vector3(0, fishingManager.CursorPosition, 0);
        fish.transform.GetComponent<RectTransform>().anchoredPosition = cursorPosition;
        timeBar.fillAmount = fishingManager.RemainTime / FishingManager.MAX_TIME_LIMIT;
        if(fishingManager.CheckArea())
        {
            Color color = successArea.color;
            color.a = 0.5f + fishingManager.TimeInSuccessArea / fishingManager.TimeToSuccess / 2;
            successArea.color = color;
        }
        else
        {
            Color color = successArea.color;
            color.a = 0.5f;
            successArea.color = color;
        }
    }

    public IEnumerator AlertSuccess()
    {
        bar.SetActive(false);
        yield return new WaitForSeconds(1f);
        alert.GetChild(0).GetChild(0).GetComponent<Image>().sprite = fishingManager.fish.Image;
        alert.GetChild(1).GetComponent<TextMeshProUGUI>().text = "야호! 야생의\n["+fishingManager.fish.Name+"]을(를) 잡았다!";
        alert.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        alert.gameObject.SetActive(false);
        fishingManager.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Fail()
    {
        bar.SetActive(false);
        fishingManager.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}