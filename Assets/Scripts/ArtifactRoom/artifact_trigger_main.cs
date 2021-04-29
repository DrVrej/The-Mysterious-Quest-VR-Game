/*
	Author: Vrej
	- Handles the main code for the artifact room
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artifact_trigger_main : MonoBehaviour{
	public GameObject doorLeft; // The left door to open
	public GameObject doorRight; // The right door to open
	private bool activated = false; // If set to true, then the player put all 4 of the artifacts
	private int level = 0; // When it reaches 4, it will trigger the activation sequence!
	
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	// Increments the level
	public void IncrementLevel(){
		if (activated == true) { return; }
		//Debug.Log("Increment!");
		level++; // Increment to reach 4 eventually
		if (level >= 4){
			//Debug.Log("Activate!");
			activated = true;
			// Make both of the doors start moving!
			doorLeft.GetComponent<artifact_door>().ActivateDoor();
			doorRight.GetComponent<artifact_door>().ActivateDoor();
			//Destroy(this.gameObject); // Remove this trigger
		}
	}
}
