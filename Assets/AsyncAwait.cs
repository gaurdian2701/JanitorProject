using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncAwait : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log(AsyncFunc1());
        var value = await Task.Run(async () =>
        {
            Debug.Log("Task 3 started. Wait for 3 seconds...");
            await Task.Delay(3000);
            Debug.Log("Task 3 Completed. Returning value: ");
            return 30;
        });
                Debug.Log(value);
        Func2();
    }

    //Func1 and Func2 run in background asynchronously if await is not used. But you cant get the return value for Func 1 since you dont use await.
    //Instead, you could use a lambda function
    //Task 3 uses an async lambda function and since we use await, we actually get the return value
    private async void Func2()
    {
        Debug.Log("Task 2 Started. Wait for 2 seconds...");
        await Task.Delay(2000);
        Debug.Log("Task 2 Completed");
    }

    private async Task<int> AsyncFunc1()
    {
        Debug.Log("Task 1 started. Wait for 3 seconds...");
        await Task.Delay(3000);
        Debug.Log("Task 1 Completed. Returning value: ");
        return 30;
    }
}
