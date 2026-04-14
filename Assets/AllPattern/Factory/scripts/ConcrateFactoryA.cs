using UnityEngine;

public class ConcrateFactoryA : Factory
{
    [SerializeField] private ProductA prefab;
    public override IProduct GetProduct(Vector3 vector)
    {
        GameObject obj = Instantiate(prefab.gameObject, vector, Quaternion.identity);
        ProductA proA = obj.GetComponent<ProductA>();
        proA.Intialize();
        return proA;
    }

}
