using System.Collections.Generic;
using UnityEngine;

public class ClickToCreate : MonoBehaviour
{
    [SerializeField] private LayerMask layerToClick;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Factory[] factories;
    private List<GameObject> createdProducts = new List<GameObject>();


    void Update()
    {
        GetProductAtClick();
    }

    private void GetProductAtClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Factory seletfactory = factories[Random.Range(0, factories.Length)];
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,Mathf.Infinity,layerToClick)&&seletfactory!=null)
            {
                IProduct product = seletfactory.GetProduct(hit.point + offset);
                if(product is Component component)
                {
                    createdProducts.Add(component.gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var item in createdProducts)
        {
            Destroy(item);
        }
        createdProducts.Clear();
    }
}
