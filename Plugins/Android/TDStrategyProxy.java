package cn.thinkingdata.strategy;

import android.content.Context;

import org.json.JSONException;
import org.json.JSONObject;

import cn.thinkingdata.strategy.core.result.TDStrategyResult;
import cn.thinkingdata.strategy.core.result.TDStrategyResultListener;

public class TDStrategyProxy {

    public static void enableLog(boolean enable) {
        TDStrategy.enableLog(enable);
    }

    public static void init(Context context, String appId, String serverUrl, int mode) {
        TDStrategySettings settings = new TDStrategySettings(appId, serverUrl);
        if (mode == 1) {
            settings.mode = TDStrategy.TDStrategyMode.DEBUG;
        }
        TDStrategy.init(context, settings);
    }

    public static void setTriggerListener(StrategyResultListener listener) {
        TDStrategy.setTriggerListener(new TDStrategyResultListener() {
            @Override
            public void onTriggerResult(TDStrategyResult tdStrategyResult) {
                if (null != listener) {
                    JSONObject json = new JSONObject();
                    try {
                        json.put("appId", tdStrategyResult.appId);
                        json.put("taskId", tdStrategyResult.taskId);
                        json.put("channelMsgType", tdStrategyResult.channelMsgType);
                        json.put("pushId", tdStrategyResult.pushId);
                        json.put("content", tdStrategyResult.content);
                        json.put("userParams", tdStrategyResult.userParams);
                        json.put("opsProperties", tdStrategyResult.opsProperties);
                        listener.onTriggerResult(json.toString());
                    } catch (JSONException e) {
                        listener.onTriggerResult("{}");
                    }
                }
            }
        });
    }

    public static void addClientParams(String params){
        try {
            TDStrategy.addClientParams(new JSONObject(params));
        }catch (Exception ignore){
        }
    }

    public static void removeClientParam(String key){
        try {
            TDStrategy.removeClientParam(key);
        }catch (Exception ignore){

        }
    }

    public interface StrategyResultListener {
        void onTriggerResult(String result);
    }

}