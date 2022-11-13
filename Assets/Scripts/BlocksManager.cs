using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    public GameObject[] BoxPrefabs;
    public int MinBox;
    public int MaxBox;
    public float DistanceBetweenBox;

    private void Awake()
    {
        int BoxCount = Random.Range(MinBox, MaxBox + 1);

        for (int i = 0; i < BoxCount; i++)
        {
            int prefabIndex = Random.Range(0, BoxPrefabs.Length);
            GameObject Block = Instantiate(BoxPrefabs[prefabIndex], transform);
            Block.transform.localPosition = new Vector3(0, 0, DistanceBetweenBox * i);
        }
    }
}
