using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 6f;
    [SerializeField]
    bool limitDiagonalSpeed = true;
    [SerializeField]
    float antiBumpFactor = .75f;

    MouseLook cameraLook;
    Vector3 moveDirection = Vector3.zero;
	CharacterController controller;
	float slideLimit;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraLook = GetComponentInChildren<MouseLook>();
        slideLimit = controller.slopeLimit - .1f;
    }

    // FixedUpdate is called once per fixed frame-rate frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed.
        float inputModifyFactor = (inputX != 0f && inputY != 0f && limitDiagonalSpeed) ? .7071f : 1f;

        // Rotates the player based on the camera rotation
        transform.rotation = Quaternion.Euler (0, cameraLook.currentYRotation, 0);

        // Recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
        moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
        moveDirection = transform.TransformDirection(moveDirection) * speed;
        
        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
