using Unity.VisualScripting;
using UnityEngine;

public class BACKK : MonoBehaviour
{
    public Transform playerTransform; // Référence au transform du joueur
    public string enemyTag = "Enemy"; // Tag pour les ennemis

    public HUDManager backstabTextDisplay;
 

    public Animator enemyAnimator; // Référence à l'Animator de l'ennemi

    public float anglemin = 10f;
    public float distancemin = 2f;
    public float distancevue = 5f;
    public float anglemax = 10f;
    void Update()
    {
        backstabTextDisplay.HideBackstabTxt();

        backstabTextDisplay.HidespottedTxt();


        GameObject GetNearestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearestEnemy = null;
            float nearestDistance = float.MaxValue;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy;
        }




        // Récupérer tous les ennemis avec le tag spécifié
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemyObject in enemies)
        {
            Transform enemyTransform = enemyObject.transform;

            // Vecteur de différence entre les positions du joueur et de l'ennemi
            Vector3 playerToEnemy = enemyTransform.position - playerTransform.position;
            Vector3 EnemyToPlayer = playerTransform.position - enemyTransform.position;

            // Calcul de l'angle entre le vecteur de direction du joueur et le vecteur joueur-vers-ennemi
            float angle = Vector3.Angle(playerTransform.forward, playerToEnemy);
            float anglenemy = Vector3.Angle(enemyTransform.forward, EnemyToPlayer);

            // Calcul de la distance entre le joueur et l'ennemi
            float distance = playerToEnemy.magnitude;


            // Direction joueur et ennemi
            Vector3 playerDirection = playerTransform.forward.normalized;
            Vector3 enemyDirection = playerToEnemy.normalized;


            // Produit scalaire : Regardent dans la même direction ?
            float dotProduct = Vector3.Dot(playerDirection, enemyDirection);

            // Produit
            Vector3 crossProduct = Vector3.Cross(playerDirection, playerToEnemy);

            // cross.z doit être positif
            bool isBehind = crossProduct.z > -0.05f;
            bool isFront = crossProduct.z < -0.05f;






            if (distance < distancevue && anglenemy < 30f)
            {
                backstabTextDisplay.ShowspottedTxt();
            }



            else if (dotProduct > 0.8f && distance < distancemin && angle < anglemin && isBehind)
            {
                backstabTextDisplay.ShowBackstabTxt();

                // Find the nearest enemy
                GameObject nearestEnemy = GetNearestEnemy();

                // Check if the nearest enemy is within backstab range and can be backstabbed
                if (Input.GetMouseButtonDown(0))
                {
                    // Perform the backstab action (e.g., destroy the enemy)
                    Destroy(nearestEnemy);
                }



            }



        }




    }
}