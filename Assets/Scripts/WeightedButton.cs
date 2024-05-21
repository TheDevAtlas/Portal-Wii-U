using UnityEngine;

public class WeightedButton : MonoBehaviour
{
    public Vector3 unpressedPosition;
    public Vector3 pressedPosition;
    public float lerpSpeed = 0.1f;
    public Material pressedMaterial;
    private Material originalMaterial;
    public Renderer r;

	public GameObject mainObj;

    public Trigger t;

    private void Start()
    {
		unpressedPosition += mainObj.transform.position;
		pressedPosition += mainObj.transform.position;

        originalMaterial = r.material;
    }

    private void Update()
    {
        // Check if there is something on top of the button
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale);
        bool isPressed = false;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject) // Exclude itself from the check
            {
                if(collider.gameObject.tag == "Pickupable" || collider.gameObject.tag == "Player")
                {
                    isPressed = true;
                    break;
                }
                
            }
        }

        // Interpolate between positions
        

        // Change material if pressed
        if (isPressed)
        {
            r.material = pressedMaterial;

            Vector3 targetPosition = pressedPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

            t.isPressed = true;
        }
        else
        {
            r.material = originalMaterial;

            Vector3 targetPosition = unpressedPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

            t.isPressed = false;
        }
    }
}
