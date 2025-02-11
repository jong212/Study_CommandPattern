using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute(); // 실행
    void Undo();    // 실행 취소
}
