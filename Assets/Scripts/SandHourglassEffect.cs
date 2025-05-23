
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandHourglassEffect : MonoBehaviour
{
    
    public GameObject[] figurePrefab;
    public Button resetButton;
    public Transform spawnPoint;
    public float delayBetweenDrops = 0.2f;
    public List<GameObject> figurePrefabs = new List<GameObject>();
    public List<GameObject> spawnedFigurines = new List<GameObject>();


    public void Start()
    {
        resetButton.interactable = false;
        StartCoroutine(DropFigure());
    }
    public void Reset()
    {
        resetButton.interactable = false;
        RemoveMatching();
        StartCoroutine(DropFigurines());
        
    }
    public void RemoveMatching()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject obj in spawnedFigurines)
        {
            figurePrefabs.Add(obj);
            toRemove.Add(obj);
        }

        foreach (GameObject obj in toRemove)
        {
            spawnedFigurines.Remove(obj);
            obj.SetActive(false);
        }
    }
    public void RemoveMatchingFinish()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject obj in figurePrefabs)
        {
            toRemove.Add(obj);
        }

        foreach (GameObject obj in toRemove)
        {
            figurePrefabs.Remove(obj);
            Destroy(obj);
        }
        resetButton.interactable = true;
    }
    IEnumerator DropFigurines()
    {
        foreach (GameObject prefab in figurePrefabs)
        {
            GameObject figurine = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            figurine.SetActive(true);
            figurine.transform.localScale = Vector3.one * 0.05f;
            figurine.transform.SetParent(spawnPoint);
            Rigidbody2D rb = figurine.GetComponent<Rigidbody2D>();
            Collider2D col = figurine.GetComponent<Collider2D>();

            if (rb != null) rb.simulated = true;
            if (col != null) col.enabled = true;

            spawnedFigurines.Add(figurine);
            yield return new WaitForSeconds(delayBetweenDrops);
        }
        RemoveMatchingFinish();
    }
    IEnumerator DropFigure()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < figurePrefab.Length; i++)
            {
                GameObject figurine = Instantiate(figurePrefab[i], spawnPoint.position, Quaternion.identity);
                figurine.transform.SetParent(spawnPoint);
                Rigidbody2D rb = figurine.GetComponent<Rigidbody2D>();
                Collider2D col = figurine.GetComponent<Collider2D>();

                if (rb != null) rb.simulated = true;
                if (col != null) col.enabled = true;

                spawnedFigurines.Add(figurine);
                yield return new WaitForSeconds(delayBetweenDrops);
            }
        }
        resetButton.interactable = true;
    }
}
