
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets; // 필수
using UnityEngine.ResourceManagement.AsyncOperations; // 필수


public class AddressableLoder : MonoBehaviour
{
    public string addressName = "MyCube";
    private List<GameObject> _createdCubes = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            OnLoadCompleted();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            RemoveCube();
        }
    }

    private void OnLoadCompleted()
    {
        Addressables.InstantiateAsync(addressName).Completed += (h) =>
        {
            if (h.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject spawnedCube = h.Result;
                spawnedCube.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                _createdCubes.Add(spawnedCube);
            }
            else
            {
                Debug.Log("실패");
            }
        };
      
    }
    private void RemoveCube()
    {
        if(_createdCubes.Count>0)
        {
            GameObject cubeToDestroy = _createdCubes[0];
            // 어드레서블 전용 삭제 함수 (Destroy 대신 이것을 써야 메모리가 해제됨)
            Addressables.ReleaseInstance(cubeToDestroy);
            _createdCubes.Remove(cubeToDestroy);
        }
    }
}
