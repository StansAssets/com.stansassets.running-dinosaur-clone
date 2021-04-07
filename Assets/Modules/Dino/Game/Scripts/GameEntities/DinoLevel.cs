using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class DinoLevel : MonoBehaviour
    {
        public event Action<float> OnScoreGained;
        public event Action OnReset;

        // Component that controls and updates game visual appearance
        [SerializeField] float m_InitialSpeed, m_MaxSpeed, m_AccelerationPerFrame, m_ScoreFromSpeed;
        [SerializeField] RectTransform[] m_GroundBlocks;
        [SerializeField] float m_GroundRespawnPositionX;
        // The game is expected to run at 60 FPS
        [SerializeField] int m_SpawnNothingForFirstFrames = 30;

        bool m_Running;
        float m_Speed;
        RectTransform m_AttachTarget;
        int m_FramesBeforeSpawn;
        IReadOnlyList<ObjectSpawner> m_Spawners;
        float m_FullGroundWidth;
        
        // In the original game, the gap depends on width of the obstacle, the speed, and a width of the field of view 
        int GetFramesGap (float minGapWidth)
        {
            // Number of frames required to cover the min gap, while moving with a current speed
            var baseFramesGap = minGapWidth / (m_Speed * m_ScoreFromSpeed);
            // Randomizing gap width, while also applying some additional scaling depending on current speed.
            // As a result, the frames gap between obstacles is more or less the same value. 
            return Mathf.CeilToInt (GapCoefficient * baseFramesGap * UnityEngine.Random.Range (1f, 1.3f));
        }

        // todo
        float GapCoefficient => 1;
        
        IEnumerable<ObjectSpawner> AvailableSpawners => m_Spawners.Where (spawner => spawner.RequiredSpeed <= m_Speed);
        
        void Start ()
        {
            m_Spawners = FindObjectsOfType<ObjectSpawner> ();
            m_AttachTarget = m_GroundBlocks[0];
            m_FullGroundWidth = m_GroundBlocks.Sum (block => block.rect.width);
        }

        // Returns level to default values
        public void Reset ()
        {
            m_Speed = m_InitialSpeed;
            m_FramesBeforeSpawn = m_SpawnNothingForFirstFrames;
            OnReset?.Invoke ();
        }

        // Allows to pause the level
        public void SetLevelActive (bool active) => m_Running = active;

        void FixedUpdate ()
        {
            if (!m_Running) return;
            
            var scoreGain = m_ScoreFromSpeed * m_Speed;
            OnScoreGained?.Invoke (scoreGain);

            var distance = m_Speed * Time.fixedDeltaTime * Vector2.left;
            for (int i = 0; i < m_GroundBlocks.Length; i++) {
                m_GroundBlocks[i].Translate (distance);
                if (m_GroundBlocks[i].transform.position.x < m_GroundRespawnPositionX) {
                    m_GroundBlocks[i].Translate (new Vector3 (m_FullGroundWidth, 0));
                    int nextBlockIndex = (i + 1) % m_GroundBlocks.Length;
                    m_AttachTarget = m_GroundBlocks[nextBlockIndex];
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
            var spawners = AvailableSpawners.ToArray ();
            var selectedSpawner = spawners[UnityEngine.Random.Range (0, spawners.Length - 1)];
            var result = selectedSpawner.GetObject ();
            result.SetActive (true);
            m_FramesBeforeSpawn = GetFramesGap (selectedSpawner.RequiredSpace);
            return result;
        }
    }
}
