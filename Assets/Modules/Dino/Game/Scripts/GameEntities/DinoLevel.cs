using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StansAssets.Dino.Game
{
    public class DinoLevel : MonoBehaviour
    {
        public event Action<float> OnScoreGained;
        public event Action OnReset;

        [SerializeField] float m_InitialSpeed,
            m_MaxSpeed = default,
            m_AccelerationPerFrame = default,
            m_ScoreFromSpeed = default;

        [SerializeField] RectTransform[] m_GroundBlocks = default;
        [SerializeField] RectTransform m_GroundRespawnPoint = default;
        [SerializeField] int m_SpawnNothingForFirstFrames = 30;
        [SerializeField] private Tutorial m_Tutorial = default;

        int GroundRespawnPositionX => (int) m_GroundRespawnPoint.anchoredPosition.x;

        bool m_Running, m_Initialized;
        float m_Speed, m_Score;
        int m_FramesBeforeSpawn;
        ObjectSpawner[] m_Spawners;

        IReadOnlyList<ObjectSpawner> Spawners => m_Spawners ??= FindObjectsOfType<ObjectSpawner>();
        public int Score => Mathf.RoundToInt(m_Score);

        int GetFramesGap(float minGapWidth)
        {
            // In the original game, the gap depends on width of the obstacle, the speed, and a width of the field of view
            // This method returns randomized gap width, and applies some scaling depending on current speed.
            return Mathf.CeilToInt(GapCoefficient * minGapWidth * UnityEngine.Random.Range(1f, 1.5f));
        }

        // Gap width coefficient. Reduces density of obstacles.
        float GapCoefficient => Mathf.Sqrt(m_MaxSpeed / m_Speed);

        void Init()
        {
            OnScoreGained += (value) => m_Score += value;

            var timeOfDay = GetComponentInChildren<TimeOfDay>();
            OnReset += timeOfDay.Reset;
            OnScoreGained += timeOfDay.ScoreGained;
            m_Initialized = true;
        }

        // Set default values
        public void Reset()
        {
            if (!m_Initialized)
                Init();

            m_Score = 0;
            m_Speed = m_InitialSpeed;
            m_FramesBeforeSpawn = m_SpawnNothingForFirstFrames;
            foreach (var spawner in Spawners)
            {
                spawner.Reset();
            }
#if UNITY_EDITOR || UNITY_STANDALONE
            m_Tutorial.Hide();
#else
            m_Tutorial.Show();
#endif
            OnReset?.Invoke();
        }

        // Allows to pause the level
        public void SetLevelActive(bool active) => m_Running = active;

        void FixedUpdate()
        {
            if (!m_Running) return;

            OnScoreGained?.Invoke(m_ScoreFromSpeed * m_Speed);

            var distance = m_Speed * Time.fixedDeltaTime * Vector3.left;
            for (int i = 0; i < m_GroundBlocks.Length; i++)
            {
                m_GroundBlocks[i].Translate(distance);
                if (m_GroundBlocks[i].anchoredPosition.x < GroundRespawnPositionX)
                    RepositionGroundBlock(i);
            }

            if (m_FramesBeforeSpawn-- <= 0)
                GetRandomObstacle().transform.SetParent(LastBlockTransform);

            m_Speed = Mathf.Min(m_MaxSpeed, m_AccelerationPerFrame + m_Speed);
        }

        void RepositionGroundBlock(int index)
        {
            var blockToReposition = m_GroundBlocks[index];
            int nextBlockIndex = index + 1 >= m_GroundBlocks.Length ? 0 : index + 1;
            var nextBlock = m_GroundBlocks[nextBlockIndex];
            blockToReposition.anchoredPosition =
                new Vector2(nextBlock.anchoredPosition.x + blockToReposition.sizeDelta.x,
                    blockToReposition.anchoredPosition.y);
        }

        GameObject GetRandomObstacle()
        {
            var spawners = Spawners.Where(spawner => spawner.RequiredSpeed <= m_Speed).ToArray();
            var selectedSpawner = spawners[UnityEngine.Random.Range(0, spawners.Length)];
            m_FramesBeforeSpawn = GetFramesGap(selectedSpawner.RequiredSpace);
            return selectedSpawner.GetObject();
        }

        RectTransform LastBlockTransform
        {
            get
            {
                return m_GroundBlocks.Aggregate(
                    (lastGround, ground) =>
                        ground.anchoredPosition.x > lastGround.anchoredPosition.x ? ground : lastGround);
            }
        }

    }
}
