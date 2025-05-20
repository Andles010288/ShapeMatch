using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarManager : MonoBehaviour
{
    public static ActionBarManager Instance;
    public Transform[] slots;
    public GameObject winPanel;
    public GameObject losePanel;

    private List<GameObject> bar = new List<GameObject>();

    void Awake() => Instance = this;

    public void TryAddToBar(GameObject figurine)
    {
        if (bar.Count >= 7)
        {
            losePanel.SetActive(true);
            return;
        }

        GameObject clone = Instantiate(figurine, slots[bar.Count].position, Quaternion.identity, slots[bar.Count]);
        clone.transform.localScale = Vector3.one * 0.8f;
        bar.Add(clone);

        CheckForMatch();
    }

    void CheckForMatch()
    {
        if (bar.Count < 3) return;

        int count = bar.Count;
        GameObject a = bar[count - 1];
        GameObject b = bar[count - 2];
        GameObject c = bar[count - 3];

        if (a.name == b.name && b.name == c.name)
        {
            Destroy(a);
            Destroy(b);
            Destroy(c);
            bar.RemoveRange(count - 3, 3);
            // Optional: Play match sound/effect
        }

        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (GameObject.FindGameObjectsWithTag("Figurine").Length == 0)
        {
            winPanel.SetActive(true);
        }
    }
}