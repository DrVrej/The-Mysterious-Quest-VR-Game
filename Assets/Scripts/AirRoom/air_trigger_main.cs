/*
	Author: Vrej
	- The main activation trigger when the player enters the room
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class air_trigger_main : MonoBehaviour{
	public GameObject PlayerObject; // The player object
	public GameObject ArtifactBoxObject; // The artifact box object
	public GameObject ArtifactBoxTriggerObject; // The artifact box trigger object
	public GameObject ArtifactObject; // The artifact object
	public GameObject TornadoObject; // The tornado object
	public Material lampRed; // Red texture for the lamp
	public Material lampGreen; // Green texture for the lamp

	// The following are variables that hold static localized position for different objects
	private Vector3 teleportPosArtifactBox = new Vector3(-0.3899994f, -1.660137f, 17.177f);
	private Vector3 teleportPosArtifact = new Vector3(1.251999f, 18.904f, 15.551f);
	private Vector3 teleportPosTornado = new Vector3(0.07754707f, -1.587137f, 17.03402f);

	private bool entranceActivated = false; // When the player enters the room
	private int level = 0; // When it reaches 4, it will trigger the activation sequence!
	private bool finalActivated = false; // If set to true, then the player finished the task for this room

	// Start is called before the first frame update
	void Start(){

	}

	// Update is called once per frame
	void Update(){

	}

	// Plays all the necessary sequences in order
	private IEnumerator PlaySequence(){
		// Change the lamp and the light to red
		GameObject lamp = GameObject.Find("AirRoomLamp");
		if (lamp != null)
		{
			Material[] lampMats = lamp.GetComponent<MeshRenderer>().materials; // Get the array of materials
			lampMats[0] = lampRed; // Set the green material to the first item
			lamp.GetComponent<MeshRenderer>().materials = lampMats; // Now set the edited array as the material array
		}
		GameObject light = GameObject.Find("AirRoomLight");
		if (light != null)
		{
			light.GetComponent<Light>().color = new Color(1f, 0f, 0.01595974f);
		}

		// Activate the door closing sequence
		air_door door = GameObject.Find("AirRoomDoor").GetComponent<air_door>();
		door.ActivateDoor(false);
		yield return new WaitUntil(() => !door.isDoorActivated()); // Wait until the door has fully closed!

		// Teleport the artifact box and the artifact itself
		ArtifactBoxObject.transform.localPosition = teleportPosArtifactBox;
		ArtifactObject.transform.localPosition = teleportPosArtifact;

		// Teleport the tornado
		TornadoObject.transform.localPosition = teleportPosTornado;

		// Start the looping wind sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
	}
	
	// When an entity enters the trigger box
	private void OnTriggerEnter(Collider other){
		if (entranceActivated == true) { return; }
		// If it's the player that entered...
		if (other.gameObject == PlayerObject)
		{
			//Debug.Log("PLAYER ENTERED AIR ROOM");
			entranceActivated = true;
			StartCoroutine(PlaySequence());
		}
	}

	// Plays all the necessary sequences in order
	private IEnumerator PlaySequenceFinal(){
		Destroy(TornadoObject); // Destroy the tornado

		// Stop the looping wind sound
		AudioSource audio = GetComponent<AudioSource>();
		audio.Stop();

		// Activate the door closing sequence
		air_door door = GameObject.Find("AirRoomDoor").GetComponent<air_door>();
		door.ActivateDoor(true);
		yield return new WaitUntil(() => !door.isDoorActivated());

		// Change the lamp and the light to green
		GameObject lamp = GameObject.Find("AirRoomLamp");
		if (lamp != null)
		{
			Material[] lampMats = lamp.GetComponent<MeshRenderer>().materials; // Get the array of materials
			lampMats[0] = lampGreen; // Set the green material to the first item
			lamp.GetComponent<MeshRenderer>().materials = lampMats; // Now set the edited array as the material array
		}
		GameObject light = GameObject.Find("AirRoomLight");
		if (light != null)
		{
			light.GetComponent<Light>().color = new Color(0f, 1f, 0.2431f);
		}

		// Play a sound and destroy the artifact box
		AudioSource audioGlass = ArtifactBoxTriggerObject.GetComponent<AudioSource>();
		audioGlass.Play();
		Destroy(ArtifactBoxObject); // Destroy the artifact box
	}

	// Increments the level
	public void IncrementLevel(){
		if (finalActivated == true) { return; } // If it has already activated its final run then don't run it again!
		//Debug.Log("Increment!");
		level++; // Increment the level (We needi t to reach 4!)
		if (level >= 4){
			//Debug.Log("Activated!");
			finalActivated = true;
			StartCoroutine(PlaySequenceFinal());
			//Destroy(this.gameObject); // Remove this trigger
		}
	}
}
