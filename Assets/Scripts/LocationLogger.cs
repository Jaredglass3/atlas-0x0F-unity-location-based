using UnityEngine;
using TMPro;
using System.Collections;

public class LocationLogger : MonoBehaviour
{
    public TextMeshProUGUI latitudeText;
    public TextMeshProUGUI longitudeText;
    public TextMeshProUGUI altitudeText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI localPositionText;

    private bool storingCoordinates = false;
    private Vector2 storedCoordinates;

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services not enabled");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Waiting for location service to initialize");
        }

        if (maxWait <= 0)
        {
            Debug.Log("Location service initialization timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

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

    public void StoreCoordinates()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            storedCoordinates = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            storingCoordinates = true;
            GPSEncoder.SetLocalOrigin(storedCoordinates);
            UpdateLocationText();
            Debug.Log("Stored coordinates: " + storedCoordinates);
        }
        else
        {
            Debug.Log("Location service not running");
        }
    }

    public void GetCurrentCoordinates()
    {
        if (!storingCoordinates)
        {
            Debug.Log("Start storing coordinates first.");
            return;
        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            Vector2 currentCoordinates = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            UpdateLocationText(); // Update text fields with current coordinates
            Debug.Log("Current coordinates: " + currentCoordinates);
        }
        else
        {
            Debug.Log("Location service not running");
        }
    }

    public void CalculateDistance()
    {
        if (!storingCoordinates)
        {
            Debug.Log("Start storing coordinates first.");
            return;
        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            Vector2 currentCoordinates = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            float distance = CalculateDistance(storedCoordinates, currentCoordinates);
            distanceText.text = "Distance: " + distance.ToString("F2") + " meters";

            // Convert GPS coordinates to Unity local position
            Vector3 localPosition = GPSEncoder.GPSToUCS(currentCoordinates);
            localPositionText.text = "Local Position: " + localPosition.ToString();

            Debug.Log("Distance: " + distance + " meters");
            Debug.Log("Local Position: " + localPosition);
        }
        else
        {
            Debug.Log("Location service not running");
        }
    }

    float CalculateDistance(Vector2 coord1, Vector2 coord2)
    {
        float earthRadius = 6371000f;
        float dLat = Mathf.Deg2Rad * (coord2.x - coord1.x);
        float dLon = Mathf.Deg2Rad * (coord2.y - coord1.y);

        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(Mathf.Deg2Rad * coord1.x) * Mathf.Cos(Mathf.Deg2Rad * coord2.x) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return earthRadius * c;
    }

    private void OnApplicationQuit()
    {
        Input.location.Stop();
    }
}
