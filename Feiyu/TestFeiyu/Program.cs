using Feiyu.DoCoroutine;
using Feiyu.Util;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestFeiyu
{
    class Program
    {
        static void Main(string[] args)
        {
            RunManager.Add(new TestDo());
            RunManager.Run(0);
            Console.ReadKey();
        }
    }
}


