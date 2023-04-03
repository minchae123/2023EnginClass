using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private float _speed, _time, _stopTime;
    private float _direction = 1;

    private float _currentTime = 0;

    private bool _isMove = true;

    void Update()
    {
        if (_isMove == false) return;

        _currentTime += Time.deltaTime;

        transform.Translate(new Vector3(_speed * Time.deltaTime * _direction, 0, 0));

        if(_currentTime >= _time)
        {
            _direction *= -1;
            _currentTime = 0;
            StartCoroutine(Stop(_stopTime));
        }
    }

    IEnumerator Stop(float time)
    {
        _isMove = false;
        yield return new WaitForSeconds(time);
        _isMove = true;
    }
}
