using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
   [SerializeField] GameObject[] _guns;
   [SerializeField] GameObject[] _buttons;
   [SerializeField] private int _equpedGunIndex;
    private SaveSerial _saver;
    private bool _isAlive = true;

    void Start()
    {
        GameObject.Find("Player").GetComponent<Health>()._deadEvent += CharacterIsDead;
        _saver = GameObject.Find("Main Camera").GetComponent<SaveSerial>();
        _equpedGunIndex = _saver._equipedGundIndexToLoad;
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].SetActive(false);
            _buttons[i].SetActive(false);
        }
        _guns[_equpedGunIndex].SetActive(true);
        //_buttons[_equpedGunIndex].SetActive(true);
    }
    public void SwitchGun()
    {
        if (_isAlive)
        {
            if (_equpedGunIndex < _guns.Length - 1  && _guns[_equpedGunIndex].GetComponent<GunAbstract>().IsReload == false)
            {
                _guns[_equpedGunIndex].SetActive(false);
                //_buttons[_equpedGunIndex].SetActive(false);
                _equpedGunIndex++;
                _guns[_equpedGunIndex].SetActive(true);
                //_buttons[_equpedGunIndex].SetActive(true);
            }
            else if(_guns[_equpedGunIndex].GetComponent<GunAbstract>().IsReload == false)
            {
                _guns[_equpedGunIndex].SetActive(false);
                //_buttons[_equpedGunIndex].SetActive(false);
                _equpedGunIndex = 0;
                _guns[_equpedGunIndex].SetActive(true);
                //_buttons[_equpedGunIndex].SetActive(true);

            }
             _saver._equipedGunIndexToSave = _equpedGunIndex;
        }
    }
    private void CharacterIsDead()
    {
        _isAlive = false;
    }

}
