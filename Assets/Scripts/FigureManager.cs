using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    public GameObject figurePrefab;          // Префаб фигурки с Rigidbody2D
    public Transform spawnTopPoint;          // Верхняя точка спавна (над полем)
    public float spawnInterval = 0.2f;       // Задержка между фигурками
    public int rows = 6;
    public int cols = 6;

    private Figure[,] fieldData;

    public void StartLevel(Figure[,] fieldData)
    {
        this.fieldData = fieldData;
        StartCoroutine(SpawnFiguresCoroutine());
    }

    IEnumerator SpawnFiguresCoroutine()
    {
        int total = rows * cols;
        for (int i = 0; i < total; i++)
        {
            int row = i / cols;
            int col = i % cols;
            Figure fig = fieldData[row, col];

            Vector3 spawnPos = spawnTopPoint.position + new Vector3(col * 1f, 0, 0);
            GameObject go = Instantiate(figurePrefab, spawnPos, Quaternion.identity);
            go.GetComponent<FigureView>().Setup(fig);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
