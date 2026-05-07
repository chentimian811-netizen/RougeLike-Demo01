using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menue : MonoBehaviour
{
    public void GameStart()
    {
        // 直接查找AudioManager物体上的AudioSource组件
        GameObject AudioManager = GameObject.Find("AudioSource");
        if (AudioManager != null)
        {
            AudioSource bgmAudio = AudioManager.GetComponent<AudioSource>();

            if (bgmAudio != null && bgmAudio.isPlaying)
            {
                bgmAudio.Pause();
                Debug.Log("开始游戏，暂停背景音乐");
            }
        }

        // 切换关卡
        SceneManager.LoadScene("Level_1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}