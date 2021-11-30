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

    public void OnClickInventory() {
        gameObject.SetActive(true);
        Button fishTab = fishBtn.GetComponent<Button>();
    }

    public void OnClickFishTab() {
        baitPanel.SetActive(false);
        fishScrollView.SetActive(true);

        // ���� �� ����� �κ��丮 �����ֱ� (�̹��� ũ�⵵ ����)
        int fishCnt;
        try
        {
            fishCnt = Player.Instance.FishTank.Count;
            //Debug.Log("���� �κ��丮 �� ����� ��: "+fishCnt);
        }
        catch(Exception e) {
            fishCnt = 6;
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
                string name = Player.Instance.FishTank[i].Name;
                fishName.GetComponent<TextMeshProUGUI>().text = name;
                fishImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("FishImg/"+ name) as Sprite;

                //TODO: ������ ����=���� �� ���� ���θ� �� ��� ���� ���
                float size = Player.Instance.FishTank[i].Size;
                fishImg.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }

    public void OnClickBaitTab() {
        fishScrollView.SetActive(false);
        baitPanel.SetActive(true);

        // ���� �� �̳� ���� �����ֱ�. ���� �ٲٱ�.
        for (int i = 0; i < baitPanel.transform.childCount; i++) {
            Transform baitCount = baitPanel.transform.GetChild(i).GetChild(2);
            try
            {
                baitCount.GetComponent<TextMeshProUGUI>().text = Player.Instance.Bait[i].ToString();
            }
            catch {
                baitCount.GetComponent<TextMeshProUGUI>().text = i.ToString(); //Test�� �ڵ�
            }
        }
    }

    public void OnClickCertainBait() {
        GameObject clickedBait = EventSystem.current.currentSelectedGameObject;
        // TODO: ��������Ʈ �����ϱ�
        Debug.Log(clickedBait.name);
        Sprite selectedBait = clickedBait.transform.GetChild(1).GetComponent<Image>().sprite;
        UIManager.Instance.EquipBait(selectedBait);
    }

    public void OnClickExitBtn() {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        clickedObject.transform.parent.gameObject.SetActive(false);
        baitPanel.SetActive(false);
        fishScrollView.SetActive(false);
    }
}
