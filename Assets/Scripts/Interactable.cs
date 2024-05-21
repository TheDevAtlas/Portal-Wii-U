using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class Interactable : MonoBehaviour
{
    public bool isInteractable;
    // Function to be called when the player interacts with the object
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        // Implement your interaction logic here
    }

    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void Update()
    {
		WiiU.Remote rem = WiiU.Remote.Access(0);
		WiiU.RemoteState state = rem.state;

		if (Input.GetKeyDown(KeyCode.E) || state.IsPressed(WiiU.RemoteButton.NunchukZ))
            {
                // Call the Interact function
                Interact();
            }
        
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        isInteractable = true;
        // Check if the other collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger of " + gameObject.name);
            // You can add additional logic here if needed
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
        // Check if the other collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger of " + gameObject.name);
            // You can add additional logic here if needed
        }
    }
}
