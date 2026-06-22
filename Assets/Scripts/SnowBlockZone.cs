using UnityEngine;

public class SnowBlockZone : MonoBehaviour
{
    public float forwardSlowAmount = 3f;
    public float sideSlowAmount = 3f;
    public float duration = 1.5f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.ActivateSnowBlock(forwardSlowAmount, sideSlowAmount, duration);
            }
        }
    }
}