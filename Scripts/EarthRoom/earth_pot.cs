/*
	Author: Vrej
	- Controls the planting system for the Earth room
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth_pot : MonoBehaviour{
	public AudioClip soundSeed; // The sound it plays when the seed is placed
	public AudioClip soundWater; // The sound it plays when the water can has been used on the soil
	public AudioClip soundPlant; // The sound it plays when the plant is growing
	public Material lampGreen; // Green texture for the lamp
	
	private int curStatus = 0; // The current status | 0 = Has nothing! | 1 = Has seed | 2 = Has seed & water can
	
    // Start is called before the first frame update
    void Start(){
		
    }

    // Update is called once per frame
    void Update(){
		
    }
	
	// Plays all the sounds in order, than destroys the glass
	private IEnumerator PlaySD_Plant(){
		// Play the watering sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = soundWater;
		audio.Play();
		yield return new WaitUntil(() => !audio.isPlaying); // Wait until the watering sound is finished
		
		// Then play the plant growing sound and grow the plant!
		audio.clip = soundPlant;
		audio.Play();
		// Make the garlic plant grow!
		GameObject garlic = GameObject.Find("EarthGarlicPlant");
		if(garlic != null){
			garlic.GetComponent<earth_plant>().startGrowing();
		}
		yield return new WaitUntil(() => !audio.isPlaying); // Once the sound has finished playing then the plant has also finished growing, continue...
		
		//audio.clip = soundGlass;
		//audio.Play();
		// Break the artifact glass door
		GameObject door = GameObject.Find("EarthArtifectBox_Door");
		if(door != null){
			StartCoroutine(door.GetComponent<earth_glass>().glassBreak());
			//Destroy(door);
		}
		
		// Turn the lamp and the light to green!
		GameObject lamp = GameObject.Find("EarthArtifectBox_Lamp");
		if(lamp != null){
			Material[] lampMats = lamp.GetComponent<MeshRenderer>().materials; // Get the array of materials
			lampMats[0] = lampGreen; // Set the green material to the first item
			lamp.GetComponent<MeshRenderer>().materials = lampMats; // Now set the edited array as the material array
		}
		GameObject light = GameObject.Find("EarthArtifectBox_Light");
		if(light != null){
			light.GetComponent<Light>().color = new Color(0f, 1f, 0.2431f);
		}
	}

	private void OnCollisionEnter(Collision other){
		// Everything is already done so just return
		if(curStatus == 2){
			return;
		}
		
		// Seed placement (step 1)
		if(other.gameObject.name == "EarthSeed" && curStatus == 0){
			curStatus = 1;
			Destroy(other.gameObject);
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = soundSeed;
			audio.Play();
			//Debug.Log("Got seed!");
		// Watering can (step 2)
		}else if(other.gameObject.name == "EarthWateringCan" && curStatus == 1){
			curStatus = 2;
			Destroy(other.gameObject);
			StartCoroutine(PlaySD_Plant());
			//Debug.Log("Got water can!");
		}
		//this.GetComponent<MeshRenderer>().material.color = Color.red;
		//this.transform.position = this.transform.position + new Vector3(0f, 2f, 0f);
	}
}
