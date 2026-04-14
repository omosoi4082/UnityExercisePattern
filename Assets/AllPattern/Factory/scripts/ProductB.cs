using UnityEngine;

public class ProductB : MonoBehaviour,IProduct
{
    [SerializeField] private string productName = "B";
    public string ProductName { get => productName; set => productName = value; }
    private AudioSource audioSource;
    public void Intialize()
    {
        gameObject.name = productName;
        audioSource = GetComponent<AudioSource>();  
        if (audioSource == null ) { return; }
        audioSource.Stop();
        audioSource.Play();
    }

}
