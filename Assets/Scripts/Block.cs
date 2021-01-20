using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] AudioClip breakClip;
    [SerializeField] GameObject sparklesEffect;
    [SerializeField] Sprite[] hitSprites;

    // Cached references
    Level level;
    GameSession gameSession;

    // State variables
    int timesHit;

    private void Start() {
        gameSession = FindObjectOfType<GameSession>();
        AddToBlockCounter();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        HandleHit();
    }

    private void AddToBlockCounter() {
        level = FindObjectOfType<Level>();

        if (tag != "Unbreakable") {
            level.countBlock();
        }
    }

    private void HandleHit() {
        if (tag != "Unbreakable") {
            int maxHits = hitSprites.Length + 1;

            timesHit++;

            if (timesHit >= maxHits) {
                DestroyBlock();
            }
            else {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite() {
        int index = timesHit - 1;
        if (hitSprites[index] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[index];
        }
        else {
            Debug.LogError("Block sprite is missing from array. - " + gameObject.name);
        }
    }

    private void DestroyBlock() {
        AudioSource.PlayClipAtPoint(breakClip, Camera.main.transform.position);
        TriggerSparkles();
        Destroy(gameObject);
        gameSession.AddToScore();
        level.discountBlock();
    }

    private void TriggerSparkles() {
        GameObject sparkles = Instantiate(sparklesEffect, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
