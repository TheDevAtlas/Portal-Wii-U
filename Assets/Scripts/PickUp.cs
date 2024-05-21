using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class PickUp : MonoBehaviour {

    public float pickupDistance = 3f; // Maximum distance to pick up the object
    public Transform holdPosition; // Position to hold the object when picked up
    public KeyCode pickupKey = KeyCode.E; // Key to pick up/drop the object

    private Transform objectToPickup;
    private bool isPickedUp = false;

    void Update()
    {
		WiiU.Remote rem = WiiU.Remote.Access(0);
		WiiU.RemoteState state = rem.state;

		if (!isPickedUp && (Input.GetKeyDown(pickupKey) || state.IsPressed(WiiU.RemoteButton.NunchukZ))) // If not holding object and press pickup key
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupDistance))
            {
                if (hit.collider.CompareTag("Pickupable")) // If looking at a pickupable object
                {
                    Pick(hit.transform);
                }
            }
        }
        else if (isPickedUp && Input.GetKeyDown(pickupKey)) // If holding object and press pickup key
        {
            Drop();
        }

        if(isPickedUp)
        {
            objectToPickup.localPosition = Vector3.Lerp(objectToPickup.localPosition,new Vector3(0, 0, 0),0.125f);
            objectToPickup.localRotation = Quaternion.Lerp(objectToPickup.localRotation,Quaternion.identity, 0.125f);
        }
    }

    void Pick(Transform obj)
    {
        objectToPickup = obj;
        objectToPickup.GetComponent<Rigidbody>().isKinematic = true; // Disable physics while holding
        //objectToPickup.position = holdPosition.position; // Move object to hold position
        objectToPickup.parent = holdPosition; // Parent object to hold position
        /*objectToPickup.localPosition = new Vector3(0, 0, 0);
        objectToPickup.localRotation = Quaternion.identity;*/
        isPickedUp = true;
    }

    void Drop()
    {
        objectToPickup.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
        objectToPickup.parent = null; // Unparent object
        objectToPickup = null;
        isPickedUp = false;
    }
}
