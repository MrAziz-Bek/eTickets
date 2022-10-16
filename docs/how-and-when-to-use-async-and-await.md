# How and when to use `async` and `await`
When using async and await the compiler generates a state machine in the background. <br/><br/>Here's an example on which I hope I can explain some of the high-level details that are going on:
```
public async Task MyMethodAsync()
{
    Task<int> longRunningTask = LongRunningOperationAsync();
    // independent work which doesn't need the result of LongRunningOperationAsync can be done here

    //and now we call await on the task 
    int result = await longRunningTask;
    //use the result 
    Console.WriteLine(result);
}

public async Task<int> LongRunningOperationAsync() // assume we return an int from this long running operation 
{
    await Task.Delay(1000); // 1 second delay
    return 1;
}
```
OK, so what happens here:
1. `Task<int> longRunningTask = LongRunningOperationAsync();` starts executing LongRunningOperation
2. Independent work is done on let's assume the Main Thread (Thread ID = 1) then `await longRunningTask` is reached. <br/><br/>Now, if the `longRunningTask` hasn't finished and it is still running, `MyMethodAsync()` will return to its calling method, thus the main thread doesn't get blocked. When the `longRunningTask` is done then a thread from the ThreadPool (can be any thread) will return to `MyMethodAsync()` in its previous context and continue execution (in this case printing the result to the console).

A second case would be that the `longRunningTask` has already finished its execution and the result is available. When reaching the `await longRunningTask` we already have the result so the code will continue executing on the very same thread. (in this case printing result to console). Of course this is not the case for the above example, where there's a `Task.Delay(1000)` involved <br/><br/>© [stack**overflow**](https://stackoverflow.com/a/19985988)

---
What are Async and Await keywords?
  - async and await are markers which mark code positions from where control should resume after a task (thread) completes

© [Science Library's Notion](https://science-library.notion.site/C-Interview-Questions-and-Answers-25573b7e343341c48b14306f6e628055)