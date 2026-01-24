using System.Runtime.InteropServices;
using UnityEngine;

public class DescriptionSO : ScriptableObject
{
    [TextArea(3, 20)]
    [SerializeField][NullRefChecker.Optional] protected string m_Description;
}
