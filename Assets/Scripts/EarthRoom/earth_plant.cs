/*
	Author: Vrej
	- Controls the plant growing system after placing a seed and watering
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth_plant : MonoBehaviour{
	private MeshRenderer mesh; // The saved mesh renderer (for optimizations)
	private bool growing = false; // If true then the plant is growing (sequence)
	private float curScale; // The scale of the plant, used to calculate if the plant should stop growing
	
    // Start is called before the first frame update
    void Start(){
        mesh = GetComponent<MeshRenderer>(); // Save the renderer object for optimization purposes
		mesh.enabled = false; // Make it invisible initially
		curScale = transform.localScale.y;  // Save the starting scale
    }

    // Update is called once per frame
    void Update(){
		// If it's supposed to grow and then grow up to 100 units!
        if(growing == true && curScale < 100.0f){
			//Debug.Log("Plant is growing!");
			curScale = transform.localScale.y + 0.27285f;
			transform.localScale = new Vector3(transform.localScale.x, curScale, transform.localScale.z); // Grow!
		}
    }
	
	// Makes the plant activate the growing system
	public void startGrowing(){
		// Start rendering and grow!
		mesh.enabled = true;
		growing = true;
	}
}
