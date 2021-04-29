/*
	Author: Vrej
	- Controls the player's collision for VR headsets
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRBodyStandup : MonoBehaviour{
	Vector3 startPosition;
	Vector3 startRotation;

	public bool constrainPositionX;
	public bool constrainPositionY;
	public bool constrainPositionZ;

	public bool constrainRotationX;
	public bool constrainRotationY;
	public bool constrainRotationZ;

	// Use this for initialization
	void Start(){
		startPosition = transform.position;
		startRotation = transform.eulerAngles;
	}

	// Update is called once per frame
	void Update(){
		// We need both of these items to stand up for proper collisions:
		// GetComponent<CapsuleCollider>()
		// GetComponent<Rigidbody>()

		// For position
		Vector3 currentPosition = transform.position;
		if (constrainPositionX){
			currentPosition = new Vector3(startPosition.x, currentPosition.y, currentPosition.z);
			GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
		}
		if (constrainPositionY){
			currentPosition = new Vector3(currentPosition.x, startPosition.y, currentPosition.z);
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
		}
		if (constrainPositionZ){
			currentPosition = new Vector3(currentPosition.x, currentPosition.y, startPosition.z);
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0);
		}
		GetComponent<Rigidbody>().transform.position = currentPosition;
		GetComponent<CapsuleCollider>().transform.position = currentPosition;
		
		// For rotation
		Vector3 currentRotation = transform.eulerAngles;
		if (constrainRotationX){
			currentRotation = new Vector3(startRotation.x, currentRotation.y, currentRotation.z);
			GetComponent<Rigidbody>().angularVelocity = new Vector3(0, GetComponent<Rigidbody>().angularVelocity.y, GetComponent<Rigidbody>().angularVelocity.z);
		}
		if (constrainRotationY){
			currentRotation = new Vector3(currentRotation.x, startRotation.y, currentRotation.z);
			GetComponent<Rigidbody>().angularVelocity = new Vector3(GetComponent<Rigidbody>().angularVelocity.x, 0, GetComponent<Rigidbody>().angularVelocity.z);
		}
		if (constrainRotationZ){
			currentRotation = new Vector3(currentRotation.x, currentRotation.y, startRotation.z);
			GetComponent<Rigidbody>().angularVelocity = new Vector3(GetComponent<Rigidbody>().angularVelocity.x, GetComponent<Rigidbody>().angularVelocity.y, 0);
		}
		GetComponent<Rigidbody>().transform.eulerAngles = currentRotation;
		GetComponent<CapsuleCollider>().transform.eulerAngles = currentRotation;
	}
}
