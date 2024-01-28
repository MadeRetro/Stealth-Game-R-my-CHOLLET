using Unity.VisualScripting;
using UnityEngine;

public class BACKK : MonoBehaviour
{
    public Transform playerTransform; 
    public string enemyTag = "Enemy";

    public HUDManager backstabTextDisplay;
 



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




        // Repérer tous les ennemis
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemyObject in enemies)
        {
            Transform enemyTransform = enemyObject.transform;

            // Différence entre les positions du joueur et de l'ennemi
            Vector3 playerToEnemy = enemyTransform.position - playerTransform.position;
            Vector3 EnemyToPlayer = playerTransform.position - enemyTransform.position;

            // Angle entre le vecteur de direction du joueur et le vecteur joueur --> ennemi
            float angle = Vector3.Angle(playerTransform.forward, playerToEnemy);
            float anglenemy = Vector3.Angle(enemyTransform.forward, EnemyToPlayer);

            // Distance entre le joueur et l'ennemi
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
            




            // Conditions pour être spotted
            if (distance < distancevue && anglenemy < 30f)
            {
                backstabTextDisplay.ShowspottedTxt();
                backstabTextDisplay.HideBackstabTxt();

            }



            if (dotProduct > 0.8f && distance < distancemin && angle < anglemin && isBehind )
            {
                backstabTextDisplay.ShowBackstabTxt();
                backstabTextDisplay.HidespottedTxt();

                // Ennemi le plus proche
                GameObject nearestEnemy = GetNearestEnemy();

                
                if (Input.GetMouseButtonDown(0))
                {
                    // KILL
                    Destroy(nearestEnemy);
                }



            }



        }




    }
}