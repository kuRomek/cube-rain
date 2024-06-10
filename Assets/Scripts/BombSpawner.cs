using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void SpawnAt(Vector3 position)
    {
        Pool.Get().transform.position = position;
    }
}
