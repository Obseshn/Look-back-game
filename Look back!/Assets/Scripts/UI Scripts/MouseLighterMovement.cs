using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLighterMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private Camera mainCamera;

    private Vector3 mousePos;

    private string TAG_MainCamera = "MainCamera";

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(TAG_MainCamera).GetComponent<Camera>();
    }
    private void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos + offset;
    }
}
