using UnityEngine;
using UnityEngine.UI;

public class Makarov : GunAbstract
{
    private Bullet _makarovBullet;
    private void Start()
    {
        _makarovBullet = _bullet.GetComponent<Bullet>();
        _makarovBullet.OverwriteDamage(_damage);
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

    override protected void OnTriggerStay2D(Collider2D collision)
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
}

