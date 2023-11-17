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
        _shootButton = GameObject.Find("ShootButton").GetComponent<Button>();
        _reloadButton = GameObject.Find("ReloadBullet").GetComponent<Button>();
        Initialized();
        _initialized = true;
    }

    private void OnEnable() 
    {
        _shootButton?.onClick.RemoveAllListeners();

        _reloadButton?.onClick.AddListener(() => Reload());
        _shootButton?.onClick.AddListener(() => Shoot());

        if (_initialized) 
             _uiManager.SetBulletsText(_bulletsInMagazine);
    }
    private void OnDisable() 
    {
        _shootButton?.onClick.RemoveAllListeners();
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
        TargetTheEnemy(collision);
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
