# ThinkingData Strategy SDK for Unity

This is the [ThinkingData](https://www.thinkingdata.cn)™ Strategy SDK for Unity.

## Install via Package Manager

Depends on **TDRemoteConfig 1.2.2** (`com.thinkingdata.remoteconfig`).

Add from git URL (Unity 2019.3+):

```
https://github.com/ThinkingDataAnalytics/tdstrategy-unity.git?path=/Assets#v1.3.2
```

Or add to your project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.thinkingdata.strategy": "https://github.com/ThinkingDataAnalytics/tdstrategy-unity.git?path=/Assets#v1.3.2"
  }
}
```

> Note: Unity Package Manager 对「包依赖另一个 Git 包」支持有限。若未自动拉取 TDRemoteConfig，请在宿主工程中手动添加：
> `https://github.com/ThinkingDataAnalytics/tdremoteconfig-unity.git?path=/Assets#v1.2.2`
>
> 本仓库 `Assets/TDRemoteConfig/` 为本地开发 stub，不会打进 `ta_strategy_sdk.unitypackage`。通过 Git UPM 安装时，请使用真实的 TDRemoteConfig 包，并确保宿主工程中不保留该 stub。

## Quick Start

```csharp
using ThinkingData.Strategy;

TDStrategy.Init("YOUR_APP_ID", "https://YOUR_SERVER_URL");
TDStrategy.SetTriggerListener(listener);
TDStrategy.Fetch();
```

API entry: `ThinkingData.Strategy.TDStrategy`.

---
