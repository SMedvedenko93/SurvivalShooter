using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class CaseSpawnSystem : IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private SceneData sceneData;

    private float nextActionTime = 0.0f;
    private GameObject casePrefab;

    private List<Vector3> emptyPositionList = new List<Vector3>();

    public void Run()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += sceneData.configuration.CaseSpawnInterval;

            EcsEntity caseEntity = ecsWorld.NewEntity();
            ref var caseComponent = ref caseEntity.Get<CaseComponent>();
            var caseType = Random.Range(0, 3);

            switch (caseType)
            {
                case 0:
                    casePrefab = sceneData.configuration.CaseArmor;
                    break;
                case 1:
                    casePrefab = sceneData.configuration.CaseHealth;
                    break;
                case 2:
                    casePrefab = sceneData.configuration.CaseWeapon;
                    break;
            }

            emptyPositionList = new List<Vector3>();
            foreach (var position in sceneData.configuration.CaseSpawnPosition)
            {
                if (!sceneData.casePositionList.Contains(position))
                {
                    emptyPositionList.Add(position);
                }
            }

            if (emptyPositionList.Count > 0)
            {
                var positionIndex = Random.Range(0, emptyPositionList.Count);
                GameObject caseGO = Object.Instantiate(casePrefab, emptyPositionList[positionIndex], Quaternion.identity);
                caseGO.GetComponent<CaseView>().ecsWorld = ecsWorld;
                sceneData.casePositionList.Add(caseGO.transform.position);
            }

        }
    }
}