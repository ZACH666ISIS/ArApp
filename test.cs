using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public AudioSource m_MyAudioSource;
    public AudioClip[] clyp;
    public Button btn;
    private int x = 0;
    void Start()
    {
        clyp= new AudioClip[11];
        for (int i = 0; i <= 10; i++)
        {
            var audioClip = Resources.Load<AudioClip>("ArSound/Number/ar/" + i);
            clyp[i] = audioClip;
           
        }
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        m_MyAudioSource.clip = clyp[x];
        m_MyAudioSource.Play();
        x++;
    }

}
