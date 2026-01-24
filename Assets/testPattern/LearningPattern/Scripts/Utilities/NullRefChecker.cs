using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 필수 필드 검사 오류 로그 출력
/// 직렬화 필드(인스펙터에 노출), [Optional]이 붙어있다면 적용
/// </summary>
public static class NullRefChecker
{
    public static void Validate(object instance)
    {
        //현대 클래스에 선언된 모든 변수 정보 모음(public, private 가리지 않고)
        FieldInfo[] fields=instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public |BindingFlags.NonPublic);
        foreach (FieldInfo field in fields)
        {
            //인스펙터에 노출 되지 않거나 변수위에[Optional]이 붙어있다면 , 값이 비어도 continue넘어감
            if (!field.IsDefined(typeof(SerializeField),true)||field.IsDefined(typeof(OptionalAttribute),true))
            {
                continue;
            }
            if(field.GetValue(instance)==null)
            {
                //MonoBehaviour 일경우 
                if (instance is MonoBehaviour behaviour)
                {
                    GameObject gameObject = behaviour.gameObject;
                    Debug.LogError($"Missing assignment for field: {field.Name} in component: {instance.GetType().Name} on GameObject: " +
                           $"{gameObject}", gameObject);

                }
                //ScriptableObject 일경우
                else if (instance is ScriptableObject scriptable)
                {
                    Debug.LogError($"Missing assignment for field: {field.Name} on ScriptableObject:  " +
                          $"{scriptable.name} ({instance.GetType().Name})");
                }
                else//c#단순 클레스
                {
                    Debug.LogError($"Missing assignment for field: {field.Name} in object: {instance.GetType().Name}");

                }
            }
        }
    }
    //PropertyAttribute유니티 인스펙터 창에 나타나는 변수(Property) 전용
    public class OptionalAttribute :PropertyAttribute { }
}