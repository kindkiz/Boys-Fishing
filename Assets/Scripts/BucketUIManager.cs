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

        // 현재 내 물고기 인벤토리 보여주기 (이미지 크기도 조절)
        int fishCnt;
        try
        {
            fishCnt = Player.Instance.FishTank.Count;
            //Debug.Log("현재 인벤토리 내 물고기 수: "+fishCnt);
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
            fishTransform.parent = fishScrollView.transform.GetChild(0).GetChild(0); // scrollview 자식인 viewport의 자식인 content를 부모로 설정.
            fishTransform.localScale = Vector3.one;

            try
            {
                Transform fishName = fishTransform.GetChild(0);
                Transform fishImg = fishTransform.GetChild(1);
                string name = Player.Instance.FishTank[i].Name;
                fishName.GetComponent<TextMeshProUGUI>().text = name;
                fishImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("FishImg/"+ name) as Sprite;

                //TODO: 사이즈 가로=세로 로 할지 가로를 더 길게 할지 고민
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

        // 현재 내 미끼 정보 보여주기. 갯수 바꾸기.
        for (int i = 0; i < baitPanel.transform.childCount; i++) {
            Transform baitCount = baitPanel.transform.GetChild(i).GetChild(2);
            try
            {
                baitCount.GetComponent<TextMeshProUGUI>().text = Player.Instance.Bait[i].ToString();
            }
            catch {
                baitCount.GetComponent<TextMeshProUGUI>().text = i.ToString(); //Test용 코드
            }
        }
    }

    public void OnClickCertainBait() {
        GameObject clickedBait = EventSystem.current.currentSelectedGameObject;
        // TODO: 스프라이트 변경하기
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
