/*
	Author: Vrej
	- Controls the glass door breaking system
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth_glass : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
		
    }

    // Update is called once per frame
    void Update(){
		
    }
	
	// Plays the glass break sound and destroys the object
	public IEnumerator glassBreak() {
		// Play glass breaking sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length - 3); // Wait just a little bit then remove the glass
		Destroy(this.gameObject);
	}
}
