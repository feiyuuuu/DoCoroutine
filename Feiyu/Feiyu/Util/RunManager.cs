using System;
using System.Collections.Generic;
using System.Text;

namespace Feiyu.Util
{
    public class RunManager : Singleton<RunManager>
    {
        public List<IRun> RunList = new List<IRun>();
        public static void Add(IRun iRun)
        {
            Instance.RunList.Add(iRun);
        }

        public static void Run(int index)
        {
            Instance.RunList[index].Run();
        }
    }
}
