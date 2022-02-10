using Feiyu.DoCoroutine;
using Feiyu.Util;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

public class TestDo : IRun
{
    float deltaTime = 1 / 40f;
    public void Run()
    {
        DoManager.Instance.SetDeltaTime(deltaTime);
        int i = 0; Do.Run(()=> { Console.WriteLine($"{i++}s"); },0,1,2000);
        Test03();
        while (true)
        {
            DoManager.Instance.Update();
            System.Threading.Thread.Sleep((int)(deltaTime * 1000f));
        }
    }

    IEnumerator PrintTime()
    {
        for (int i = 0; true; i++)
        {
            Console.WriteLine($"{i}s");
            yield return Do.Wait(1);
        }
    }

    void Test01()
    {
        Do.Run(() => Console.WriteLine("----Action01"), 1);
        Do.Run(() => Console.WriteLine("----Action02"), 1);
        Do.Run(() => Console.WriteLine("----Repeat01"), 1, 1, 2700);
    }

    void Test02()
    {
        Do.Run(IE02());
    }

    void Test03()
    {
        Do.Run(IE03());
    }

    void Test04()
    {
        Do.Run(IE04());
    }

    void Test05()
    {
        Do.Run(IE05());
    }

    void Test06()
    {
        Do.Run(IE06());
    }

    void Test07()
    {
        Do doo01 = new Do(() => Console.WriteLine("----Action01"), 1);
        Do doo02 = new Do(() => Console.WriteLine("----Action02"), 1);//不会被执行
        Do.Run(doo01);
        Do.Run(() => Console.WriteLine("----Action03"), 1, 1, 10);
    }

    void Test08()
    {
        Do.Run(IE08());
    }

    IEnumerator IE02()
    {
        for (int i = 0; i < 2; i++)
        {
            Do.Wait(1f);//无效，没有使用yield return
            yield return Do.Wait(1f);
            yield return Do.Run(() => Console.WriteLine("----Action01"), 1);
        }
    }

    IEnumerator IE03()
    {
        yield return Do.Run(() => Console.WriteLine("----Action03"), 1, 2, 5).OnCompleted(()=>Console.WriteLine("hello world!"));
    }

    IEnumerator IE04()
    {
        yield return Do.Run(() => Console.WriteLine("----Action04"), 1, 1, 1f);
        yield return Do.Run(() => Console.WriteLine("----Action05"), 1, 1, 2f);
        yield return Do.Run(() => Console.WriteLine("----Action06"), 1, 1, 3f);
        yield return Do.Run(IE02());
    }

    IEnumerator IE05()
    {
        yield return Do.Run(IE05_01());
        yield return Do.Run(() => Console.WriteLine("----Action05_00"), 1, 1, 2f);

    }

    IEnumerator IE05_01()
    {
        yield return Do.Run(IE05_02());
        yield return Do.Run(() => Console.WriteLine("----Action05_01"), 1, 1, 2f);
    }

    IEnumerator IE05_02()
    {
        yield return Do.Run(IE05_03());
        yield return Do.Run(() => Console.WriteLine("----Action05_02"), 1, 1, 2f);
    }

    IEnumerator IE05_03()
    {
        yield return Do.Wait(1f);
        yield return Do.Run(() => Console.WriteLine("----Action05_03"), 1, 1, 2f);
    }

    IEnumerator IE06()
    {
        Console.WriteLine(DoSomething(10));
        yield return Do.Wait(1f);
    }

    IEnumerator IE07()
    {
        Console.WriteLine(DoSomething(10));
        yield return Do.Wait(1f);
    }

    IEnumerator IE08()
    {
        var doo = Do.Run(() => Console.WriteLine("----Action08"), 1, 1, 10);
        Do.Run(() => Console.WriteLine("----Action09"), 1, 1, 10);
        yield return Do.Wait(2f);
        doo.Over();
        yield return Do.Wait(4f);
        DoManager.Instance.OverAll();
    }

    string DoSomething(int length)
    {
        var result = DoCalAsync(length).Result;//调用了Result，运行到这里会等待
        return $"[DoSomething] {result}";
    }

    async Task<int> DoCalAsync(int length)
    {
        double s = 0;
        await Task.Run(() =>
        {
            for (double i = 0; i < length * 100000000; i++)
                s += Math.Sqrt(Math.Sqrt(Math.Sqrt(i))) * 0.0000001f;
        });
        return (int)s;
    }
}


