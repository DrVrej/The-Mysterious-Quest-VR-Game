/*
	Author: Vrej
	- Used for props, adds impact sounds
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impact_sound : MonoBehaviour{
	public AudioClip soundImpact; // The impact sound that it plays when it collides with something

	private float nextImpactT; // Used to delay the sound, we don't want it to play the impact sound 100x per second!

	// Start is called before the first frame update
	void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //print(Time.fixedTime);
    }
	
	private void OnCollisionEnter(Collision other) {
		// Play the sound!
		AudioSource audio = GetComponent<AudioSource>();
		if(audio != null && nextImpactT < Time.fixedTime){
			nextImpactT = Time.fixedTime + 0.2f; // Add a delay
			audio.clip = soundImpact;
			audio.volume = Mathf.Clamp01(other.relativeVelocity.magnitude / 50); // Increase the sound depending on how strong the impact was
			audio.Play();
		}
		//Debug.Log("Impact detected!");
	}
}
