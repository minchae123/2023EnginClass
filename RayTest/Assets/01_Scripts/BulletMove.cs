using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    private void Awake()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        transform.position += Vector3.forward * 5 * Time.deltaTime;
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
