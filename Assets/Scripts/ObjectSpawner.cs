using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject meshPrefab; // Assign your 3D mesh prefab in the Unity editor

    public void InstantiateMesh()
    {
        // Get the current position of the device
        Vector3 currentPosition = GetDevicePosition();

        // Instantiate the mesh at the current position
        Instantiate(meshPrefab, currentPosition, Quaternion.identity);
    }

    // Function to get the current position of the device
    private Vector3 GetDevicePosition()
    {
        // Example: You may use GPS, accelerometer, or other sensors to get the position
        // For simplicity, let's assume a fixed position for demonstration
        return new Vector3(0, 0, 0); // Change this to get the actual device position
    }
}