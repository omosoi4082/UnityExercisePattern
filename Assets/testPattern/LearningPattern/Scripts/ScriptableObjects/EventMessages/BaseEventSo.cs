using System;
using UnityEngine;

public abstract class BaseEventSo : DescriptionSO
{
    public event Action EventRaised;
    [Space]
    [SerializeField] protected bool m_Debuglog;

    //BaseEventSo선언
    public BaseEventSo()
    {
        //빈 함수로 초기화 ->없으면 에러 발생 
        EventRaised += () => { };
    }

    //사용
    public virtual void OnEventRaised()
    {
        EventRaised?.Invoke();
    }
}
