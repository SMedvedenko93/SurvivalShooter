using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public GameObject player;
    public int points;

    public Configuration configuration;
    public Camera mainCamera;

    public float nextTimeToFire;
    public Transform projectileSpawnPosition;

    public List<Vector3> casePositionList = new List<Vector3>();

    public UIData uiData;
    public SoundData soundData;
}