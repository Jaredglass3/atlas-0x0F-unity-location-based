using UnityEngine;
using TMPro;
using System.Collections;

public class LocationLogger : MonoBehaviour
{
    public TextMeshProUGUI latitudeText;
    public TextMeshProUGUI longitudeText;
    public TextMeshProUGUI altitudeText;

    private bool storingCoordinates = false;

    IEnumerator Start()
    {
        // First, check if location services are enabled on the device
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services not enabled");
            yield break; // Exit coroutine
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Waiting for location service to initialize");
        }

        // Check if initialization has timed out
        if (maxWait <= 0)
        {
            Debug.Log("Location service initialization timed out");
            yield break; // Exit coroutine
        }

        // Check if service has failed to initialize
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break; // Exit coroutine
        }

        // Initial update if needed
        UpdateLocationText();
    }

    void UpdateLocationText()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            latitudeText.text = "Latitude: " + Input.location.lastData.latitude.ToString();
            longitudeText.text = "Longitude: " + Input.location.lastData.longitude.ToString();
            altitudeText.text = "Altitude: " + Input.location.lastData.altitude.ToString();
        }
        else
        {
            Debug.Log("Location service not running");
        }
    }

    // Button click event for storing current coordinates
    public void StoreCoordinates()
    {
        storingCoordinates = true;
        UpdateLocationText(); // Update the text when storing coordinates
    }

    // Button click event for getting current coordinates after moving
    public void GetCurrentCoordinates()
    {
        if (!storingCoordinates)
        {
            Debug.Log("Start storing coordinates first.");
            return;
        }

        UpdateLocationText();
    }

    // Stop location service when the application is stopped
    private void OnApplicationQuit()
    {
        Input.location.Stop();
    }
}
