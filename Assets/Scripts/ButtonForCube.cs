using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForCube : Interactable {
    public Transform spawnCube;
    public GameObject cubePrefab;
    public AudioClip sound;

    public override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        if(isInteractable)
        {
            AudioManager.instance.PlaySoundEffect(sound);
            Instantiate(cubePrefab, spawnCube.position, Quaternion.identity);
        }
        
        // Implement your interaction logic here
    }
}
