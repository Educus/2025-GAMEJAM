using UnityEngine;

public interface St3EliteState
{
    void OnEnter(St3EliteController elite); // 상태 진입 시 호출
    void OnUpdate();                        // 매 프레임 호출
    void OnExit();                          // 상태 종료 시 호출
}