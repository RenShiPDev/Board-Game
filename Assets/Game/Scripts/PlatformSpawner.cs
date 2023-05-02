using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject finishPrefab;

    [SerializeField] private Vector2 _startPos;

    private void Start()
    {
        SpawnPlatforms();
    }

    private void SpawnPlatforms()
    {
        bool isRight = false;

        for(int i = 0; i < Mathf.Abs(_startPos.x)*2; i+=2)
        {
            for(int j = 0; j < Mathf.Abs(_startPos.y)*2; j++)
                SpawnPlatform(new Vector2(_startPos.y+i, _startPos.x+j), platformPrefab);

            if(Mathf.Abs(_startPos.x)*2 > i+2)
            {
                if(isRight)
                    SpawnPlatform(new Vector2(_startPos.y+i+1, _startPos.x), platformPrefab);
                else
                    SpawnPlatform(new Vector2(_startPos.y+i+1, Mathf.Abs(_startPos.x)-1), platformPrefab);
            }
            else
            {
                SpawnPlatform(new Vector2(_startPos.y+i+1, Mathf.Abs(_startPos.x)-1), finishPrefab);
            }

            isRight = !isRight;
        }
    }

    private void SpawnPlatform(Vector2 pos, GameObject prefab)
    {
        var newPos = new Vector3(pos.x, prefab.transform.position.y, pos.y);
        GameObject platform = Instantiate(prefab, newPos, Quaternion.identity);
    }
}
