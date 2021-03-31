using System;
using System.Collections.Generic;
using StansAssets.Foundation.Patterns;
using StansAssets.ProjectSample.Core;

namespace StansAssets.ProjectSample.Boxes
{
    class EnemyCharactersSpawner : PoolableGameObjectsSpawner, IBoxesGameEntity
    {
        readonly List<IEnemyCharacter> m_SpawnedEnemies = new List<IEnemyCharacter>();
        bool m_IsPaused;

        public void Init(IReadOnlyServiceLocator services, Action onComplete)
        {
            StartCoroutine(SpawnLoop());
            onComplete.Invoke();
        }

        public void Pause(bool isPaused)
        {
            m_IsPaused = isPaused;
            foreach (var enemyCharacter in m_SpawnedEnemies)
            {
                enemyCharacter.SetFreeze(isPaused);
            }
        }

        public void Restart()
        {
            var currentlySpawned = ListPool<IEnemyCharacter>.Get();
            currentlySpawned.AddRange(m_SpawnedEnemies);

            foreach (var entity in currentlySpawned)
            {
                entity.Die();
            }

            ListPool<IEnemyCharacter>.Release(currentlySpawned);
        }

        public void Destroy() { }

        protected override PoolableGameObject Spawn()
        {
            if (m_IsPaused)
                return null;

            var spawn = base.Spawn();
            if (spawn is IEnemyCharacter enemyCharacter)
            {
                m_SpawnedEnemies.Add(enemyCharacter);
                enemyCharacter.OnDeath += () =>
                {
                    m_SpawnedEnemies.Remove(enemyCharacter);
                };
            }

            return spawn;
        }
    }
}
