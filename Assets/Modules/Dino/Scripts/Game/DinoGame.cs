using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class DinoGame : MonoBehaviour
{
    [SerializeField] ScoreCounter m_ScoreCounter;
    [SerializeField] GameObject m_RestartScreen;
    
    // Component that controls and updates game visual appearance
    [SerializeField] TimeOfDay m_TimeOfDay;
    [SerializeField] float m_InitialSpeed, m_MaxSpeed, m_AccelerationPerFrame, m_ScoreFromSpeed;
    [SerializeField] RectTransform[] m_GroundBlocks;
    [SerializeField] Vector2 m_GroundRespawnPosition;
    [SerializeField] int m_SpawnNothingForFirstFrames = 30;
    [SerializeField] int m_MinFramesBetweenSpawns = 50;
    [SerializeField] int m_MaxFramesBetweenSpawns = 80;

    bool m_Running;
    float m_Speed;
    IReadOnlyList<ObjectSpawner> m_Spawners;
    RectTransform m_AttachTarget;
    int m_FramesBeforeSpawn;
    float m_GroundLength;
    float m_OneOverInitialSpeed;

    public bool Running {
        get => m_Running;
        private set {
            if (m_Running == value) return;
            
            m_RestartScreen.SetActive (value);
            m_Running = value;
        }
    }

    int GetFramesTillNextSpawn() => Mathf.CeilToInt (Random.Range (m_MinFramesBetweenSpawns, m_MaxFramesBetweenSpawns) 
                                                   * m_Speed 
                                                   * m_OneOverInitialSpeed);

	void Start ()
    {
        m_Spawners = FindObjectsOfType<ObjectSpawner> ();
        FindObjectOfType<DinoCharacter> ().OnHit += () => Running = false;
        m_OneOverInitialSpeed = 60 / m_InitialSpeed;
    }

    public void BeginRun ()
    {
        Running = true;
        m_TimeOfDay.Reset ();
        m_Speed = m_InitialSpeed;
        m_FramesBeforeSpawn = m_SpawnNothingForFirstFrames;
    }
    
	void FixedUpdate ()
    {
        var scoreGain = m_ScoreFromSpeed * m_Speed;
        m_TimeOfDay.ScoreGained (scoreGain);
        m_ScoreCounter.ScoreGained (scoreGain);

        var distance = m_Speed * Time.fixedDeltaTime * Vector2.left;
        foreach (var ground in m_GroundBlocks) {
            ground.Translate (distance);
            if (ground.transform.position.x < m_GroundRespawnPosition.x - ground.rect.width) {
                ground.SetPositionAndRotation (m_GroundRespawnPosition, Quaternion.identity);
                m_AttachTarget = ground;
            }
        }
        
        if (MaybeSpawnObstacle (out var obstacle))
            obstacle.SetParent (m_AttachTarget);
        
        m_Speed = Mathf.Min (m_MaxSpeed, m_AccelerationPerFrame + m_Speed);
    }

    bool MaybeSpawnObstacle (out Transform obstacle)
    {
        bool spawned = m_FramesBeforeSpawn-- <= 0;
        obstacle = spawned ? GetRandomObstacle ().transform : null;
        return spawned;
    }

    GameObject GetRandomObstacle ()
    {
        var result = m_Spawners[Random.Range (0, m_Spawners.Count - 1)].GetObject ();
        result.SetActive (true);
        m_FramesBeforeSpawn = GetFramesTillNextSpawn();
        return result;
    }
}
