using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject implementation of a basic object pooling system. This 
/// object pool can grow if the user needs it to.
/// </summary>
[CreateAssetMenu]
public class ObjectPooler : ScriptableObject
{
    /// <summary>
    /// Prefab object to be pooled on initialization.
    /// </summary>
    [Tooltip("Prefab object to be pooled on initialization.")]
    public GameObject ObjectToPool;

    /// <summary>
    /// Initial size of the object pool.
    /// </summary>
    [Tooltip("Initial size of the object pool.")]
    public int InitialPoolSize;

    /// <summary>
    /// If the pool fails to return an object, should it create one and 
    /// increase the pool's size accordingly?
    /// </summary>
    [Tooltip("If the pool fails to return an object, should it create " +
        "one and increase the pool's size accordingly?")]
    public bool CanGrow;

    /// <summary>
    /// Transform to store objects upon instantiation.
    /// </summary>
    private Transform objectPoolParent;

    /// <summary>
    /// Object pool list.
    /// </summary>
    private List<GameObject> objectPool;

    /// <summary>
    /// Has this pool been initialized?
    /// </summary>
    public bool Initialized { get; set; } = false;

    /// <summary>
    /// Returns the first inactive gameObject in the object pool. If the pool
    /// is allowed to grow, and this method fails to return an inactive
    /// gameObject, then a new object will be created, added to the pool, and 
    /// returned.
    /// </summary>
    /// <returns>{GameObject} First inactive object in pool, or newly added 
    /// gameObject if the pool is allowed to grow.</returns>
    public GameObject GetObject()
    {
        foreach (GameObject gameObject in objectPool)
        {
            if (!gameObject.activeInHierarchy)
            {
                return gameObject;
            }
        }

        if (CanGrow)
        {
            InstantiatePoolObject(out GameObject gameObject);
            return gameObject;
        }

        return null;
    }

    /// <summary>
    /// Creates an object pool of size initialPoolSize
    /// </summary>
    public void InitializePool()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < InitialPoolSize; i++)
        {
            InstantiatePoolObject();
        }

        Initialized = true;
    }

    /// <summary>
    /// Overload for InitializeObjectPool that parents each instatiated
    /// gameObject to a passed parent transform.
    /// </summary>
    /// <param name="objectPoolParent">Transform to parent objects to.</param>
    public void InitializePool(Transform objectPoolParent)
    {
        this.objectPoolParent = objectPoolParent;
        InitializePool();
    }

    /// <summary>
    /// Deactivates all objects in the object pool. Useful for resets.
    /// </summary>
    public void DeactivateAll()
    {
        if (objectPool != null)
        {
            foreach (GameObject gameObject in objectPool)
            {
                if (objectPoolParent != null)
                {
                    gameObject.transform.SetParent(objectPoolParent);
                }
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Resets the object pool by deactivating all pooled objects, then clearing
    /// the parent and pool objects.
    /// </summary>
    public void Reset()
    {
        DeactivateAll();
        if (objectPoolParent != null)
        {
            objectPoolParent = null;
        }
        if (objectPool != null)
        {
            objectPool = null;
        }
        Initialized = false;
    }

    /// <summary>
    /// Instatiates the game object and parents it to the objectPoolParent, if
    /// available.
    /// </summary>
    /// <param name="gameObject"></param>
    private void InstantiatePoolObject()
    {
        GameObject gameObject;

        if (objectPoolParent != null)
        {
            gameObject = Instantiate(ObjectToPool, objectPoolParent);
            gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            gameObject = Instantiate(ObjectToPool, Vector3.zero,
                Quaternion.identity);
        }

        gameObject.SetActive(false);
        objectPool.Add(gameObject);
    }

    /// <summary>
    /// Instatiates the game object and parents it to the objectPoolParent, if
    /// available. Outputs a GameObject reference.
    /// </summary>
    /// <param name="gameObject"></param>
    private void InstantiatePoolObject(out GameObject gameObject)
    {
        if (objectPoolParent != null)
        {
            gameObject = Instantiate(ObjectToPool, objectPoolParent);
            gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            gameObject = Instantiate(ObjectToPool, Vector3.zero,
                Quaternion.identity);
        }

        gameObject.SetActive(false);
        objectPool.Add(gameObject);
    }
}