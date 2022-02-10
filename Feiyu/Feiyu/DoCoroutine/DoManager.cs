using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feiyu.DoCoroutine
{
    internal class ConStants
    {
        public const float Default = -3.4578987654f;    //默认数值
    }

    public class DoManager
    {
        internal float deltaTime = 0.025f;          //协程管理器更新间隔时间
        internal float minFloatError = 0.00025f;    //协程浮点数误差
        internal List<Do> doos = new List<Do>();    //Do协程集合
        //协程管理器单例
        private static DoManager instance;
        public static DoManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DoManager();
                return instance;
            }
        }
        private DoManager() { }

        //更新所有Do协程
        public void Update()
        {
            for (int i = 0; i < doos.Count;i++)
                doos[i].Update();
        }
        //设置协程管理器更新间隔时间
        public void SetDeltaTime(float deltaTime)
        {
            this.deltaTime = deltaTime;
            minFloatError = deltaTime / 100f;
        }
        //返回协程管理器更新间隔时间
        public float GetDeltaTime()
        {
            return deltaTime;
        }
        //结束所有Do协程
        public void OverAll()
        {
            doos.Clear();
        }
        //返回当前Do协程数量
        public int GetDoosCount()
        {
            return doos.Count;
        }
        //返回当前所有Do协程是否全部完成，即Do协程数量为0
        public bool IsAllFinished()
        {
            return doos.Count==0?true:false;
        }
    }
}
