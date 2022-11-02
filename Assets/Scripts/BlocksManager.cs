using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    [Header("Snake manager")]

    public SnakeMovement SM;
    public float distanceSnakeBarrier;

    [Header("BlockPrefab")]

    public GameObject BlockPrefab;

    [Header("Time To SpawnDelegate management")]

    public float minSpawnTime;
    public float maxSpawnTime;
    private float thisTime;
    private float randomTime;

    [Header("Snake Value for Spawning")]

    public int minSpawnDist;
    Vector3 previousSnakePos; // �������� ������ 2 �� ������3
    public List<Vector3> SimpleBoxPositions = new List<Vector3>(); // �������� ������ Boo.Lang.List �� ������ List


    private void Start()
    {
        thisTime = 0;

        SpawnBarrier();

        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
        if (GameController.gameState == GameController.GameState.GAME)
        {
            if (thisTime < randomTime)
            {
                thisTime += Time.deltaTime;
            }
            else
            {
                SpawnBlocks();
                thisTime = 0;
                randomTime = Random.Range (minSpawnTime, maxSpawnTime);
            }

            if (SM.transform.childCount > 0)
            {
                if (SM.transform.GetChild(0).position.y - previousSnakePos.y > minSpawnDist)
                {
                    SpawnBarrier();
                }
            }
        }
    }

    public void SpawnBarrier()
    {
        float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
        float distBetweenBlocks = screenWidthWorldPos / 5;

        for (int i = -2; i < 3; i++)
        {
            float x = 2 * i * distBetweenBlocks;
            float y = 0;

            if (SM.transform.childCount > 0)
            {
                y = (int)SM.transform.GetChild(0).position.y + distBetweenBlocks * 2 + distanceSnakeBarrier;
                if (Screen.height / Screen.width == 4 / 3)
                {
                    y *= 4 / 3f;
                }
            }

            Vector3 spawnPos = new Vector3 (x, y, 0);
            GameObject boxInstance = Instantiate (BlockPrefab, spawnPos, Quaternion.identity, transform);

            if (SM.transform.childCount > 0)
            {
                previousSnakePos = SM.transform.GetChild(0).position;
            }
        }
    }

    public void SpawnBlocks ()
    {
        float screenWidthWorldPos = Camera.main.orthographicSize * Screen.width / Screen.height;
        float distBetweenBlocks = screenWidthWorldPos / 5;

        int random;
        random = Random.Range(-2, 3);

        float x = 2 * random * distBetweenBlocks;
        float y = 0; 
        //�������� ������������ Z

        if (SM.transform.childCount > 0)
        {
            y = (int)SM.transform.GetChild(0).position.y + distBetweenBlocks * 2 + distanceSnakeBarrier;
            if (Screen.height / Screen.width == 4 / 3)
            {
                y *= 2;
            }
        }

        Vector3 SpawnPos = new Vector3 (x, y, 0);
        bool canSpawnBlock = true;

        if (SimpleBoxPositions.Count == 0)
        {
            SimpleBoxPositions.Add (SpawnPos);
        }
        else
        {
            for (int k = 0; k < SimpleBoxPositions.Count; k++)
            {
                if (SpawnPos == SimpleBoxPositions[k])
                {
                    canSpawnBlock = false;
                }
            }
        }

        GameObject boxInstance;

        if (canSpawnBlock)
        {
            SimpleBoxPositions.Add(SpawnPos);
            boxInstance = Instantiate(BlockPrefab, SpawnPos, Quaternion.identity, transform);
            boxInstance.name = "SimpleBox";
            boxInstance.tag = "SimpleBox";
            boxInstance.layer = LayerMask.NameToLayer("Default");
            boxInstance.AddComponent<Rigidbody>(); //������ ���������2� �� ���������
            boxInstance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; //������ ���������2� �� ��������� � ���������.�����������2� �� ��� 2�
        }
    }

    public void SetPreviousSnakePosAfterGameover()
    {
        Invoke("PreviousPosInvoke", 0.5f);
    }

    private void PreviousPosInvoke ()
    {
        previousSnakePos.y = SM.transform.GetChild(0).position.y;
    }
}
