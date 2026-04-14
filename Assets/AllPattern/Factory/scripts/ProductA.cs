using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ProductA : MonoBehaviour, IProduct
{
    [SerializeField] private string productName = "A";
    public string ProductName { get => productName; set => productName = value; }
    private ParticleSystem ParticleSystem;

    public void Intialize()
    {
        gameObject.name = productName;

        ParticleSystem = GetComponentInChildren<ParticleSystem>();
    
        if (ParticleSystem == null) return;
        ParticleSystem.Stop();
     
        ParticleSystem.Play();
       
    }

}
