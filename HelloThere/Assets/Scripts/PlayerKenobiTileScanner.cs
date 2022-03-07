using System.Collections;
using UnityEngine;

public class PlayerKenobiTileScanner : MonoBehaviour
{
    [HideInInspector] public GameObject CurrentKenobiTile;

    [SerializeField] private float kenobiScanningFrequency;

    private Transform playerKenobiTransform;
    private float kenobiScanDelay;



    void Start()
    {
        kenobiScanDelay = 1 / kenobiScanningFrequency;
        playerKenobiTransform = gameObject.transform;

        StartCoroutine(UpdateCurrentKenobiTileOnTimer());
    }



    private IEnumerator UpdateCurrentKenobiTileOnTimer()
    {
        while (true)
        {
            if (Physics.Raycast(playerKenobiTransform.position, Vector3.down, out RaycastHit kenobiHit))
            {
                var kenobiTile = kenobiHit.collider.gameObject.GetComponent<KenobiTile>();

                if (kenobiTile == null)
                {
                    throw new System.Exception("WHAT DID YOU COLLIDE WITH THAT ISN'T A TILE! SO UNCIVILIZED!");
                }

                CurrentKenobiTile = kenobiHit.collider.gameObject;
            }

            yield return new WaitForSeconds(kenobiScanDelay);
        }
    }
}
