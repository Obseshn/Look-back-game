using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLighterMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos + offset;
    }
}
