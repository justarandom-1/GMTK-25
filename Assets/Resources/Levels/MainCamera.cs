using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 check = Player.instance.isInBoundary();
        Vector2 playerPosition = Player.instance.getPosition();
        if (check.x == 1)
            transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z);

        if (check.y == 1)
            transform.position = new Vector3(transform.position.x, playerPosition.y, transform.position.z);
    }

    public Vector2 getPosition(float offsetX = 0, float offsetY = 0)
    {
        return transform.position + new Vector3(offsetX, offsetY, 0);
    }
}
