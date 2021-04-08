using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StansAssets.ProjectSample.Dino.Game
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject m_ExampleObject;
        [SerializeField] int m_MinPositionX = -1000;
        [SerializeField] float m_RequiredSpeed;
        [SerializeField] float m_MinFramesGap = 50;

        readonly List<GameObject> m_ActiveObjects = new List<GameObject> ();
        readonly Stack<GameObject> m_StashedObjects = new Stack<GameObject> ();

        protected Vector3 GetSpawnPosition () => transform.position;
        // min speed required to spawn this type of obstacles
        internal float RequiredSpeed => m_RequiredSpeed;
        // min space after the obstacle (in score points)
        internal float RequiredSpace => m_MinFramesGap;

        void Start ()
        {
            m_ExampleObject.SetActive (false);
        }

        void FixedUpdate ()
        {
            var toRemove = m_ActiveObjects.Where (OutOfValidRange).ToList ();
            foreach (var obj in toRemove) {
                StashSpawnedObject (obj);
            }
        }

        bool OutOfValidRange (GameObject obj)
        {
            float positionX = obj.transform.position.x;
            return positionX < m_MinPositionX || positionX > 2500;
        }

        public void Reset ()
        {
            foreach (var obj in m_ActiveObjects)
                Destroy (obj);
            m_ActiveObjects.Clear ();

            foreach (var obj in m_StashedObjects) { Destroy (obj); }

            m_StashedObjects.Clear ();
        }

        public GameObject GetObject ()
        {
            if (m_StashedObjects.Any ()) {
                var result = m_StashedObjects.Pop ().gameObject;
                result.SetActive (true);
                m_ActiveObjects.Add (result);
                result.transform.SetPositionAndRotation (GetSpawnPosition (), Quaternion.identity);
                return result;
            }
            else {
                var newCopy = Instantiate (m_ExampleObject, GetSpawnPosition (), Quaternion.identity);
                newCopy.SetActive (true);
                m_ActiveObjects.Add (newCopy);
                return newCopy;
            }
        }

        void StashSpawnedObject (GameObject obj)
        {
            if (m_ActiveObjects.Remove (obj)) {
                m_StashedObjects.Push (obj);
                obj.transform.SetParent (transform);
                obj.SetActive (false);
                obj.transform.localPosition = Vector3.zero;
            }
        }
    }
}
