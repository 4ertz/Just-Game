using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class EnemyPlot : MonoBehaviour
{
    [SerializeField] int _range;
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _loot;
    private Collider2D _collider;
    private bool _attackCooldown = false;
    private ParticleSystem _deadEffect;
    private GameObject _player;
    

    private void Start()
    {
        _slider.maxValue = _health;
        _slider.value = _health;
        _collider = GetComponent<Collider2D>();
        _deadEffect = GameObject.Find("DeadEffect").GetComponent<ParticleSystem>();
        _player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _range)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _speed);
            _animator.SetBool("IsRuning", true);
            if (_player.transform.position.x < transform.position.x)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            }
            else
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
        }
        else { _animator.SetBool("IsRuning", false); }
    }
    public void Damage(int damage)
    {
        _health -= damage;
        _slider.DOValue(_health, 0.3f);
        if (_health <= 0)
        {
            _deadEffect.transform.position = gameObject.transform.position;
            _deadEffect.Play();
            Instantiate(_loot[Random.Range(0, _loot.Length)], gameObject.transform.position, gameObject.transform.rotation);
            GameObject.Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_attackCooldown == false && collision.tag == "Player")
        {
            collision.TryGetComponent<Health>(out Health health);
            {
                health.Damage(20);
                _animator.SetTrigger("Attack");
                _attackCooldown = true;
                StartCoroutine(AttackCd());
            }
            
        }
    }
   private IEnumerator AttackCd()
    {
        yield return new WaitForSeconds(2);
        _attackCooldown = false;
    }
}
