/*
	Author: Vrej
	- A camera test I coded, it didn't work out well for mixed reality headset
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRBodyCollision : MonoBehaviour{
	
	public Transform head;
	public Transform feet;
	
    // Start is called before the first frame update
    void Start(){
		
    }

    // Update is called once per frame
    void Update(){
		gameObject.transform.position = new Vector3(head.position.x, feet.position.y, head.position.z);
    }
}
