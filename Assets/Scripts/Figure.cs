using UnityEngine;

[System.Serializable]
public class Figure
{
    public string shape;
    public string color;
    public string animal;

    public Figure(string shape, string color, string animal)
    {
        this.shape = shape;
        this.color = color;
        this.animal = animal;
    }

    public override string ToString()
    {
        return $"({shape}, {color}, {animal})";
    }
}
