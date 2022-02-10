using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feiyu.DoCoroutine
{
    public class Do
    {
        IEnumerator _enumerator;         //Do协程内部迭代器
        internal Action _action;         //Do事件
        Action _onCompletedAction;       //Do事件完成后调用的事件
        Func<bool> _stopCondition;       //Do事件停止条件
        internal float _delayTime;       //Do延迟开始时间
        internal float _interTime = ConStants.Default;          //Do事件间隔调用时间
        internal float _liveTime = ConStants.Default;           //Do事件运行时间，存活时间

        //根据迭代器生成一个Do协程
        public Do(IEnumerator enumerator)
        {
            _enumerator = enumerator;
        }
        //根据参数生成一个Do协程
        public Do(Action action, float delayTime, float interTime = ConStants.Default, float liveTime = ConStants.Default)
        {
            _action = action;
            _delayTime = delayTime;
            _interTime = interTime;
            _liveTime = liveTime;
            _enumerator = GetEnumerator();
        }
        //设置Do协程停止条件
        public Do StopIf(Func<bool> stopCondition)
        {
            _stopCondition = stopCondition;
            return this;
        }
        //设置Do协程完成后的调用事件
        public Do OnCompleted(Action onCompletedAction)
        {
            _onCompletedAction = onCompletedAction;
            return this;
        }
        //立即结束协程
        public void Over()
        {
            DoManager.Instance.doos.Remove(this);
        }
        //协程是否完成
        public bool IsFinished()
        {
            return !DoManager.Instance.doos.Contains(this);
        }
        //根据Do事件等参数生成对应的迭代器
        private IEnumerator GetEnumerator()
        {
            return new DoEnumertor(this);
        }
        //更新协程
        internal bool Update()
        {
            //检查停止条件
            if (_stopCondition != null && _stopCondition() == true)
            {
                Over();
                return false;
            }
            bool isHasNext = true;
            //Iwait,当前对象为Iwait
            if (_enumerator.Current is IWait)
            {
                var IWait = (IWait)_enumerator.Current;
                if (!IWait.Tick())
                    isHasNext = _enumerator.MoveNext();
            }
            //Do,当前对象为Do协程
            else if (_enumerator.Current is Do)
            {
                if (!((Do)(_enumerator.Current)).Update())
                    isHasNext = _enumerator.MoveNext();
            }
            //当前对象是DoEnumerator或者Null(第一次调用迭代器也是Null)
            else
                isHasNext = _enumerator.MoveNext();
            //结束当前协程，执行协程结束事件
            if (!isHasNext)
            {
                Over();
                if (_onCompletedAction != null)
                    _onCompletedAction();
            }
            //在MoveNext之后必须进行检测！
            //如果当前对象是Do协程，则当前对象是属于一个Do协程的子Do协程,无需再进行单独调用
            if (_enumerator.Current is Do)
            {
                var dooChild = (Do)(_enumerator.Current);
                dooChild.Over();
            }
            return isHasNext;
        }

        /*
         * 公用方法
         */
        //用于在协程中等待一定时间
        public static WaitSceonds Wait(float seconds)
        {
            return new WaitSceonds(seconds);
        }
        //运行一个Do协程
        public static void Run(Do doo)
        {
            DoManager.Instance.doos.Add(doo);
        }
        //根据迭代器生成一个Do协程然后运行
        public static Do Run(IEnumerator enumerator)
        {
            var doo = new Do(enumerator);
            DoManager.Instance.doos.Add(doo);
            return doo;
        }
        //根据参数生成一个Do协程然后运行
        public static Do Run(Action action, float delayTime, float interTime = ConStants.Default, float runTime = ConStants.Default)
        {
            Do doo = new Do(action, delayTime, interTime, runTime);
            DoManager.Instance.doos.Add(doo);
            return doo;
        }
    }
}
