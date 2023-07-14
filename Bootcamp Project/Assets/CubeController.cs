using UnityEngine;

public class SelectedCubeController : MonoBehaviour
{
    public Light[] spotLights; // Se�ili olan k�pteki ���klar

    private void Start()
    {
        spotLights = GetComponentsInChildren<Light>();

        // Ba�lang��ta ���klar� kapal� olarak ayarla
        foreach (Light light in spotLights)
        {
            light.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Se�ili olan k�pte karakter z�plad���nda ���klar� a�
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
            // Se�ili olan k�pten karakter uzakla�t���nda ���klar� kapat
            foreach (Light light in spotLights)
            {
                light.enabled = false;
            }
        }
    }
}
