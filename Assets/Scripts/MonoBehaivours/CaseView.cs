using Leopotam.Ecs;
using UnityEngine;

public class CaseView : MonoBehaviour
{
    public EcsWorld ecsWorld { get; set; }
    public string type;

    private void OnTriggerEnter(Collider other)
    {
        var hit = ecsWorld.NewEntity();

        ref var tryTakeCase = ref hit.Get<TryTakeCase>();

        tryTakeCase.Type = type;

        Destroy(gameObject);
    }

}
