using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Assign your prefab in the Unity Editor

    public void OnClick()
    {
        // Calculate position in front of the player
        Vector3 spawnPosition = transform.position + transform.forward * 2f; // Adjust distance as needed

        // Spawn the object
        Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }
}
