using UnityEngine;

public class ProjectileTimerDestroy : MonoBehaviour
{
    public float timeLive;

    public void Start()
    {

    }

    public void Update()
    {
        timeLive -= Time.deltaTime;
        if (timeLive <= 0)
        {
            Destroy(transform.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.gameObject);
    }
}
