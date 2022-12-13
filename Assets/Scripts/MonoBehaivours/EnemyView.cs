using Leopotam.Ecs;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    public EcsEntity entity;
    public SceneData sceneData;
    private bool soundDone;
    [SerializeField]
    public Collider colliderHead, colliderBody;

    public void Update()
    {
        ref var health = ref entity.Get<Health>();
        if(health.Value <= 0)
        {
            colliderHead.enabled = false;
            colliderBody.enabled = false;
            if (!soundDone)
            {
                sceneData.points++;
                sceneData.soundData.DeathEnemy();
                sceneData.uiData.SetPointsTextValue(sceneData.points);
            }
            soundDone = true;
            GetComponent<NavMeshAgent>().speed = 0;
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        entity.Destroy();
        Destroy(gameObject);
    }
}
