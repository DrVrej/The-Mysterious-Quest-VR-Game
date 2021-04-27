/*
	Author: Vrej
	- Handles the triggers for the 3 valve sites
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_trigger : MonoBehaviour{
	public GameObject ValveObject; // The valve object to accept as trigger
	public GameObject ValveObjectStatic; // The static valve object to enable drawing
	
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	// Starts the sequence
	private IEnumerator PlaySound(){
		// Play metal turning sound for the valve
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length); // Wait until the sound has finished playing...
		
		Destroy(this.gameObject); // Remove this trigger
	}
	
	private void OnTriggerEnter(Collider other){
		//Debug.Log("TRIGGER ENTERED");
		// If the collider is the correct valve prop...
		if(other.gameObject == ValveObject){
			//Debug.Log("Trigger activated!");
			Destroy(ValveObject); // Remove the valve prop
			GameObject valveStatic = GameObject.Find(ValveObjectStatic.name); // Get & enable drawing for the static version of the valve
			if(valveStatic != null){
				valveStatic.GetComponent<MeshRenderer>().enabled = true; // Start rendering the fake static object!
			}
			GameObject valveController = GameObject.Find("WaterRoomTriggers"); // Get the main controller object
			if(valveController != null){
				valveController.GetComponent<water_main>().IncrementLevel();
			}
			StartCoroutine(PlaySound());
		}
	}
}
