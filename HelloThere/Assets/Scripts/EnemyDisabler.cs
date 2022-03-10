using UnityEngine;

public class EnemyDisabler : MonoBehaviour
{
    private const float maxDistance = 20;
    private Transform playerTransform;
    
    

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("CheckDistance", 2, 0.2f);
    }



    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > maxDistance)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
