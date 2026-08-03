using System;
using System.Collections.Generic;
using ThinkingData.Strategy.Wrapper;
using ThinkingData.RemoteConfig;
namespace ThinkingData.Strategy {
    public class TDStrategy {

        public static void EnableLog(bool enable)
        {
            TDWrapper.EnableLog(enable);
        }

        public static void Init(string appId, string serverUrl) {
            TDStrategySettings settings = new TDStrategySettings();
            settings.appId = appId;
            settings.serverUrl = serverUrl;
            TDWrapper.Init(settings);
        }

        public static void Init(TDStrategySettings settings) {
            TDWrapper.Init(settings);
        }

        public static void SetTriggerListener(TDStrategyResultListener listener) {
            TDWrapper.SetTriggerListener(listener);
        }

        public static void AddClientParams(Dictionary<string,object> p) {
            if (p == null) return;
            TDWrapper.AddClientParams(p);
        }

        public static void RemoveClientParam(string key) {
            if (key == null) return;
            TDWrapper.RemoveClientParam(key);
        }

        public static void Fetch() {
            TDRemoteConfig.Fetch("", "td_strategy");
        }

        public static string GetSDKVersion() {
            return "1.3.2";
        }

    }

    public class TDStrategySettings {
        public string appId;
        public string serverUrl;
        public TDStrategyMode mode;
    }

    public enum TDStrategyMode {
        NORMAL = 0,
        DEBUG = 1
    }

    public class TDStrategyResult {
        public string appId;
        public string taskId;
        public string channelMsgType;
        public string pushId;
        public Dictionary<string, object> content;
        public Dictionary<string, object> userParams;
        public Dictionary<string, object> opsProperties;
    }

    public interface TDStrategyResultListener {
        public void OnTriggerResult(TDStrategyResult result);
    }
}


