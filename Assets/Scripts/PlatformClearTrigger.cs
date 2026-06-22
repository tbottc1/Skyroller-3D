using UnityEngine;

public class PlatformClearTrigger : MonoBehaviour
{
    private bool used = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (used)
        {
            return;
        }

        if (collider.CompareTag("Player"))
        {
            used = true;
            FindAnyObjectByType<GameManager>().AddPlatformBonus();
        }
    }
}