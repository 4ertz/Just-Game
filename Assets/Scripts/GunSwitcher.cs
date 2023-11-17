using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
   [SerializeField] GameObject[] _guns;
   [SerializeField] private int _equpedGunIndex;

    public int _gunIndex; //{ get; private set; }

    private SaveSerial _saver;
    private bool _isAlive = true;

    void Start()
    {
        GameObject.Find("Player").GetComponent<Health>()._deadEvent += CharacterIsDead;
        _saver = GameObject.Find("Main Camera").GetComponent<SaveSerial>();
        _equpedGunIndex = _saver._equipedGundIndexToLoad;
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        _guns[_equpedGunIndex].GetComponent<SpriteRenderer>().enabled = true;
        _gunIndex = _guns[_equpedGunIndex].GetComponent<GunAbstract>()._gunIndex;
    }
    public void SwitchGun()
    {
        if (_isAlive)
        {
            if (_equpedGunIndex < _guns.Length - 1  && _guns[_equpedGunIndex].GetComponent<GunAbstract>().IsReload == false)
            {
                _guns[_equpedGunIndex].GetComponent<SpriteRenderer>().enabled = false;
                _equpedGunIndex++;
                _gunIndex = _guns[_equpedGunIndex].GetComponent<GunAbstract>()._gunIndex;
                _guns[_equpedGunIndex].GetComponent<SpriteRenderer>().enabled = true;
                _guns[_equpedGunIndex].GetComponent<GunAbstract>().SetBulletAmount();
            }
            else if(_guns[_equpedGunIndex].GetComponent<GunAbstract>().IsReload == false)
            {
                _guns[_equpedGunIndex].GetComponent<SpriteRenderer>().enabled = false;
                _equpedGunIndex = 0;
                _gunIndex = _guns[_equpedGunIndex].GetComponent<GunAbstract>()._gunIndex;
                _guns[_equpedGunIndex].GetComponent<SpriteRenderer>().enabled = true;
                _guns[_equpedGunIndex].GetComponent<GunAbstract>().SetBulletAmount();
            }
            _saver._equipedGunIndexToSave = _equpedGunIndex;
        }
    }
    private void CharacterIsDead()
    {
        _isAlive = false;
    }

}
