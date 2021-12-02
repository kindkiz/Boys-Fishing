using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInfoUIManager : MonoBehaviour
{
    [SerializeField] GameObject boy;
    [SerializeField] GameObject ship;
    [SerializeField] GameObject rod;
    [SerializeField] GameObject line;
    [SerializeField] GameObject reel;

    public void OnClickUserInfo() {
        ChangeUserInfo();
        gameObject.SetActive(true);
    }

    public void OnClickExitBtn() {
        gameObject.SetActive(false);
    }

    private void ChangeUserInfo() {
        // TODO: boy -> level 바꾸기
        // boy.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Player.Instance.GetAverageLevel();

        // TODO: ship, rod, line, reel -> img, level, name 바꾸기
    }
}
