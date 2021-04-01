using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_ExampleObject;
    [SerializeField] int m_MinPositionX = -1000;
    
    bool m_IsSpawning;
    List<GameObject> m_ActiveObjects = new List<GameObject> ();
    readonly Stack<GameObject> m_StashedObjects = new Stack<GameObject> ();

    protected Vector3 GetSpawnPosition () => transform.position;

    void Update ()
    {
        List<GameObject> remainingActiveObjects = new List<GameObject> ();
        foreach (var obj in m_ActiveObjects) {
            if(obj.transform.position.x <= m_MinPositionX)
                StashSpawnedObject (obj);
            else {
                remainingActiveObjects.Add (obj);
            }
        }

        m_ActiveObjects = remainingActiveObjects;
    }

    public void Reset ()
    {
        foreach (var obj in m_ActiveObjects) 
            Destroy(obj);
        m_ActiveObjects.Clear ();

        foreach (var obj in m_StashedObjects) {
            Destroy (obj);
        }
        m_StashedObjects.Clear ();
    }

    public GameObject GetObject ()
    {
        if (m_StashedObjects.Any ()) {
            var result = m_StashedObjects.Pop ().gameObject;
            result.SetActive (true);
            result.transform.SetPositionAndRotation (GetSpawnPosition (), Quaternion.identity);
            return result;
        }
        else {
            var newCopy = Instantiate (m_ExampleObject, GetSpawnPosition (), Quaternion.identity);
            m_ActiveObjects.Add (newCopy);
            return newCopy;
        }
    }

    internal void StashSpawnedObject (GameObject obj)
    {
        if (m_ActiveObjects.Remove (obj)) {
            m_StashedObjects.Push (obj);
            obj.gameObject.SetActive (false);
            obj.transform.SetParent (transform.parent);
        }
    }
}
