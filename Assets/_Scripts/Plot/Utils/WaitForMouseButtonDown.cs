using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForMouseButtonDown : CustomYieldInstruction
{
    private int curr;   // 当前的鼠标点击次数
    private int max;    // 鼠标点击次数的上限

    public WaitForMouseButtonDown(int max)
    {
        curr = 0;
        this.max = max;
    }

    public override bool keepWaiting => CheckKeepWaiting(); // 每一帧都会来访问一次
                                                            // 检查状态
    private bool CheckKeepWaiting()
    {
        if (curr >= max)    // 说明满足了条件
        {
            return false;   // 不需要保持了等待状态了
        }
        if (Input.GetMouseButtonDown(0))
        {
            curr++; // 点击则自增
        }
        return true;    // 保持等待状态
    }
}