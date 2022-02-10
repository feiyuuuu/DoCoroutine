using System;

namespace Feiyu.DoCoroutine
{
    public class WaitSceonds:IWait
    {
        float seconds;
        public WaitSceonds(float seconds)
        {
            this.seconds = seconds;
        }
        
        internal override bool Tick()
        {
            seconds -= DoManager.Instance.deltaTime;
            if (seconds<= DoManager.Instance.minFloatError)
                return false;
            return true;
        }
    }
}
