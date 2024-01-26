using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minRestTime = 1f; // Adjust this value for the minimum rest time
    public float maxRestTime = 3f; // Adjust this value for the maximum rest time
    public float rotationSpeed = 100f; // Adjust this value for the rotation speed

    private bool isMoving = true;
    private int rotationDirection = 1; // 1 for clockwise, -1 for counterclockwise

    void Update()
    {
        if (isMoving)
        {
            Move();
        }
        else
        {
            RotateWhileIdle();
        }
    }

    void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void RotateWhileIdle()
    {
        transform.Rotate(Vector3.up, rotationDirection * rotationSpeed * Time.deltaTime);
    }

    System.Collections.IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minRestTime, maxRestTime));
            isMoving = !isMoving;

            // Randomly determine the rotation direction
            rotationDirection = Random.value > 0.5f ? 1 : -1;
        }
    }
}
