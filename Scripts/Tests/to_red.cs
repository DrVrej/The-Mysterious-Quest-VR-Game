/*
	Author: Vrej
	- Just a test file for cubes
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class to_red : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	
	private void OnCollisionEnter(Collision other){
		Debug.Log("I have collided with something!");
		this.GetComponent<MeshRenderer>().material.color = Color.red; // Turn red
		this.transform.position = this.transform.position + new Vector3(0f, 2f, 0f); // Teleport up
	}
}
