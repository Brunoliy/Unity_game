using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInimigo : MonoBehaviour // Script que faz os objetos spawnarem aleatoriamente pelo mapa
{
    [SerializeField] GameObject lixoPretoPrefab;
    [SerializeField] GameObject lixoVerdePrefab;
    [SerializeField] GameObject bolaPapelPrefab;
    [SerializeField] GameObject garrafaPlasticoPrefab;
    [SerializeField] TimerJogo timerJogo;


    [SerializeField] private float intervaloLixoPreto = 3.5f;
    [SerializeField] private float intervaloLixoVerde = 3.5f;
    [SerializeField] private float intervaloBolaPapel = 5f;
    [SerializeField] private float intervaloGarrafaPlastico = 10f;

    private void Start()
    {
        StartCoroutine(spawnEnemy(intervaloLixoPreto, lixoPretoPrefab));
        StartCoroutine(spawnEnemy(intervaloLixoVerde, lixoVerdePrefab));
        StartCoroutine(spawnEnemy(intervaloBolaPapel, bolaPapelPrefab));
        StartCoroutine(spawnEnemy(intervaloGarrafaPlastico, garrafaPlasticoPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        Debug.Log("Respawnando inimigos");
        yield return new WaitForSeconds(interval);

        if (timerJogo.minutos == 0 && timerJogo.segundos < 35)
        {
            Instantiate(enemy, new Vector3(Random.Range(112, 122), Random.Range(-55.77f, -61.55f), 0), Quaternion.identity);
        }
        else if (timerJogo.minutos == 0 && timerJogo.segundos >= 40)
        {
            Instantiate(enemy, new Vector3(Random.Range(124, 138), Random.Range(-35f, -45f), 0), Quaternion.identity);
        }
        else if (timerJogo.minutos == 1 && timerJogo.segundos >= 5)
        {
            Instantiate(enemy, new Vector3(Random.Range(130, 143), Random.Range(10, -5f), 0), Quaternion.identity);
        }


        if (gameObject.activeSelf)
        {
            Debug.Log("Ativando spawn");
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    }

}
