#if UNITY_ANDROID && !(UNITY_EDITOR) && !TE_DISABLE_ANDROID_JAVA
using System;
using System.Collections.Generic;
using UnityEngine;
using ThinkingData.Strategy.Utils;

namespace ThinkingData.Strategy.Wrapper
{
    public partial class TDWrapper
    {
        private static readonly AndroidJavaClass sdkClass = new AndroidJavaClass("cn.thinkingdata.strategy.TDStrategyProxy");

        private static TDStrategyResultListener mListener;

        public static void EnableLog(bool enable)
        {
            sdkClass.CallStatic("enableLog", enable);
        }

        public static void Init(TDStrategySettings settings)
        {
            if (settings == null) return;
            AndroidJavaObject context = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            sdkClass.CallStatic("init",context,settings.appId,settings.serverUrl, (int)settings.mode);
        }

        public static void SetTriggerListener(TDStrategyResultListener listener)
        {
            mListener = listener;
            TriggerListenerAdapter adapter = new TriggerListenerAdapter();
            sdkClass.CallStatic("setTriggerListener", adapter);
        }

        public static void AddClientParams(Dictionary<string, object> p)
        {
            try
            {
                sdkClass.CallStatic("addClientParams", TDMiniJson.Serialize(p));
            }
            catch (Exception) {
            }
        }

        public static void RemoveClientParam(string key)
        {
            sdkClass.CallStatic("removeClientParam", key);
        }

        private class TriggerListenerAdapter : AndroidJavaProxy
        {
            public TriggerListenerAdapter() : base("cn.thinkingdata.strategy.TDStrategyProxy$StrategyResultListener") { }
            public void onTriggerResult(string result)
            {
                if(mListener != null)
                {
                    TDStrategyResult tdStrategyResult = new TDStrategyResult();
                    try
                    {
                        Dictionary<string, object> dict = TDMiniJson.Deserialize(result);
                        tdStrategyResult.appId = (string)dict["appId"];
                        tdStrategyResult.taskId = (string)dict["taskId"];
                        tdStrategyResult.channelMsgType = (string)dict["channelMsgType"];
                        tdStrategyResult.pushId = (string)dict["pushId"];
                        tdStrategyResult.content = (Dictionary<string, object>)dict["content"];
                        tdStrategyResult.userParams = (Dictionary<string, object>)dict["userParams"];
                        tdStrategyResult.opsProperties = (Dictionary<string, object>)dict["opsProperties"];
                    }
                    catch (Exception)
                    {
                        if (tdStrategyResult.appId == null)
                        {
                            tdStrategyResult.appId = "";
                        }
                        if (tdStrategyResult.taskId == null)
                        {
                            tdStrategyResult.taskId = "";
                        }
                        if (tdStrategyResult.channelMsgType == null)
                        {
                            tdStrategyResult.channelMsgType = "";
                        }
                        if (tdStrategyResult.pushId == null)
                        {
                            tdStrategyResult.pushId = "";
                        }
                        if (tdStrategyResult.content == null)
                        {
                            tdStrategyResult.content = new Dictionary<string, object>();
                        }
                        if (tdStrategyResult.userParams == null)
                        {
                            tdStrategyResult.userParams = new Dictionary<string, object>();
                        }
                        if (tdStrategyResult.opsProperties == null)
                        {
                            tdStrategyResult.opsProperties = new Dictionary<string, object>();
                        }
                    }
                    mListener.OnTriggerResult(tdStrategyResult);
                }
            }
        }
    }

}
#endif