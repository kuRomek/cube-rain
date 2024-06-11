using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<Type> : MonoBehaviour where Type : Component
{
    [SerializeField] private Type _object;
    
    public ObjectPool<Type> Pool { get; private set; }
    public int CountAll => Pool.CountAll;
    public int CountActive => Pool.CountActive;

    public event Action OnTotalCountChanged;
    public event Action OnActiveCountChanged;

    private void Awake()
    {
        Pool = new ObjectPool<Type>(
            createFunc: () => Instantiate(_object),
            actionOnGet: (spawningObject) => SpawnOneObject(spawningObject),
            actionOnRelease: (spawningObject) => ActionOnRelease(),
            actionOnDestroy: (spawningObject) => ActionOnDestroy(spawningObject),
            collectionCheck: true,
            defaultCapacity: 5,
            maxSize: 10); ;
    }

    protected virtual void SpawnOneObject(Type spawningObject)
    {
        spawningObject.gameObject.SetActive(true);

        if (spawningObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.freezeRotation = true;
            rigidbody.rotation = Quaternion.identity;
            rigidbody.freezeRotation = false;
        }

        StartCoroutine(WaitToRelease(spawningObject));

        OnTotalCountChanged?.Invoke();
        OnActiveCountChanged?.Invoke();
    }

    private IEnumerator WaitToRelease(Type spawningObject)
    {
        yield return new WaitUntil(() => spawningObject.gameObject.activeSelf == false);
        Pool.Release(spawningObject);
    }

    private void ActionOnRelease()
    {
        OnActiveCountChanged?.Invoke();
    }

    private void ActionOnDestroy(Type spawningObject)
    {
        OnTotalCountChanged?.Invoke();
        Destroy(spawningObject.gameObject);
    }
}
