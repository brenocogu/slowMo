//Source: https://gist.github.com/brenocogu/d4c8ecc72b72c3779a312986e5827eed
//Useful for generic pooling, autoincrement, creating pools. Everything Useful to pool
//I've made that for gameObjects but change it as you wish.
//Fell free to use it, you are awesome
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class PoolHub
{
    Dictionary<object, List<GameObject>> hub;
    static PoolHub _instance;

    static PoolHub Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PoolHub();

            return _instance;
        }

        set { }
    }
    public PoolHub()
    {
        hub = new Dictionary<object, List<GameObject>>();
    }

    GameObject _GetFromPool(object obj)
    {
        if (obj is Behaviour)
            throw new System.ArgumentException("Parameter must inherit from MonoBehaviour or be a GameObject", "obj");

        GameObject converted = obj as GameObject;

        //If there's no pool, we create one
        if (!hub.ContainsKey(obj))
        {
            List<GameObject> listO = new List<GameObject>();
            //This is the List initialization. 10 = Pool size
            for (int i = 0; i <= 10; i++)
            {
                GameObject @object = Instantiate(converted);
                @object.SetActive(false);
                listO.Add(@object);
            }
            hub.Add(obj, listO);
        }
        //Linq to end beautifully
        GameObject returnO = (hub[obj].Where(x => !x.activeSelf).Any()) ? hub[obj].Where(x => !x.activeSelf).First() : null;

        //This is the Auto Increment.
        if (returnO == null)
        {
            returnO = Instantiate(converted);
            returnO.SetActive(false);
            hub[obj].Add(returnO);
        }

        return returnO;
    }
}

/// <summary>
/// Access the Pool Hub
/// </summary>
public partial class PoolHub : MonoBehaviour
{
    /// <summary>
    /// Get GameObject from the pool
    /// </summary>
    /// <param name="obj">Object to get</param>
    /// <returns></returns>
    public static GameObject GetFromPool(object obj)
    {
        return Instance._GetFromPool(obj);
    }
}