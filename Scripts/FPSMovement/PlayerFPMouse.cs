/*
	Author: Vrej, online help
	- Camera mouse movement system
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPMouse : MonoBehaviour{
    [SerializeField] Transform objCamera; // The camera object
    [SerializeField] float sensitivity; // Mouse sensitivity
    [SerializeField] float headRotationLimit = 90f; // How much the head can rotate, too much is unrealistic, same vice versa

	private float curHeadRotation = 0f; // The current head rotation
	
	// Called once, initialize function
    void Start(){
        Cursor.visible = false; // Turn off the cursor pointer
        Cursor.lockState = CursorLockMode.Locked; // Lock the screen to the game window
    }

	// Think function, runs every tick
	void Update(){
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; // Right & Left
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * -1f; // Up & down
		/* Return values from Input.GetAxisRaw:
			- 1 = if player moves the mouse to the right/up
			- -1 = if player moves the mouse to the left/down
			- 0 = if no mouse movement was registered
		*/
		
		// Preform rotation on for right/left
        transform.Rotate(0f, x, 0f);
		
		// Preform rotation on for up/down
        curHeadRotation += y;
        curHeadRotation = Mathf.Clamp(curHeadRotation, -headRotationLimit, headRotationLimit);
        objCamera.localEulerAngles = new Vector3(curHeadRotation, 0f, 0f);
    }

}
