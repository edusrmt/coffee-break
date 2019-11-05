using UnityEngine;

public class MouseLook : MonoBehaviour
{
	[SerializeField]
    float lookSensitivity = 2f;
    [SerializeField]
	float lookSmoothDamp = 0.1f;

	float xRotation;
	float yRotation;
	float currentXRotation;
	[HideInInspector]
    public float currentYRotation;
	float xRotationV;
	float yRotationV;

    void Update () {
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
    }
}
