using UnityEngine;

public class FollowPlayerZ : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 currentPosition = transform.position;

        currentPosition.z = player.position.z;

        transform.position = currentPosition;
    }
}