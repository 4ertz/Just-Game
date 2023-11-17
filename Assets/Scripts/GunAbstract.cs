using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class GunAbstract : MonoBehaviour
{
    public bool IsReload => _isReload;

    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected GameObject _bulletSpawner;
    [SerializeField] protected Transform _handTarget; //“Ó˜Í‡ Í ÍÓÚÓÓÈ ÚˇÌÂÚÒˇ ÛÍ‡
    [SerializeField] protected Transform _defaultHandTarget; //“Ó˜Í‡ Í ÍÓÚÓÓ˚È ÚˇÌÚÒˇ ÛÍ‡, ÍÓ„‰‡ ÌÂÚ ÔÓÚË‚ÌËÍ‡
    [SerializeField] protected int _damage;
    [SerializeField] protected int _reloadTime;
    [SerializeField] protected int _magazine—apacity;
    [SerializeField] protected float _targetingTime; //—ÍÓÓÒÚ¸ Ì‡‚Â‰ÂÌËˇ

    protected Transform _mainTarget;
    protected GameObject _player;
    protected Button _reloadButton;
    protected Button _shootButton;
    protected bool _isReload = false;
    protected int _bulletsInMagazine;
    protected bool _initialized;
    protected UiManager _uiManager;
    protected bool _isAlive = true;

    virtual public void Shoot()
    {
        if (!_isReload && _bulletsInMagazine > 0 && _isAlive)
        {
            _bulletsInMagazine--;
            Instantiate(_bullet, _bulletSpawner.transform.position, _bulletSpawner.transform.rotation);
            if (_bulletsInMagazine == 0) 
            {
              StartCoroutine(ReloadCorutine());
              _isReload = true; 
            }
            _uiManager.SetBulletsText(_bulletsInMagazine);
        }
    }

    protected void Initialized()
    {
         GameObject.Find("Player").GetComponent<Health>()._deadEvent += CharacterIsDead;
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _player = GameObject.Find("Player");
        _bulletsInMagazine = _magazine—apacity;
        _uiManager.SetBulletsText(_bulletsInMagazine);
    }
    protected virtual void TargetTheEnemy(Collider2D collision)
    {
        if (collision.tag == "Enemy" && _isAlive)
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
    abstract protected void OnTriggerStay2D(Collider2D collision);

    abstract protected void OnTriggerExit2D(Collider2D collision);
    public void Reload() { StartCoroutine(ReloadCorutine()); }
    protected IEnumerator ReloadCorutine()
    {
        if (_bulletsInMagazine < _magazine—apacity && !_isReload)
        {
            _isReload = true;
            yield return new WaitForSeconds(_reloadTime);
            _bulletsInMagazine = _magazine—apacity;
            _isReload = false;
            _uiManager.SetBulletsText(_bulletsInMagazine);
            _isReload = false;
        }
    }
    protected void CharacterIsDead()
    {
        _isAlive = false;
    }
}
