/*
	Author: Vrej
	- Script to make the big 2 doors move for the tressure room
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artifact_door : MonoBehaviour{
	public int moveDir = 0; // 0 = Left, 1 = Right
	private bool doorActivated = false; // If true, the door will move
	private int doorActivatedPhase = 1; // Current phase of the door: 1 = moving back, 2 = moving to the side
	private float startPosX; // Get the starting x, used to calculate if the door has finished moving
	private float startPosZ; // Get the starting z, used to calculate if the door has finished moving
	
    // Start is called before the first frame update
    void Start(){
		startPosX = transform.position.x;
		startPosZ = transform.position.z;
    }
	
    // Update is called once per frame
    void Update(){
		// If activated then move the door!
        if(doorActivated == true){
			Vector3 curPos = transform.position;
			if(doorActivatedPhase == 1){ // Move back using the Z axis
				transform.position = new Vector3(curPos.x, curPos.y, curPos.z + 0.005f);
				// If we have moved back enough, then change it to the second phase!
				if(Mathf.Abs(transform.position.z - startPosZ) > 0.635f){
					doorActivatedPhase = 2;
				}
			}else if(doorActivatedPhase == 2){ // Move to the side using the X axis
				transform.position = new Vector3(curPos.x + (moveDir == 0 ? -0.08f : 0.08f), curPos.y, curPos.z);
				// Handle the case when it closes
				if(Mathf.Abs(transform.position.x - startPosX) > 5.428f){
					doorActivated = false;
				}
			}
		}
    }
	
	// Returns true if the door is activated
	public bool isDoorActivated(){
		return doorActivated;
	}
	
	// Activates the door to start moving	
	public void ActivateDoor(){
		// Play the opening sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		
		doorActivated = true;
		doorActivatedPhase = 1; // Set to the move back phase first then it automatically transitions to the next phase
	}
}
