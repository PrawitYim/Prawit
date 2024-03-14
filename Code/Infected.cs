using UnityEngine;

public class Infected : MonoBehaviour
{
    public float slowedSpeed; // Adjust this to control the slowed speed

    private float normalSpeed; // Store the player's normal speed

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.tag == ("PP"))
        {
            
            PlayerController.instance.movespeed -= slowedSpeed; 
            Debug.Log("Slow");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("PP"))
        {

            PlayerController.instance.movespeed += slowedSpeed;

            Debug.Log("Slow");
        }
    }
}
