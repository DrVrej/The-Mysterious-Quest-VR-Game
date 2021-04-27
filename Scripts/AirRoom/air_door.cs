/*
	Author: Vrej
	- Script to make the door move in the air room
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class air_door : MonoBehaviour{
	private bool doorActivated = false; // If true, the door will move
	private bool doorActivatedBack = false; // If true, the door will move backwards!
	private float startPosZ; // Get the starting z, used to calculate if the door has finished moving
	
    // Start is called before the first frame update
    void Start(){
		startPosZ = transform.position.z;
    }

    // Update is called once per frame
    void Update(){
		// If activated then move the door!
        if(doorActivated == true){
			Vector3 curPos = transform.position;
			if(doorActivatedBack == true){ // Backwards
				transform.position = new Vector3(curPos.x, curPos.y, curPos.z - 0.03f);
			}else{ // Normal (Forward)
				transform.position = new Vector3(curPos.x, curPos.y, curPos.z + 0.08f);
			}
			// Handle the case when it closes
			if(Mathf.Abs(transform.position.z - startPosZ) > 3.91598f){
				doorActivated = false;
				doorActivatedBack = false;
			}
		}
    }
	
	// Returns true if the door is activated
	public bool isDoorActivated(){
		return doorActivated;
	}
	
	// Activates the door to start moving
	public void ActivateDoor(bool back){
		// Play the moving sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		doorActivated = true; // Makes the door move
		// Only if we are going to move backwards (opening)
		if(back == true){
			// Set the start pos to current since the door is closed, we need it to calculate based on its new start pos
			startPosZ = transform.position.z;
			doorActivatedBack = true;
		}
	}
}
