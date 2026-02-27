using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    [Header("Dead Zone")]
    public float upperThreshold = 2f; // Horizontal dead zone threshold
    public float lowerThreshold = -2f; // Horizontal dead zone threshold

    [Header("Camera Follow Speed")]
    public float followSpeed = 5f; // Speed at which the camera follows the player

    public float minY = -4.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player) return;

        float camY = transform.position.y;
        float playerY = player.position.y;

        float difference = playerY - camY;
        float targetY = camY;

        if (difference > upperThreshold)
            targetY = playerY - upperThreshold;
        else if (difference < lowerThreshold)
            targetY = playerY - lowerThreshold;

        Vector3 newPos = transform.position;
        newPos.y = Mathf.Lerp(camY, targetY, followSpeed * Time.deltaTime);

        // Clamp so camera never scrolls below the level
        newPos.y = Mathf.Max(newPos.y, minY);

        transform.position = newPos;
    }
}
