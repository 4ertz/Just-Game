using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private int _enemyAmount;
    [SerializeField] private float _distance;
    private GameObject _world;
    void Start()
    {
        _world = GameObject.Find("World");
        for (int i = 0; i < _enemyAmount; i++)
        {
          var enemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], Random.insideUnitCircle * _distance, Quaternion.identity, transform);
            enemy.transform.SetParent(_world.transform);
        }
    }
}
