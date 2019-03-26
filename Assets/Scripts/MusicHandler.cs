using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    bool isOn;
    // public AudioSource music;
    // public  UnityEngine.UI.Slider Volume;
    // Start is called before the first frame update
    void Start()
    {
        isOn = true;
        DontDestroyOnLoad(this.gameObject);
//        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     Volume = UnityEngine.UI.Slider.FindGameObjectWithTag("Volume");
    //     if(Volume!=null){
    //         music.Volume = Volume.value;
    //     }
    // }

    // public void mute(){
    //     if(isOn){
    //         isOn = false;
    //         music.mute = !AudioSource.mute;
    //     }else{
    //         isOn = true;
    //         music.mute = AudioSource.mute;
    //     }
    // }
}
