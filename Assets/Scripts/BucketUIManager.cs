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
    [SerializeField] GameObject fishPrefab;
    [SerializeField] GameObject desc;

    private GameObject[] fishCards;

    enum Bait { Basic=0, Red, Blue, Strong };
    string[] descs = { "���� �⺻���� ������ ���� �⺻ �̳�", 
                        "���� ���� ����Ⱑ ������ ���� �̳�", 
                        "���� ū ����Ⱑ ������ �Ķ� �̳�", 
                        "���� ����, �� ū ����⸦ ���� �� �ִ� ���� �̳�" };
    public void OnClickInventory() {
        gameObject.SetActive(true);
        Button fishTab = fishBtn.GetComponent<Button>();
        fishTab.Select();
        OnClickFishTab();
    }

    public void OnClickFishTab() {
        DestroyChildren(fishScrollView.transform.GetChild(0).GetChild(0));
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
            fishCards[i].GetComponent<Button>().transition = Selectable.Transition.None;
            Transform fishTransform = fishCards[i].transform;
            fishTransform.SetParent(fishScrollView.transform.GetChild(0).GetChild(0)); // scrollview �ڽ��� viewport�� �ڽ��� content�� �θ�� ����.
            fishTransform.localScale = Vector3.one;

            if(fishCards[i].GetComponent<FishButton>())
            {
                fishCards[i].GetComponent<FishButton>().Index = i;
            }

            try
            {
                Transform fishName = fishTransform.GetChild(0);
                Transform fishImg = fishTransform.GetChild(1);
                string name = Player.Instance.FishTank[i].Name;
                fishName.GetComponent<TextMeshProUGUI>().text = name;
                fishImg.GetComponent<Image>().sprite = Player.Instance.FishTank[i].Image;

                //�ִ�ũ�� �ּ�ũ�⸦ ���� �̹��� ũ��� ��Ī
                float minsize = 80f;
                float size = minsize + Player.Instance.FishTank[i].Size/1.5f;

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
        for (int i = 0; i < baitPanel.transform.childCount-1; i++) {
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
        desc.SetActive(true);
        GameObject clickedBait = EventSystem.current.currentSelectedGameObject;
        
        // player currentbait�� 0-3
        Player.Instance.CurrentBait = (int)Enum.Parse(typeof(Bait), clickedBait.name);
        desc.GetComponent<TextMeshProUGUI>().text = descs[Player.Instance.CurrentBait];
        Sprite selectedBait = clickedBait.transform.GetChild(1).GetComponent<Image>().sprite;
        UIManager.Instance.EquipBait(selectedBait);
    }

    public void OnClickExitBtn() {
        gameObject.SetActive(false);
        baitPanel.SetActive(false);
        fishScrollView.SetActive(false);
        desc.SetActive(false);
    }

    private void DestroyChildren(Transform obj)
    {
        for (int i = 0; i < obj.childCount; i++)
        {
            Destroy(obj.GetChild(i).gameObject);
        }
    }
}
