using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGGM : IEnumerable
{
    private int[] arr = { 1, 2, 3 };
    private int index = 0;

    public IEnumerator GetEnumerator()
    {
        yield return arr[0];
        yield return arr[1];
        yield return arr[2];
    }
}

public class CoroutineHandle : IEnumerator
{
    public bool IsDone { get; private set; }
    public object Current { get; }

    public bool MoveNext()
    {
        return !IsDone;
    }

    public void Reset()
    {

    }

    public CoroutineHandle(MonoBehaviour owner, IEnumerator coroutine)
    {
        Current = owner.StartCoroutine(Wrap(coroutine));
    }

    private IEnumerator Wrap(IEnumerator coroutine)
    {
        yield return coroutine;
        IsDone = true;
    }
}

public class SampleTest : MonoBehaviour
{
    private MyGGM ggm = new MyGGM();

    private IEnumerator Start()
    {
        CoroutineHandle task1 = this.RunCorotine(CoA());
        CoroutineHandle task2 = this.RunCorotine(CoB());

        while(!task1.IsDone || !task2.IsDone)
        {
            yield return null;
        }

        //yield return CoA();
        //yield return CoB();

        // yield return task1; 
        // yield return task2;

        Debug.Log("Job Complete");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var a in ggm)
            {
                Debug.Log(a);
            }
        }
    }

    private IEnumerator CoA()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Co A Complete");
    }

    private IEnumerator CoB()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Co B Complete");
    }
}
