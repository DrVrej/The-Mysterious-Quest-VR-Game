/*
	Author: Vrej
	- Main script controller for the water room, activates the sequence as well
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_main : MonoBehaviour{
	public Material lampGreen; // Green texture for the lamp
	private int level = 0; // When it reaches 3, it will trigger the activation sequence!
	private bool activated = false; // If true, then its job is done!
	
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	// Starts the sequence
	private IEnumerator PlaySequence() {
		// Activate the water!
		water_liquid liquid = GameObject.Find("WaterUnderGroundLiquid").GetComponent<water_liquid>();
		liquid.ActivateWater();
		GameObject.Find("WaterUnderGroundLiquid").GetComponent<AudioSource>().Play();
		yield return new WaitUntil(() => liquid == null); // Wait until the water has been removed
		
		// Turn the lamp and the light to green!
		GameObject lamp = GameObject.Find("WaterUnderGround_Lamp");
		if(lamp != null){
			Material[] lampMats = lamp.GetComponent<MeshRenderer>().materials; // Get the array of materials
			lampMats[0] = lampGreen; // Set the green material to the first item
			lamp.GetComponent<MeshRenderer>().materials = lampMats; // Now set the edited array as the material array
		}
		GameObject light = GameObject.Find("WaterUnderGround_Light");
		if(light != null){
			light.GetComponent<Light>().color = new Color(0f, 1f, 0.2431f);
		}
	}
	
	// Increments the level
	public void IncrementLevel(){
		if(activated == true){ return; } // It has already been activate, return!
		//Debug.Log("Increment!");
		level++; // Increment until it reaches 3
		if(level >= 3){
			//Debug.Log("Activate!");
			activated = true;
			StartCoroutine(PlaySequence());
			// Get the glass/invisible object and destroy it
			GameObject glass = GameObject.Find("WaterUnderGroundGlass");
			if(glass != null){
				Destroy(glass);
			}
		}
	}
}
