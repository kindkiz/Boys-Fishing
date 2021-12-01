using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoUIManager : MonoBehaviour
{
    [SerializeField] GameObject boy;
    [SerializeField] GameObject boat;
    [SerializeField] GameObject rod;
    [SerializeField] GameObject line;
    [SerializeField] GameObject reel;

    public void OnClickUserInfo() {
        gameObject.SetActive(true);
    }

    public void OnClickExitBtn() {
        gameObject.SetActive(false);
    }

    private void ChangeUserInfo() { 
        // TODO: boy -> level 바꾸기
        // TODO: boat, rod, line, reel -> img, level, name 바꾸기
    }
}
