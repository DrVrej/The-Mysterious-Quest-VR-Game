/*
	Author: Vrej
	- Handles the triggers for the slabs on each 4 corners
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class air_trigger_slab : MonoBehaviour{
	public GameObject propObject; // The actual physical prop object
	public GameObject staticPropObject; // The fake static object
	
	private bool activated = false; // If true, then it has finished its task
	
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	// Starts the sequence
	private IEnumerator PlaySequence(){
		// Play the placement sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		//Debug.Log(audio.clip.length);
		yield return new WaitForSeconds(audio.clip.length); // Wait until the sound has finished...
		
		GameObject airController = GameObject.Find("AirRoomTriggerMain"); // Get the main controller object
		if(airController != null){
			airController.GetComponent<air_trigger_main>().IncrementLevel();
		}
		Destroy(this.gameObject); // Remove this trigger
	}
	
	private void OnTriggerEnter(Collider other){
		if(activated == true){ return; } // If already activated then the job of this trigger is finished
		// If it's the object that entered the trigger...
		if (other.gameObject == propObject){
			activated = true;
			Destroy(propObject);
			// Make the fake prop appear and have collision
			staticPropObject.GetComponent<MeshRenderer>().enabled = true;
			staticPropObject.GetComponent<MeshCollider>().enabled = true;
			StartCoroutine(PlaySequence());
		}
	}
}
