using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    /*    // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }*/

    public void ChangeAudioSetting(bool onAudio) {
        gameObject.GetComponentInParent<AudioSource>().enabled = onAudio;
    }

    public void OnClickSettingBtn() {
        gameObject.SetActive(true);
    }

    public void OnClickExitBtn() {
        gameObject.SetActive(false);
    }
}
