#if UNITY_IOS && !(UNITY_EDITOR) && !TE_DISABLE_IOS_OC
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ThinkingData.Strategy.Utils;

namespace ThinkingData.Strategy.Wrapper
{
    public partial class TDWrapper
    {
        [DllImport("__Internal")]
        private static extern void st_enableLog(bool enable);

        [DllImport("__Internal")]
        private static extern void st_init(string appId, string serverUrl, int mode);

        [DllImport("__Internal")]
        private static extern void st_add_client_params(string p);

        [DllImport("__Internal")]
        private static extern void st_remove_client_param(string key);

        [DllImport("__Internal")]
        private static extern void st_setTriggerDelegate();

        [DllImport("__Internal")]
        public static extern void RegisterRecieveStrategyCallback
        (
            IntPtr handlerPointer
        );

        private static TDStrategyResultListener mListener;

        public static void EnableLog(bool enable)
        {
            st_enableLog(enable);
        }

        public static void Init(TDStrategySettings settings)
        {
            if (settings == null) return;
            RegisterStrategyCallback();
            st_init(settings.appId, settings.serverUrl, (int)settings.mode);
        }

        public static void SetTriggerListener(TDStrategyResultListener listener)
        {
            mListener = listener;
            st_setTriggerDelegate();
        }

        public static void AddClientParams(Dictionary<string, object> p)
        {
            try
            {
                st_add_client_params(TDMiniJson.Serialize(p));
            }
            catch (Exception)
            {
            }
        }

        public static void RemoveClientParam(string key)
        {
            st_remove_client_param(key);
        }

        private static void RegisterStrategyCallback()
        {
            StrategyResultHandler handler = new StrategyResultHandler(strategyResultHandler);
            IntPtr handlerPointer = Marshal.GetFunctionPointerForDelegate(handler);
            RegisterRecieveStrategyCallback(handlerPointer);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void StrategyResultHandler(string type, string jsonData);

        [AOT.MonoPInvokeCallback(typeof(StrategyResultHandler))]
        static void strategyResultHandler(string type, string jsonData)
        {
            if (type == "StrategyResultCallback")
            {
                if (mListener != null) {
                    TDStrategyResult tdStrategyResult = new TDStrategyResult();
                    try
                    {
                        Dictionary<string, object> dict = TDMiniJson.Deserialize(jsonData);
                        tdStrategyResult.appId = (string)dict["appId"];
                        tdStrategyResult.taskId = (string)dict["taskId"];
                        tdStrategyResult.channelMsgType = (string)dict["channelMsgType"];
                        if (dict.ContainsKey("pushId")) {
                            tdStrategyResult.pushId = (string)dict["pushId"];
                        }
                        else
                        {
                            tdStrategyResult.pushId = "";
                        }
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