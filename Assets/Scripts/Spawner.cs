using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Wave[] waves;
    public Enemy enemy;

    Wave curruntWave;
    int curruntWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    void Start() {
        NextWave();
    }

    void Update() {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + curruntWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath() {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0) {
            NextWave();
        }
    }

    void NextWave() {
        curruntWaveNumber++;
        if (curruntWaveNumber - 1 <waves.Length) {
            curruntWave = waves[curruntWaveNumber - 1];
        }

        enemiesRemainingToSpawn = curruntWave.enemyCount;
        enemiesRemainingAlive = enemiesRemainingToSpawn;
    }

    [System.Serializable]
    public class Wave {
        public int enemyCount;
        public float timeBetweenSpawns;

    }

}
