using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minRestTime = 1f; // Temps de repos min
    public float maxRestTime = 3f; // Temps de repos max
    public float rotationSpeed = 100f; // vitesse rotation

    private bool isMoving = true;
    private int rotationDirection = 1;

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

            // Sens de rotation aléatoire (sens horaire ou antihoraire)
            rotationDirection = Random.value > 0.5f ? 1 : -1;
        }
    }
}
