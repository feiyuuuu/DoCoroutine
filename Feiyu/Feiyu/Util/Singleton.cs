using System;
using System.Collections.Generic;
using System.Text;

namespace Feiyu.Util
{
    public  class Singleton<T> where T:class,new ()
    {
        protected static  T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new T();
                return instance;
            }
        }
    }
}
