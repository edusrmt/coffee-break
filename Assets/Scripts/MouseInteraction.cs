using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour {
    Camera myCamera;
    [SerializeField]
    float interactionDistance = 2;

    void Start() {
        myCamera = GetComponent<Camera>();
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            RaycastHit hit;
            Ray ray = myCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, interactionDistance)) {
                MonoBehaviour script = isInteractable(hit.collider.gameObject);

                if (script)
                    ((Interactable)script).onInteract();
            }
        }
    }

    MonoBehaviour isInteractable(GameObject target)
    {
        MonoBehaviour[] components = target.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour component in components)
        {
            if (component is Interactable)
                return component;
        }

        return null;
    }
}
