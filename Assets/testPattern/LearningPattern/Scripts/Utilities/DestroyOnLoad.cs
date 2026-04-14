using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Additive 씬 로딩 시 테스트용/임시 오브젝트가
/// 다른 씬에서도 중복으로 유지되는 것을 방지하기 위해,
/// 지정한 씬이 아닐 경우 해당 오브젝트를 자동으로 제거하는 컴포넌트
/// </summary>
public class DestroyOnLoad : MonoBehaviour
{
    [Tooltip("오브젝트가 살아있어도 되는 씬")]
    [SerializeField] private string _activeWithinScene;
    [Tooltip("파괴될 대상 지정없을시 자신")]
    [SerializeField] private GameObject _objectToDestroy;
    [Tooltip("활성 유무 디버그 메세지")]
    [SerializeField] private bool _showDebug;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != _activeWithinScene)
        {
            if (_objectToDestroy == null)
                _objectToDestroy = gameObject;

            Destroy(_objectToDestroy);

            if (_showDebug)
            {
                Debug.Log("Active scene: " + SceneManager.GetActiveScene().name);
                Debug.Log("Do not destroy in scene: " + _activeWithinScene);
                Debug.Log("Destroy on load: " + _objectToDestroy);
            }
        }
    }

}
