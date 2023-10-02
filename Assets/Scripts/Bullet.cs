using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(transform.right * _force * -1);
        StartCoroutine(Delete());
    }

    public void OverwriteDamage(int damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.TryGetComponent<EnemyPlot>(out EnemyPlot enemy);
            {
                enemy.Damage(_damage);
                GameObject.Destroy(gameObject);
            }
        }
    }
    private  IEnumerator Delete()
    {
        yield return new WaitForSeconds(5);
        GameObject.Destroy(gameObject);
    }

}
