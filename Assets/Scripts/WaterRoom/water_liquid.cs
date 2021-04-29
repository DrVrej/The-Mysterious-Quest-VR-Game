/*
	Author: Vrej
	- Script to make the water go down and remove
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_liquid : MonoBehaviour{
	private bool waterActivated = false; // If true, it will go down and remove itself
	private float startPosY; // Get the starting y, used to calculate if the water has finished going down
	
    // Start is called before the first frame update
    void Start(){
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update(){
		//Debug.Log(startPosY);
		// If activated then move the water down!
        if(waterActivated == true){
			Vector3 curPos = transform.position;
			transform.position = new Vector3(curPos.x, curPos.y - 0.001f, curPos.z);
			// If we have reached down enough then remove the water
			if(transform.position.y < (startPosY - 1.25f)){
				Destroy(this.gameObject); // Destroy the water
			}
		}
    }
	
	// Activates the water to start moving
	public void ActivateWater(){
		waterActivated = true;
	}
}
