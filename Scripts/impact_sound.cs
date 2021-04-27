/*
	Author: Vrej
	- Used for props, adds impact sounds
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impact_sound : MonoBehaviour{
	public AudioClip soundImpact;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	private void OnCollisionEnter(Collision other) {
		// Play the sound!
		AudioSource audio = GetComponent<AudioSource>();
		if(audio != null){
			audio.clip = soundImpact;
			audio.volume = Mathf.Clamp01(other.relativeVelocity.magnitude / 50); // Increase the sound depending on how strong the impact was
			audio.Play();
		}
		//Debug.Log("Impact detected!");
	}
}
