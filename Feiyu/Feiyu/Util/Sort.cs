using System;
using System.Collections.Generic;
using System.Text;

namespace Feiyu.Util
{
    public class Sort
    {
        //洗牌算法
        public static void Shuffle<T>(List<T> list)
        {
            Random rd = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int randomNum = rd.Next(0, list.Count);
                //Swap
                T temp = list[randomNum];
                list[randomNum] = list[i];
                list[i] = temp;
            }
        }

        public static string GetPrintInfo<T>(List<T> list,Func<T,string> func)
        {
            var info = "";
            for (int i = 0; i < list.Count; i++)
            {
               info+=func(list[i]);
            }
            return info;
        }
    }
}
