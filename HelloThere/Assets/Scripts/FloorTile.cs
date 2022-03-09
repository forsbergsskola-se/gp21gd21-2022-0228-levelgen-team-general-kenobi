using System;
using UnityEngine;


public class FloorTile : MonoBehaviour
{
    public bool Spawnable;

    private void Awake()
    {
        if (Spawnable)
        {
            transform.parent.parent.GetComponent<EnemySpawner>().SpawnableTiles.Add(gameObject);

            // Debug.Log(transform.parent.parent.name);
        }
    }

    private void OnDrawGizmos()
    {
        if (Spawnable)
        {
            Gizmos.DrawSphere(gameObject.GetComponent<Renderer>().bounds.center, 1);
        }
    }
}
