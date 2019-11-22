using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public bool holdingObject = false;

    [SerializeField]
    Transform cupPlacement;

    GameObject heldObject;

    public GameObject GetHeldObject () {
        if (holdingObject) {
            holdingObject = false;
            GameObject obj = heldObject;
            heldObject = null;
            return obj; 
        } else
            return null;
    }

    public bool PickUpObject (GameObject obj) {
        if (holdingObject)
            return false;

        obj.transform.parent = cupPlacement;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        holdingObject = true;
        heldObject = obj;

        return true;
    }
}
