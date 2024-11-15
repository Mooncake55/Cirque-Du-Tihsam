using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowClown : MonoBehaviour
{
    public Transform player; 
    float followSpeed = 0.1f;
    public Collider2D confiner;
    public int pixelPerUnit = 100;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = Vector3.Lerp(transform.position, player.position, followSpeed);

            Vector2 minBounds = confiner.bounds.min;
                Vector2 maxBounds = confiner.bounds.max; 
            float cameraHalfWidth = _camera.orthographicSize * _camera.aspect; 
            float cameraHalfHeight = _camera.orthographicSize; 
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth); 
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

            targetPosition.x = Mathf.Round(targetPosition.x * pixelPerUnit) / pixelPerUnit;
            targetPosition.y = Mathf.Round(targetPosition.y * pixelPerUnit) / pixelPerUnit;
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;
        }

    }
}
