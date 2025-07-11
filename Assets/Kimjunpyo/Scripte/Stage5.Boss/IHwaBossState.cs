using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 보스 상태 인터페이스. 모든 상태는 이 인터페이스를 구현해야 함.
public interface IHwaBossState
{
    void OnEnter(HwaBossController boss); // 상태 진입 시 호출
    void OnUpdate(); // 프레임 마다 호출 
    void OnExit();  // 상태 종료 시 호출
}