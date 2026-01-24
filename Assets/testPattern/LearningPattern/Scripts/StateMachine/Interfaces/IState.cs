using System.Collections;
using UnityEngine;

public interface IState
{
    //"Current State"시 먼저호출, 상태 요구사항 설정 
    void Enter();
    //enter이후 주요 로직 
    IEnumerator Execute();
    //다음 상태 이동시 호출,정리할때 사용
    void Exit();
    //전환 링큰 추가
    void AddLink(ILink link);
    //링크 제거
    void RemoveLink(ILink link);
    //모든 링크 제거
    void RemoveAllLinks();
    //상태를 체크해서 다음 상태로 이동 
    bool Validateinks(out IState nextState);
    //링크 활성화
    void EnableLinks();
    //링크 비활성화
    void DisableLinks();

}