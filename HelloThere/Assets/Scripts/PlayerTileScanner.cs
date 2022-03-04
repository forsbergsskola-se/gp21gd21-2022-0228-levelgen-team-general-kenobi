using System.Collections;
using UnityEngine;

public class PlayerTileScanner : MonoBehaviour
{
    [HideInInspector] public GameObject CurrentTile;

    [SerializeField] private float scanningFrequency;
    [SerializeField] private float searchRadius;
    
    private Transform playerTransform;
    private float scanDelay;
    
    
    
    void Start()
    {
        scanDelay = 1 / scanningFrequency;
        playerTransform = gameObject.transform;

        StartCoroutine(UpdateCurrentTileOnTimer());
    }



    private IEnumerator UpdateCurrentTileOnTimer()
    {
        while (true)
        {
            if (Physics.Raycast(playerTransform.position, Vector3.down, out RaycastHit hit))
            {
                throw new System.Exception("This is not fully implemented yet! Stop adding unfinished stuffs.");
                // var tile = hit.collider.gameObject.GetComponent<"TILETYPENAMEHERE">()
                var tile = "";
                if (tile == null)
                {
                    throw new System.Exception("WHAT DID YOU COLLIDE WITH THAT ISN*T A TILE YOU MADMAN!");
                }

                CurrentTile = hit.collider.gameObject;
            }

            yield return new WaitForSeconds(scanDelay);
        }
    }
}
