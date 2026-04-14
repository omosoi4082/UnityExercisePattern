using UnityEngine;

public class ConcrateFactoryB : Factory
{
    [SerializeField]private ProductB prefab;

    public override IProduct GetProduct(Vector3 vector)
    {
       GameObject obj=Instantiate(prefab.gameObject,vector,Quaternion.identity);
       ProductB product=obj.GetComponent<ProductB>();   

        product.Intialize();
        obj.name = product.ProductName;
        return product; 
    }

}
