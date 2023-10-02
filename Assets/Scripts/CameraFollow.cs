using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _followObjact;
    [SerializeField ] private float _speed;

    private void OnEnable()
    {
        gameObject.transform.position = _followObjact.transform.position;
    }
    private void FixedUpdate()
    {
        Vector3 _position =  Vector3.Lerp(gameObject.transform.position, _followObjact.transform.position, Time.deltaTime * _speed);
        gameObject.transform.position = new Vector3( _position.x, gameObject.transform.position.y, -10);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _position.y, -10);
    }
}
