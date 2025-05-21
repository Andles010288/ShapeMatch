
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ricimi;

public class GameManager : MonoBehaviour
{
    public SandHourglassEffect SandHourglassEffect;
    public PopupOpener PopupOpenerWin;
    public PopupOpener PopupOpenerLose;
    public Transform actionBar;
    public AudioSource audioPopupOpener;
    public int maxSlots = 7;

    public List<GameObject> actionSlots = new List<GameObject>();

    public void OnFigurineClicked(GameObject figurine)
    {
        if (actionSlots.Count >= maxSlots)
        {
            Debug.Log("Проигрыш: бар заполнен.");
            audioPopupOpener.Play();
            PopupOpenerLose.OpenPopup();
            return;
        }
        GameObject copy = Instantiate(figurine, actionBar);
        copy.transform.localScale = Vector3.one * 0.5f;
        copy.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        copy.GetComponent<Collider2D>().enabled = false;
        if (copy.GetComponent<Rigidbody2D>() != null)
        {
            Destroy(copy.GetComponent<Rigidbody2D>());
        }
        actionSlots.Add(copy);
        SandHourglassEffect.spawnedFigurines.Remove(figurine);

        CheckForMatches();
        CheckWinCondition();
    }

    void CheckForMatches()
    {
        for (int i = 0; i < actionSlots.Count; i++)
        {
            int matchCount = 1;
            string tag = actionSlots[i].tag;

            for (int j = i + 1; j < actionSlots.Count; j++)
            {
                if (actionSlots[j].tag == tag)
                {
                    matchCount++;
                }
            }

            if (matchCount >= 3)
            {
                RemoveMatching(tag);
                break;
            }
        }
    }

    void RemoveMatching(string tag)
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject obj in actionSlots)
        {
            if (obj.tag == tag && toRemove.Count < 3)
            {
                toRemove.Add(obj);
            }
        }

        foreach (GameObject obj in toRemove)
        {
            actionSlots.Remove(obj);
            Destroy(obj);
        }
    }

    void CheckWinCondition()
    {
        if (SandHourglassEffect.spawnedFigurines.Count == 0)
        {
            Debug.Log("Победа: поле очищено!");
            audioPopupOpener.Play();
            PopupOpenerWin.OpenPopup();
        }
    }
    public void Lose()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
