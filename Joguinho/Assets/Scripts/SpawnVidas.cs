using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVidas : MonoBehaviour
{
    [SerializeField] GameObject Vidas;

    [SerializeField] private float intervaloVidas = 20f;

    private void Start()
    {

        StartCoroutine(spawnEnemy(intervaloVidas, Vidas));
       
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-7f, 7), Random.Range(-5f, 5), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
