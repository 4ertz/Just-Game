using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ak47 : GunAbstract
{
    [SerializeField] private float _shootCd;
    private bool _canShoot = true;
    private Bullet _akBullet;
    private bool _isButtonHold;

    private void Start()
    {
        _akBullet = _bullet.GetComponent<Bullet>();
        _akBullet.OverwriteDamage(_damage);
        _reloadButton = GameObject.Find("ReloadBullet").GetComponent<Button>();
        Initialized();
        _initialized = true;
    }

    private void OnEnable() 
    {
        _reloadButton?.onClick.AddListener(() => Reload()); 
        if(_initialized) 
             _uiManager.SetBulletsText(_bulletsInMagazine);
    }
    private void OnDisable() 
    {
        _reloadButton?.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    private void Update()
    {
        if(_isButtonHold) { Shoot(); }
    }
    public override void Shoot()
    {
        if (_canShoot)
        {
            _canShoot = false;
            base.Shoot();
            StartCoroutine(ShootCd());
        }
    }

    public void Hold() { _isButtonHold = true; }
    public void UnHold() { _isButtonHold = false; }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (_mainTarget == null)
            {
                _mainTarget = collision.transform;
            }
            else if (Vector2.Distance(collision.transform.position, gameObject.transform.position) < Vector2.Distance(_mainTarget.position, gameObject.transform.position))
            {
                _mainTarget = collision.transform;
            }
            if (_mainTarget.position.x < _player.transform.position.x)
            {
                _player.transform.localRotation = Quaternion.Euler(0, 180, 0);
                _player.GetComponent<PlayerMovement>()._canRotate = false;
            }
            if (_mainTarget.position.x > _player.transform.position.x)
            {
                _player.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _player.GetComponent<PlayerMovement>()._canRotate = false;
            }
            _handTarget.position = Vector2.Lerp(_handTarget.position, _mainTarget.position, Time.deltaTime * _targetingTime);
        }
    }

    override protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _handTarget.position = _defaultHandTarget.position;
            _player.GetComponent<PlayerMovement>()._canRotate = true;
        }
    }

    private IEnumerator ShootCd()
    {
        yield return new WaitForSeconds(_shootCd);
        _canShoot = true;
    }
}
