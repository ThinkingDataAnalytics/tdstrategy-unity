#if ((!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)) || TE_DISABLE_ANDROID_JAVA || TE_DISABLE_IOS_OC
using System;
using System.Collections.Generic;

namespace ThinkingData.Strategy.Wrapper
{
    public partial class TDWrapper
    {
        public static void EnableLog(bool enable)
        {

        }

        public static void Init(TDStrategySettings settings) {

        }

        public static void SetTriggerListener(TDStrategyResultListener listener)
        {

        }


        public static void AddClientParams(Dictionary<string, object> p)
        {

        }

        public static void RemoveClientParam(string key)
        {

        }
    }

}
#endif

