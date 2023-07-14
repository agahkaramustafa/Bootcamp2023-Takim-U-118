using UnityEngine;

public class SelectedCubeController : MonoBehaviour
{
    public Light[] spotLights; // Seçili olan küpteki ýþýklar

    private void Start()
    {
        spotLights = GetComponentsInChildren<Light>();

        // Baþlangýçta ýþýklarý kapalý olarak ayarla
        foreach (Light light in spotLights)
        {
            light.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Seçili olan küpte karakter zýpladýðýnda ýþýklarý aç
            foreach (Light light in spotLights)
            {
                light.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Seçili olan küpten karakter uzaklaþtýðýnda ýþýklarý kapat
            foreach (Light light in spotLights)
            {
                light.enabled = false;
            }
        }
    }
}
