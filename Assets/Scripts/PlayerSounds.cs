using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

	public AudioClip slowTime;
	public AudioClip speedTime;
	private AudioSource audiosrc;

    // Start is called before the first frame update
    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AudioChoice(string command){
    	if(audiosrc.isPlaying == false){
	    	if(command == "slowtime"){
	    		audiosrc.PlayOneShot(slowTime, 0.7f);
	    	}else if(command == "speedtime"){
	    		audiosrc.PlayOneShot(speedTime, 0.7f);
	    	}
    	}
    }
}
