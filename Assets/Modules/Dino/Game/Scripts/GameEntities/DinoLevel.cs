using System;
using System.Collections.Generic;
using System.Linq;
using StansAssets.ProjectSample.InApps;
using UnityEngine;

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
        [SerializeField] Tutorial m_Tutorial;

        bool m_Running;
        float m_Speed, m_Score;
        int m_FramesBeforeSpawn;
        ObjectSpawner[] m_Spawners;
        float m_FullGroundWidth;
        // New spawned obstacles will be attached to this ground block as children.
        RectTransform m_AttachTarget;

        IReadOnlyList<ObjectSpawner> Spawners => m_Spawners ?? (m_Spawners = FindObjectsOfType<ObjectSpawner> ());
        public int Score => Mathf.RoundToInt(m_Score);
        
        int GetFramesGap (float minGapWidth)
        {
            // In the original game, the gap depends on width of the obstacle, the speed, and a width of the field of view 
            // This method returns randomized gap width, and applies some scaling depending on current speed.
            return Mathf.CeilToInt (GapCoefficient * minGapWidth * UnityEngine.Random.Range (1f, 1.5f));
        }

        // Gap width coefficient. Reduces density of obstacles.
        float GapCoefficient => Mathf.Sqrt(m_MaxSpeed / m_Speed);
        
        void Start ()
        {
            m_AttachTarget = m_GroundBlocks[1];
            m_FullGroundWidth = m_GroundBlocks.Sum (block => block.rect.width);
            OnScoreGained += (value) => m_Score += value;
        }

        // Set default values
        public void Reset ()
        {
            m_Score = 0;
            m_Speed = m_InitialSpeed;
            m_FramesBeforeSpawn = m_SpawnNothingForFirstFrames;
            foreach (var spawner in Spawners) {
                spawner.Reset ();
            }
#if UNITY_EDITOR || UNITY_STANDALONE
            m_Tutorial.Hide();
#else 
            m_Tutorial.Show();
#endif
            OnReset?.Invoke ();
        }

        // Allows to pause the level
        public void SetLevelActive (bool active) => m_Running = active;

        void FixedUpdate ()
        {
            if (!m_Running) return;
            
            OnScoreGained?.Invoke (m_ScoreFromSpeed * m_Speed);

            var distance = m_Speed * Time.fixedDeltaTime * Vector3.left;
            for (int i = 0; i < m_GroundBlocks.Length; i++) {
                m_GroundBlocks[i].Translate (distance);
                if (m_GroundBlocks[i].transform.position.x < m_GroundRespawnPositionX) 
                    RepositionGroundBlock (i);
            }

            if (m_FramesBeforeSpawn-- <= 0) 
                GetRandomObstacle ().transform.SetParent (m_AttachTarget);

            m_Speed = Mathf.Min (m_MaxSpeed, m_AccelerationPerFrame + m_Speed);
        }

        void RepositionGroundBlock (int index)
        {
            m_GroundBlocks[index].Translate (new Vector3 (m_FullGroundWidth, 0));
            int nextBlockIndex = (index + 2) % m_GroundBlocks.Length;
            m_AttachTarget = m_GroundBlocks[nextBlockIndex];
        }

        GameObject GetRandomObstacle ()
        {
            var spawners = Spawners.Where (spawner => spawner.RequiredSpeed <= m_Speed).ToArray ();
            var selectedSpawner = spawners[UnityEngine.Random.Range (0, spawners.Length)];
            m_FramesBeforeSpawn = GetFramesGap (selectedSpawner.RequiredSpace);
            return selectedSpawner.GetObject ();
        }
    }
}
