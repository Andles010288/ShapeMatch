using UnityEngine;
using UnityEngine.UI;

public class FigureView : MonoBehaviour
{
    public SpriteRenderer shapeRenderer;
    public Text animalText;

    public void Setup(Figure figure)
    {
        // ��������� ������: ����, �����, ��������
        shapeRenderer.color = GetColor(figure.color);
        shapeRenderer.sprite = GetShapeSprite(figure.shape);
        animalText.text = figure.animal;
    }

    private Color GetColor(string colorName)
    {
        return colorName switch
        {
            "Red" => Color.red,
            "Blue" => Color.blue,
            "Green" => Color.green,
            _ => Color.white
        };
    }

    private Sprite GetShapeSprite(string shape)
    {
        // �����������, � ���� ������� ��������� �������
        return Resources.Load<Sprite>($"Shapes/{shape}");
    }
}
