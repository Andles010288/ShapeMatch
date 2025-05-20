
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickableFigurine : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource audioCollision;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 1.0f)
        {
            //AudioSource audio = GetComponent<AudioSource>();
            if (audioCollision != null) audioCollision.Play();
        }
    }
    public void OnMouseDown()
    {
        if (gameManager != null)
        {
            gameManager.OnFigurineClicked(this.gameObject);
            
            StartCoroutine(DestroyGO());
        }
    }
    IEnumerator DestroyGO()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
