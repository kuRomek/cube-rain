using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Material _material;
    private bool _hasTouchedGround;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        _material.color = Color.white;
        _hasTouchedGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedGround == false && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _hasTouchedGround = true;
            _material.color = new Color(Random.value, Random.value, Random.value);
            StartCoroutine(Release());
        }
    }

    private IEnumerator Release()
    {
        float minSecondsToRelease = 2f;
        float maxSecondsToRelease = 5f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(Random.Range(minSecondsToRelease, maxSecondsToRelease));

        yield return waitForSeconds;

        gameObject.SetActive(false);
    }
}
