using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BgmController : MonoBehaviour
{
    public static BgmController instance=null;
    public AudioClip yuttari;
    AudioSource audioSource;

    public float BgmVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        audioSource.volume = BgmVolume;
    }
    private void Awake(){
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }
}
