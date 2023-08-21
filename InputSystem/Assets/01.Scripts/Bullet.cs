using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 5f;
    }
}
