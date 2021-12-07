using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public void ChangeAudioSetting(bool onAudio) {
        if (onAudio)
        {
            gameObject.GetComponentInParent<AudioSource>().Play();
        }
        else {
            gameObject.GetComponentInParent<AudioSource>().Stop();
        }
    }

    public void OnClickSettingBtn() {
        gameObject.SetActive(true);
    }

    public void OnClickExitBtn() {
        gameObject.SetActive(false);
    }
}
