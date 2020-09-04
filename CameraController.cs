using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private GameManager gameManager;
    private GameObject rat1;
    private GameObject rat2;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rat1 = gameManager.rat1;
        rat2 = gameManager.rat2;

        // Find average position of both rats
        Vector3 centerPosition = Vector3.zero;
        if (rat1 != null)
            centerPosition += rat1.transform.position;
        if (rat2 != null)
            centerPosition += rat2.transform.position;
        centerPosition /= 2;

        centerPosition.z = -10;

        // Glide camera toward new position
        transform.position = Vector3.Lerp(transform.position, centerPosition, Time.deltaTime * moveSpeed);
    }
}
