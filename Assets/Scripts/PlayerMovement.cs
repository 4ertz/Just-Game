using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float _speed;
  [SerializeField] private FixedJoystick _joystick;
  [SerializeField] private Animator _animator;

  public bool _canRotate = true;

  private bool _isAlive = true;
  private Rigidbody2D  _rigidbody ;
  private Vector2 _moveVelocity;

    private void Start()
    {
        GameObject.Find("Player").GetComponent<Health>()._deadEvent += CharacterIsDead;
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isAlive)
        {

            if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
            {
                _animator.SetBool("IsWalking", true);
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }

            if (_joystick.Horizontal > 0 && _canRotate)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (_joystick.Horizontal < 0 && _canRotate) 
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            Vector2 _moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            _moveVelocity = _moveInput.normalized * _speed;
        }
    }

    public void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveVelocity * Time.deltaTime);
    }
    private void CharacterIsDead()
    {
        _isAlive = false;
        _speed = 0;
    }
}
