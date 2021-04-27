/*
	Author: Vrej
	- The main trigger for the fire room, gets activated if the artifact leaves the light area
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_trigger : MonoBehaviour{
	public GameObject fireArtifact; // The actual fire artifact
	public Material lampRed; // Red texture for the lamp
	public Material lampGreen; // Green texture for the lamp
	
	private bool startUp = false; // This is set to true after it gets triggered for the first time, to make sure the discharge sound doesn't play on game start
	
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	// If the fire artifact leaves the trigger then turn the light green
	private void OnTriggerExit(Collider other){
		if(other.gameObject != fireArtifact){ return; } // Only do it if it's the fire artifact
		startUp = true; // It now always plays the discharge sound!
		
		// Change the lamp and light to green
		GameObject lamp = GameObject.Find("FireRoom_Lamp");
		if (lamp != null){
			Material[] lampMats = lamp.GetComponent<MeshRenderer>().materials; // Get the array of materials
			lampMats[0] = lampGreen; // Set the green material to the first item
			lamp.GetComponent<MeshRenderer>().materials = lampMats; // Now set the edited array as the material array
		}
		GameObject light = GameObject.Find("FireRoom_Light");
		if (light != null){
			light.GetComponent<Light>().color = new Color(0f, 1f, 0.2431f);
		}
		
		// Play a discharge sound
		AudioSource audioGlass = GetComponent<AudioSource>();
		audioGlass.Play();
	}
	
	// If the fire artifact comes back to the trigger, then turn it red again
	private void OnTriggerEnter(Collider other){
		if(other.gameObject != fireArtifact || !startUp){ return; } // Only do it if it's the fire artifact
		
		// Change the lamp and light to red
		GameObject lamp = GameObject.Find("FireRoom_Lamp");
		if (lamp != null){
			Material[] lampMats = lamp.GetComponent<MeshRenderer>().materials; // Get the array of materials
			lampMats[0] = lampRed; // Set the green material to the first item
			lamp.GetComponent<MeshRenderer>().materials = lampMats; // Now set the edited array as the material array
		}
		GameObject light = GameObject.Find("FireRoom_Light");
		if (light != null){
			light.GetComponent<Light>().color = new Color(1f, 0f, 0.01595974f);
		}
		// Play a discharge sound
		AudioSource audioGlass = GetComponent<AudioSource>();
		audioGlass.Play();
	}
}
