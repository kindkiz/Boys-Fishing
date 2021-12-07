using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject story;
    [SerializeField] GameObject help;

    public void onClickStart()
    {
        SceneManager.LoadScene("Scene_1");
    }

    public void OnClickStory() {
        story.SetActive(true);
    }

    public void OnClickHelp() {
        help.SetActive(true);
    }

    public void OnClickExit() {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        clickedButton.transform.parent.gameObject.SetActive(false);
    }
}
