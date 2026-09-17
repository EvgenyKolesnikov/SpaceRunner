using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public static PoolManager Instance { get; set; }
    Dictionary<EntityId, Queue<GameObject>> pool = new Dictionary<EntityId, Queue<GameObject>>();
    Camera cam;

    public void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void CreatePool(GameObject gameObj, int numberOfObj)
    {
        EntityId gameObjKey = gameObj.GetEntityId();
        if (!pool.ContainsKey(gameObjKey))
        {
            pool.Add(gameObjKey, new Queue<GameObject>());
            for (int i = 0; i < numberOfObj; i++)
            {
                var go = Instantiate(gameObj) as GameObject;
                go.SetActive(false);
                pool[gameObjKey].Enqueue(go);
            }
        }
    }
    public void Reuse(EntityId gameObjKey, Vector3 position, Quaternion orientation)
    {
        GameObject go = pool[gameObjKey].Dequeue();
        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = orientation;
        pool[gameObjKey].Enqueue(go);
    }
    public void Reuse(EntityId gameObjKey)
    {
        GameObject go = pool[gameObjKey].Dequeue();
        go.SetActive(true);
        pool[gameObjKey].Enqueue(go);
    }

    private void Update()
    {
        foreach (var item in pool)
        {
            foreach (var element in item.Value)
            {
                if (element.transform.position.z + 25f < cam.transform.position.z)
                {
                    element.SetActive(false);
                }
            }
        }
    }
}
