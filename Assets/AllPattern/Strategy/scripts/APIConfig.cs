using UnityEngine;
using UnityEngine.AddressableAssets;
//Addressables 체크필수!!
[CreateAssetMenu(fileName ="startegydata",menuName ="Strategy/Data")]
public  class APIConfig : ScriptableObject
{
    //데이터만
    public HandlerType type;
    public AssetReferenceGameObject prefab;//필요할 때만 로딩,서버에서 에셋 다운로드,DLC 가능
    public Color color;
  
}
