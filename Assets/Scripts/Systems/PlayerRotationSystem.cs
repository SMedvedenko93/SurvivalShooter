using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent> filter;
    private SceneData sceneData;

    private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public void Run()
    {
        foreach (var i in filter)
        {
            ref var player = ref filter.Get1(i);

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            player.Transform.Rotate(Vector3.up * mouseX);
            player.PlayerBody.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
