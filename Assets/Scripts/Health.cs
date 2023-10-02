using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public UnityAction<int> _healthChanged;

    [SerializeField] private int _health;
    [SerializeField] private Slider _healthBar;
    private UiManager _uiManager;
    private ParticleSystem _deadEffect;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _deadEffect = GameObject.Find("DeadEffect").GetComponent<ParticleSystem>();
    }
    public void Damage(int damage)
    {
        _health -= damage;
        _healthBar.DOValue(_health, 1);
        if (_health <= 0)
        {
            _health = 0;
            Dead();
        }
    }
    private void Dead()
    {
        _uiManager.StartCoroutine(_uiManager.DeadScreen());
        _deadEffect.transform.position = transform.position;
        _deadEffect.Play();
        gameObject.transform.DOScale(0, 0.3f);
    }
}
