# ThinkingData Strategy SDK for Unity

This is the [ThinkingData](https://www.thinkingdata.cn)™ Strategy SDK for Unity.

## Install via Package Manager

Requires **TDRemoteConfig 1.2.4** (`com.thinkingdata.remoteconfig`). Unity does not support Git dependencies inside a package's `package.json`, so add both packages in the host project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.thinkingdata.remoteconfig": "https://github.com/ThinkingDataAnalytics/tdremoteconfig-unity.git?path=/Assets#v1.2.4",
    "com.thinkingdata.strategy": "https://github.com/ThinkingDataAnalytics/tdstrategy-unity.git?path=/Assets#v1.3.4"
  }
}
```

Or add each from Package Manager → Add package from git URL.

> Note: 本仓库 `Assets/TDRemoteConfig/` 为本地开发 stub，不会打进 `ta_strategy_sdk.unitypackage`。通过 Git UPM 安装时，请使用真实的 TDRemoteConfig 包，并确保宿主工程中不保留该 stub。

## Quick Start

```csharp
using ThinkingData.Strategy;

TDStrategy.Init("YOUR_APP_ID", "https://YOUR_SERVER_URL");
TDStrategy.SetTriggerListener(listener);
TDStrategy.Fetch();
```

API entry: `ThinkingData.Strategy.TDStrategy`.

---
