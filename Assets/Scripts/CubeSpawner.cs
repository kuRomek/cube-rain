using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _secondsToSpawn;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        StartCoroutine(StartSpawning());
    }

    protected override void SpawnOneObject(Cube cube)
    {
        cube.transform.position = GetRandomSpawnPosition();
        base.SpawnOneObject(cube);
    }

    private IEnumerator StartSpawning()
    {
        WaitForSeconds secondsToSpawn = new WaitForSeconds(_secondsToSpawn);

        bool isRunning = true;

        while (isRunning)
        {
            yield return secondsToSpawn;
            Pool.Get();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomXValue = Random.Range(_collider.bounds.min.x, _collider.bounds.max.x);
        float randomZValue = Random.Range(_collider.bounds.min.z, _collider.bounds.max.z);

        return new Vector3(randomXValue, transform.position.y, randomZValue);
    }
}
