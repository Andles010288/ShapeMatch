using System.Collections.Generic;
using UnityEngine;

public class FigureGenerator : MonoBehaviour
{
    public int rows = 6;
    public int cols = 6;

    private List<string> shapes = new List<string> { "Circle", "Square", "Triangle" };
    private List<string> colors = new List<string> { "Red", "Blue", "Green" };
    private List<string> animals = new List<string> { "Cat", "Dog", "Fox" };

    void Start()
    {
        Figure[,] field = GenerateField(rows, cols);
        PrintField(field);
    }

    List<Figure> GenerateUniqueFigures()
    {
        List<Figure> uniqueFigures = new List<Figure>();
        foreach (var shape in shapes)
        {
            foreach (var color in colors)
            {
                foreach (var animal in animals)
                {
                    uniqueFigures.Add(new Figure(shape, color, animal));
                }
            }
        }
        return uniqueFigures;
    }

    Figure[,] GenerateField(int rows, int cols)
    {
        int totalCells = rows * cols;
        if (totalCells % 3 != 0)
        {
            Debug.LogError("Total cells must be divisible by 3!");
            return null;
        }

        int figureCount = totalCells / 3;
        List<Figure> uniqueFigures = GenerateUniqueFigures();

        Shuffle(uniqueFigures);
        List<Figure> selectedFigures = uniqueFigures.GetRange(0, figureCount);

        // 3 экземпляра каждой
        List<Figure> allFigures = new List<Figure>();
        foreach (var fig in selectedFigures)
        {
            allFigures.Add(fig);
            allFigures.Add(fig);
            allFigures.Add(fig);
        }

        Shuffle(allFigures);

        Figure[,] field = new Figure[rows, cols];
        int index = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                field[r, c] = allFigures[index];
                index++;
            }
        }

        return field;
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }

    void PrintField(Figure[,] field)
    {
        if (field == null) return;

        for (int r = 0; r < field.GetLength(0); r++)
        {
            string rowStr = "";
            for (int c = 0; c < field.GetLength(1); c++)
            {
                rowStr += field[r, c].ToString() + " ";
            }
            Debug.Log(rowStr);
        }
    }
}
