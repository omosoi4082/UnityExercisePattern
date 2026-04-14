using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    [SerializeField] private GameObject pathPointPrefab;
    [SerializeField] Transform pathTransform;
    [SerializeField] Vector3 offset;
    private Stack<GameObject>pathObj= new Stack<GameObject>();//마지막꺼냄

    [SerializeField]private LineRenderer lineRenderer;
    [SerializeField] List<Vector3> pointList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddToPath(transform.position);
    }


    public void AddToPath(Vector3 pos)
    {
        if (pathPointPrefab == null) return;
        GameObject newObj = Instantiate(pathPointPrefab, pos + offset, Quaternion.identity);
        pathObj.Push(newObj);   

        if(pathTransform!=null)
        {
            newObj.transform.parent = pathTransform;    
        }
        pointList.Add(newObj.transform.position);

        lineRenderer.positionCount=pointList.Count;//점갯수
        lineRenderer.SetPosition(pointList.Count - 1, pointList[pointList.Count-1]);//그리기
    }
    public void ReMoveFromPath()
    {
        GameObject lastObj=pathObj.Pop();
        pointList.RemoveAt(pointList.Count - 1);
        Destroy(lastObj);

        lineRenderer.positionCount = pointList.Count;
    }

    
}
