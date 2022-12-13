using UnityEngine;

public class SoundData : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip AttackMelleClip;
    public AudioClip AttackRangeClip;
    public AudioClip EmptyClip;
    public AudioClip ReloadClip;
    public AudioClip HitReactEnemyClip;
    public AudioClip HitReactPlayerClip;
    public AudioClip DeathEnemyClip;

    public void Attack(string type)
    {
        if (type == "Mellee")
        {
            Audio.PlayOneShot(AttackMelleClip);
        }
        else
        {
            Audio.PlayOneShot(AttackRangeClip);
        }
    }

    public void Empty()
    {
        Audio.PlayOneShot(EmptyClip);
    }

    public void Reload()
    {
        Audio.PlayOneShot(ReloadClip);
    }

    public void HitReact(string target)
    {
        if (target == "Enemy")
        {
            Audio.PlayOneShot(HitReactEnemyClip);
        }
        else
        {
            Audio.PlayOneShot(HitReactPlayerClip);
        }
    }

    public void DeathEnemy()
    {
        Audio.PlayOneShot(DeathEnemyClip);
    }
}