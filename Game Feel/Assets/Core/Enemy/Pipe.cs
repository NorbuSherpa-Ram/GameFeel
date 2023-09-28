using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnTimer = 3f;
    [SerializeField] private List<Color> color;

    [SerializeField] SpriteRenderer mySR;
    private void Start() {
        StartCoroutine(SpawnRoutine());
    }
    
    private IEnumerator SpawnRoutine() {
        while (true)
        {
          Color newColor   = color[Random.Range(0, color.Count)];
            mySR.color = newColor;
            Enemy enemy = Instantiate(_enemyPrefab, transform.position, transform.rotation);
           enemy.SetColot(newColor);
            yield return new WaitForSeconds(_spawnTimer);
        }
    }
}
