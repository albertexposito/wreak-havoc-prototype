using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotationProcessor : BaseInputProcessor<Vector2>
{
    private Camera _camera;
    private Transform _playerTransform;

    public MouseRotationProcessor(Transform playerTransform, Camera camera)
    {
        _playerTransform = playerTransform;
        _camera = camera;
    }

    public override Vector2 ProcessInput(InputAction inputAction)
    {
        Vector2 screenPointer = inputAction.ReadValue<Vector2>();
        Vector2 characterScreenPosition = _camera.WorldToScreenPoint(_playerTransform.position);

        Vector2 pointerDirection = screenPointer - characterScreenPosition;
        pointerDirection.Normalize();

        //Debug.Log($"[MouseRotationProcessor] - screenPointer: {screenPointer} | characterScreenPosition: {characterScreenPosition} | pointerDirectionNorm: {pointerDirection}");

        return pointerDirection;
    }
}
