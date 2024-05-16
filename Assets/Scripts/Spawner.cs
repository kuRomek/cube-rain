using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _secondsToSpawn;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: 5,
            maxSize: 10);
        Collider _collider = GetComponent<Collider>();

        StartCoroutine(StartSpawning());
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetRandomSpawnPosition();
        cube.gameObject.SetActive(true);
    }

    private IEnumerator StartSpawning()
    {
        WaitForSeconds secondsToSpawn = new WaitForSeconds(_secondsToSpawn);

        bool isRunning = true;

        while (isRunning)
        {
            yield return secondsToSpawn;

            Cube newCube = _pool.Get();
            StartCoroutine(WaitToRelease(newCube));
        }
    }

    private IEnumerator WaitToRelease(Cube cube)
    {
        yield return new WaitUntil(() => cube.gameObject.activeSelf == false);
        _pool.Release(cube);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomXValue = Random.Range(GetComponent<Collider>().bounds.min.x, GetComponent<Collider>().bounds.max.x);
        float randomZValue = Random.Range(GetComponent<Collider>().bounds.min.z, GetComponent<Collider>().bounds.max.z);

        return new Vector3(randomXValue, transform.position.y, randomZValue);
    }
}
