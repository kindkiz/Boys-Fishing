using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public void ChangeAudioSetting(bool onAudio) {
        gameObject.GetComponentInParent<AudioSource>().mute = !onAudio;
    }

    public void OnClickSettingBtn() {
        gameObject.SetActive(true);
    }

    public void OnClickExitBtn() {
        gameObject.SetActive(false);
    }
}
