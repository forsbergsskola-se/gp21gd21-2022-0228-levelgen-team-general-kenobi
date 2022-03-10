using System;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    public bool CanSpawnEnemy;
    public bool CanSpawnDecoration;

    private void Awake()
    {
        if (CanSpawnEnemy)
        {
            transform.parent.parent.GetComponent<EnemySpawner>().SpawnableTiles.Add(gameObject);
        }

        if (CanSpawnDecoration)
        {
            transform.parent.parent.GetComponent<DecorationSpawner>().SpawnableTiles.Add(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (CanSpawnEnemy)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(gameObject.GetComponent<Renderer>().bounds.center, 1.5f);
        }

        if (CanSpawnDecoration)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(gameObject.GetComponent<Renderer>().bounds.center, new Vector3(1, 2, 1));
        }
    }
}
