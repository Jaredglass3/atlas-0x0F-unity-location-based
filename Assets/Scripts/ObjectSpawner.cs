using UnityEngine;

public class MeshInstantiator : MonoBehaviour
{
    public GameObject meshTextPrefab; // Assign your 3D mesh with TextMeshPro text prefab in the Unity editor
    public float spawnDistance = 2f; // Distance in front of the camera to spawn the object

    public void InstantiateMesh()
    {
        Debug.Log("InstantiateMesh() method called.");

        // Get the spawn position
        Vector3 spawnPosition = GetSpawnPosition();
        Debug.Log("Spawn position: " + spawnPosition);

        // Instantiate the mesh with TextMeshPro text at the spawn position
        Instantiate(meshTextPrefab, spawnPosition, Quaternion.identity);
    }

    // Function to calculate the spawn position in front of the camera
    private Vector3 GetSpawnPosition()
    {
        // Get reference to the main camera in the scene
        Camera mainCamera = Camera.main;

        // Check if the main camera is available
        if (mainCamera != null)
        {
            // Calculate spawn position in front of the camera
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnDistance;
            return spawnPosition;
        }
        else
        {
            Debug.LogError("Main camera not found in the scene.");
            // If main camera is not found, return a default position
            return Vector3.zero;
        }
    }
}
