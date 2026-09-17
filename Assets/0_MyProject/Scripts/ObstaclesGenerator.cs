using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstaclesGenerator : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float minLenght;
    public float maxLenght;



    public GameObject Player;
    public GameObject Stage;
    public GameObject Crystal;
    public GameObject CrystalStage;
    public List<GameObject> Obstacles;

    public int colums;
    public int rows;

    void Start()
    {
        Init();    
    }


    private void Init()
    {
         // Получаем все Box Collider на объекте и его дочерних объектах
        BoxCollider[] colliders = Stage.GetComponentsInChildren<BoxCollider>();


        var rightCoord = CoordBox.GetCoordX(colliders[0]) ;
        var leftCoord = CoordBox.GetCoordX(colliders[1]);
        var coords = new CoordBox(minLenght + Stage.transform.position.z, maxLenght + Stage.transform.position.z, leftCoord, rightCoord);

        var gridCoord = GenerateGrid(coords, colums, rows);

        SpawnObjects(gridCoord, Obstacles, Crystal);
    }

    public void SpawnObjects(List<Vector3> coords, List<GameObject> obstacles, GameObject crystal)
    {
        foreach (var coord in coords)
        {
            var chance = Random.Range(0, 100);
            if (chance < 50)
            {
                GameObject spawnedObject = Instantiate(crystal,
                    new Vector3(coord.y, CrystalStage.transform.position.x + 5, coord.x), Quaternion.Euler(90, 0, 0));
                spawnedObject.transform.SetParent(transform);
            }
            else
            {
                var rnd = Random.Range(0, obstacles.Count);
                GameObject spawnedObject = Instantiate(Obstacles[rnd],
                    new Vector3(coord.y, CrystalStage.transform.position.x + 15, coord.x), Quaternion.Euler(-90, 0, 0));
                spawnedObject.transform.SetParent(transform);
            }
        }
    }

    public List<Vector3> GenerateGrid(CoordBox coord, int columns, int rows)
    {
        List<Vector3> grid = new List<Vector3>();

        // Вычисление размеров области
        float width = coord.xMax - coord.xMin;
        float height = coord.zMax - coord.zMin;

        // Размер ячейки
        float cellWidth = width / columns;
        float cellHeight = height / rows;

        // Генерация координат центров ячеек
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Центр ячейки
                float x = coord.xMin + (col + 0.5f) * cellWidth;
                float y = coord.zMin + (row + 0.5f) * cellHeight;

                grid.Add(new Vector3(x, y, 0));
            }
        }

        return grid;
    }
}

public class CoordBox
{
    public float xMin; // дальняя точка от игрока
    public float xMax; // ближняя точка от игрока
    public float zMin; // left
    public float zMax; // right

    public CoordBox(float xMin, float xMax, float zMin, float zMax)
    {
        this.xMin = xMin;
        this.xMax = xMax;
        this.zMin = zMin;
        this.zMax = zMax;


        Debug.Log("left: " + zMin);
        Debug.Log("right: " + zMax);
        Debug.Log("xMax: " + xMax);
        Debug.Log("xMin: " + xMin);
    }

    public static float GetCoordX(BoxCollider collider)
    {
        // Локальная позиция центра текущего Box Collider
        Vector3 localCenter = collider.center;

        // Мировая позиция центра текущего Box Collider
        Vector3 worldCenter = collider.transform.TransformPoint(localCenter);

        // Координата X в мировом пространстве
        return worldCenter.x;
    }
}
