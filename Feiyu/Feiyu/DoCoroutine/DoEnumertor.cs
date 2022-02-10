using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Feiyu.DoCoroutine
{
    internal class DoEnumertor : IEnumerator
    {
        Action _action;
        float _delayTime;
        float _interTime;
        float _liveTime;

        internal DoEnumertor(Do doo)
        {
            _action = doo._action;
            _delayTime = doo._delayTime;
            _interTime = doo._interTime;
            _liveTime = doo._liveTime;
        }

        public object Current { get { return this; } }
        
        public bool MoveNext()
        {
            _delayTime -= DoManager.Instance.deltaTime;
            //默认不循环调用，间隔运行时间为默认数值
            if (_interTime ==ConStants.Default)
            {
                if(_delayTime<= DoManager.Instance.minFloatError)
                {
                    _action();
                    return false;
                }
                return true;
            }
            //循环调用
            else
            {
                if (_delayTime<= DoManager.Instance.minFloatError)
                {
                    //【重要】这里更应该写delayTIme+=interTime,而不是delayTime=interTime
                    _delayTime +=_interTime;
                    _action();
                }
                _liveTime -= DoManager.Instance.deltaTime;
                if (_liveTime <= 0)
                    return false;
                return true;
            }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
