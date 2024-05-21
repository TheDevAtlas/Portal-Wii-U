using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class TeleportOnClick : MonoBehaviour
{
	public GameObject objectToTeleportLeftClick;  // Assign in the inspector
	public GameObject objectToTeleportRightClick; // Assign in the inspector
	public Camera mainCamera; // Assign your main camera here

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		// Using Wii Remote on channel 1
		WiiU.Remote rem = WiiU.Remote.Access(0);
		// Read input state
		WiiU.RemoteState state = rem.state;

		if (Input.GetMouseButtonDown(0) || state.IsTriggered(WiiU.RemoteButton.A) )// Left mouse button
		{
			TeleportObject(objectToTeleportLeftClick);
		}

		if (Input.GetMouseButtonDown(1) || state.IsTriggered(WiiU.RemoteButton.B) )// Right mouse button
		{
			TeleportObject(objectToTeleportRightClick);
		}
	}

	void TeleportObject(GameObject obj)
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			// Position the object at the hit point, then rotate to face the normal
			obj.transform.position = hit.point;
			obj.transform.rotation = Quaternion.LookRotation(-hit.normal);

			// Move the object along the normal by 0.5 units
			obj.transform.position += hit.normal;
		}
	}
}
