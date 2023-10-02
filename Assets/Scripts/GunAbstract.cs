using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class GunAbstract : MonoBehaviour
{
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
    protected bool _isReload = false;
    protected int _bulletsInMagazine;
    protected bool _initialized;
    protected UiManager _uiManager;

    public bool IsReload => _isReload;


    virtual public void Shoot()
    {
        if (!_isReload && _bulletsInMagazine > 0)
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
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _player = GameObject.Find("Player");
        _bulletsInMagazine = _magazine—apacity;
        _uiManager.SetBulletsText(_bulletsInMagazine);
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
}
