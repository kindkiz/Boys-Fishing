using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BucketUIManager : MonoBehaviour
{
    [SerializeField] GameObject fishScrollView;
    [SerializeField] GameObject baitPanel;
    [SerializeField] GameObject fishBtn;
    [SerializeField] GameObject baitBtn;

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

        // 현재 내 물고기 인벤토리 보여주기 (이미지 크기도 조절)
        
    }

    public void OnClickBaitTab() {
        fishScrollView.SetActive(false);
        baitPanel.SetActive(true);

        // 현재 내 미끼 정보 보여주기. 갯수 바꾸기.
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
