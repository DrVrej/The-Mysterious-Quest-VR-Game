/*
	Author: Vrej, online help
	- Keyboard movement system includes walking, running, crouching, jumping
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPMovement : MonoBehaviour {
    [SerializeField] private float walkSpeed = 50; // Max walk speed
    [SerializeField] private float runMultiplier = 1.5f; // Walk speed * x, where x is this variable, used for running
	[SerializeField] float crouchHeight = 0.5f; // Height of the player when it crouches
	[SerializeField] float jumpHeight = 5.0f; // How high the player jumps
	
	private Rigidbody objRigidBody; // Saved object for optimization purposes
	private Vector3 orgScale; // Saved vector of the original scale of the player
	private int curCollisions = 0; // If 0 or less, it can't jump!
	
	// Called once, initialize function
    void Start() {
        objRigidBody = GetComponent<Rigidbody>(); // The object's physics component
		orgScale = transform.localScale; // Saved vector of the original scale of the player
    }
	
	// Think function, runs every tick
    void Update() {
		// Movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
		/* Return values from Input.GetAxisRaw:
			- 1 = if player presses D or Right arrow key
			- -1 = if player presses A or Left arrow key
			- 0 = if those keys are not pressed
		*/
        Vector3 setPos = transform.right * x + transform.forward * z;
		
		// Movement walkSpeed
        float actualSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) { // Shift key is pressed - to run
            actualSpeed *= runMultiplier;
        }
        objRigidBody.MovePosition(transform.position + setPos.normalized * actualSpeed * Time.deltaTime);
		
		// Jumping, if curCollisions is 0 or less, it can't jump!
		if (Input.GetKeyDown(KeyCode.Space) && curCollisions > 0) {
            objRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
		
		// Crouching
		Vector3 newScale = new Vector3(transform.localScale.x, orgScale.y, transform.localScale.z);
        if (Input.GetKey(KeyCode.LeftControl)) {
            newScale.y = crouchHeight;
        }
        transform.localScale = newScale;
    }

	private void OnCollisionExit(Collision collision) {
        curCollisions--; // Decrease
    }
	
    private void OnCollisionEnter(Collision collision) {
        curCollisions++; // Increase
    }
}
