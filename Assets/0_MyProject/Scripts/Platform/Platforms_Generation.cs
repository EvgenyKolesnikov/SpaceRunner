using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms_Generation : MonoBehaviour
{
    public int platform_lenght = 500; //длина платформы
    public int destroy_offset = 30;  //при значение 0 платформа уничтожается прямо за игроком, что попадает в поле обзора камеры. Рекомендуется значение >20
    public int countPlatforms;

    public GameObject Player;
    public ObjectPool _Objectpool;
    public Platforms_Moving platforms_Moving;

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (Player != null) //При смерти платформы останавливаются
        {
            if (ObjectPool.Stages_Queue.Peek().transform.position.z < -platform_lenght / countPlatforms + Player.transform.position.z - destroy_offset)
            {
                _Objectpool.ReturnObject();

                if (ObjectPool.Stages_Queue.Count < countPlatforms)
                {
                    _Objectpool.GetObject(new Vector3(0, 0, ObjectPool.Stages_Queue.Peek().transform.position.z + platform_lenght * (countPlatforms - 1)), Quaternion.identity); 
                }
            } 
        }                                                                                                                          
    }

    private void Init()
    {
        for(int i = 1; i <= countPlatforms; i++)
        {
            _Objectpool.GetObject(new Vector3(0, 0, platform_lenght * i), Quaternion.identity);
        }
    }
}
