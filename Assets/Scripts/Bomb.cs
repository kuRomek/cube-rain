using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _explosionForce = 400f;
    [SerializeField] private ParticleSystem _effect;

    private Material _material;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;
    private float _lifeTime;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 1f);
        StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        while (_material.color.a > 0f)
        {
            yield return null;
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 
                Mathf.MoveTowards(_material.color.a, 0f, 1f / _lifeTime * Time.deltaTime));
        }

        Explode();
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        Instantiate(_effect, transform.position, Quaternion.identity);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius);
        }

        gameObject.SetActive(false);
    }
}
