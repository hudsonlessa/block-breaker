using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Parameters
    int blockCount;

    // Cached references
    SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void countBlock() {
        blockCount++;
    }

    public void discountBlock() {
        blockCount--;
        if (blockCount <= 0) sceneLoader.LoadNextScene();
    }
}
