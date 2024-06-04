using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerJogo : MonoBehaviour
{
    [SerializeField] SpawnerInimigo spawn;
    [SerializeField] GameObject bossLixeiraPrefab;
    [SerializeField] GameObject bossBateriaPrefab;
    [SerializeField] GameObject bossToxicoPrefab;
    //private bool vitoriaAtivada = false;
    private GameObject bossToxicoInstance;

    private int contadorBossLixeira = 1;  // Quantidade de vezes que o boss Lixeira pode ser spawnado
    private int contadorBossBateria = 1;  // Quantidade de vezes que o boss Bateria pode ser spawnado
    private int contadorBossToxico = 1;   // Quantidade de vezes que o boss T�xico pode ser spawnado

    [SerializeField] GameObject painelVitoria;
    [SerializeField] GameObject painelHUD;
    [SerializeField] GameObject painelDerrota;

    public TextMeshProUGUI tempoMinutos;
    public TextMeshProUGUI tempoSegundos;
    public float timeElapsed;
    public int segundos;
    public int minutos;
    public Vector3 spawnPositionLixeira;
    public Vector3 spawnPositionBateria;
    public Vector3 spawnPositionToxico;
    public bool bossSpawned;
    public void Start()
    {
        spawnPositionLixeira = new Vector3(125, -59f, 0);
        spawnPositionBateria = new Vector3(131, -40f, 0);
        spawnPositionToxico = new Vector3(136, 4.7f, 0);
        bossSpawned = false;
    }
    public void Update()
    {

        Derrota();
        SpawnBoss();
        VerificarBossDerrotado();
        if (bossToxicoInstance == null)
        {
            Vitoria();
        }
    }
    public void FixedUpdate()
    {
        tempoMinutos.text = minutos.ToString("00");
        tempoSegundos.text = segundos.ToString("00");
        timeElapsed += Time.deltaTime;
        minutos = (int)(timeElapsed / 60f);
        segundos = (int)(timeElapsed - minutos * 60f);
    }
    public void Derrota()
    {
        if (minutos >= 3)
        {
            Time.timeScale = 0;
            painelHUD.SetActive(false);
            painelDerrota.SetActive(true);
        }
    }
    public void Vitoria()
    {
        if (bossToxicoInstance == null && timeElapsed > 90)
        {
            painelHUD.SetActive(false);
            painelVitoria.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SpawnBoss()
    {
        if (!bossSpawned)
        {
            if (segundos >= 35 && contadorBossLixeira > 0)
            {
                Instantiate(bossLixeiraPrefab, spawnPositionLixeira, Quaternion.identity);
                contadorBossLixeira--;
                bossSpawned = true;

                if (bossSpawned == true)
                {
                    bossSpawned = false;
                }
            }
            if (minutos >= 1 && contadorBossBateria > 0)
            {
                Instantiate(bossBateriaPrefab, spawnPositionBateria, Quaternion.identity);
                contadorBossBateria--;
                bossSpawned = true;

                if (bossSpawned == true)
                {
                    bossSpawned = false;
                }
            }
            if (minutos >= 1 && segundos >= 20)
            {
                spawn.enabled = false;

                if (minutos >= 1 && segundos >= 30 && contadorBossToxico > 0)
                {
                    bossToxicoInstance = Instantiate(bossToxicoPrefab, spawnPositionToxico, Quaternion.identity);
                    contadorBossToxico--;
                    bossSpawned = true;

                }
            }

        }
    }

    public float PegarVidaBossToxico()
    {
        if (bossToxicoPrefab == null)
        {
            Inimigo bossToxicoScript = bossToxicoInstance.GetComponent<Inimigo>();

            if (bossToxicoScript != null)
            {
                if (bossToxicoScript.vidas <= 0)
                {
                    Vitoria();
                }
                return bossToxicoScript.vidas;
            }
        }
        return 0f;
    }

    public void VerificarBossDerrotado()
    {
        float vidaBoss = PegarVidaBossToxico();

        if (vidaBoss <= 0)
        {
            Vitoria();
        }
    }
}
