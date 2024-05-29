using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnInputField : MonoBehaviour
{
    public GameObject inputFieldPrefab; // Reference to the TMP input field prefab
    public GameObject prefabToSpawnBefore; // Reference to the prefab to spawn before
    private GameObject spawnedInputField; // Reference to the instantiated input field

    public void SpawnInputFieldAbovePrefab()
    {
        // Check if the prefab to spawn before is not null
        if (prefabToSpawnBefore != null)
        {
            // Find the position above the prefab to spawn before
            Vector3 spawnPosition = prefabToSpawnBefore.transform.position + new Vector3(0, 1, 0); // Adjust the Y value as needed

            // Instantiate the input field prefab at the calculated position
            spawnedInputField = Instantiate(inputFieldPrefab, spawnPosition, Quaternion.identity);

            // Make sure the input field is a child of the same parent as the prefab to spawn before
            spawnedInputField.transform.parent = prefabToSpawnBefore.transform.parent; // Assuming the parent is the same
        }
        else
        {
            Debug.LogError("Prefab to spawn before is not assigned!");
        }
    }
}
