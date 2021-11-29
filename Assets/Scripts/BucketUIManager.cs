using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class BucketUIManager : MonoBehaviour
{
    [SerializeField] GameObject fishScrollView;
    [SerializeField] GameObject baitPanel;
    [SerializeField] GameObject fishBtn;
    [SerializeField] GameObject baitBtn;
    [SerializeField] GameObject fishPrefab;

    private GameObject[] fishCards;

    Color clickedColor = Color.white;
    Color defaultColor = new Color(214, 214, 214);

/*    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public void OnClickInventory() {
        gameObject.SetActive(true);
        Button fishTab = fishBtn.GetComponent<Button>();
    }

    public void OnClickFishTab() {
        baitPanel.SetActive(false);
        fishScrollView.SetActive(true);

        // ���� �� ����� �κ��丮 �����ֱ� (�̹��� ũ�⵵ ����)
        int fishCnt = 6;
        try
        {
            fishCnt = Player.Instance.FishTank.Count;
        }
        catch(NullReferenceException e) {
            print(e);
        }
        
        fishCards = new GameObject[fishCnt];
        for (int i = 0; i < fishCnt; i++)
        {
            fishCards[i] = Instantiate(fishPrefab);
            Transform fishTransform = fishCards[i].transform;
            fishTransform.parent = fishScrollView.transform.GetChild(0).GetChild(0); // scrollview �ڽ��� viewport�� �ڽ��� content�� �θ�� ����.
            fishTransform.localScale = Vector3.one;

            try
            {
                Transform fishName = fishTransform.GetChild(0);
                Transform fishImg = fishTransform.GetChild(1);
                fishName.GetComponent<TextMeshProUGUI>().text = Player.Instance.FishTank[i].Name;
                fishImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("FishImg") as Sprite;
                fishImg.GetComponent<Image>().color = Color.red;
                float size = Player.Instance.FishTank[i].Size;
                fishImg.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            }
            catch (NullReferenceException e)
            {
                print(e);
            }
        }
    }

    public void OnClickBaitTab() {
        fishScrollView.SetActive(false);
        baitPanel.SetActive(true);

        // ���� �� �̳� ���� �����ֱ�. ���� �ٲٱ�.
    }

    public void OnClickExitBtn() {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        clickedObject.transform.parent.gameObject.SetActive(false);
        baitPanel.SetActive(false);
        fishScrollView.SetActive(false);
    }

/*    private void ChangeBtnNormalColor(Button button, Color color) {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        button.colors = cb;
    }*/
}
