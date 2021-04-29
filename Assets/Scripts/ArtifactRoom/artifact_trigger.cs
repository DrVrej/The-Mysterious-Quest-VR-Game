/*
	Author: Vrej
	- Handles the triggers for each artifact stand
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artifact_trigger : MonoBehaviour{
    public GameObject propObject; // The actual physical artifact object
	public GameObject staticPropObject; // The fake static artifact object
	
	private bool activated = false; // The trigger does NOT run if this is set to true!
	
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	// Starts the sequence
	private IEnumerator PlaySequence() {
		// Play the artifact placement sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		//Debug.Log(audio.clip.length);
		yield return new WaitForSeconds(audio.clip.length); // Wait until the sound has finished playing...
		
		GameObject airController = GameObject.Find("ArtifactRoomTriggerMain"); // Get the main controller object
		if(airController != null){
			airController.GetComponent<artifact_trigger_main>().IncrementLevel();
		}
		Destroy(this.gameObject); // Remove this trigger
	}
	
	private void OnTriggerEnter(Collider other){
		if(activated == true){ return; }
		// If it's the object that entered the trigger...
		if (other.gameObject == propObject){
			activated = true;
			Destroy(propObject); // Kill the physical artifact!
			// Make the fake prop appear and have collision
			staticPropObject.GetComponent<MeshRenderer>().enabled = true;
			staticPropObject.GetComponent<MeshCollider>().enabled = true;
			StartCoroutine(PlaySequence());
		}
	}
}
