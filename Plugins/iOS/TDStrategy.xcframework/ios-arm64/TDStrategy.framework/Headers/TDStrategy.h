//
//  TDStrategy.h
//  TDStrategy
//
//  Created by 杨雄 on 2024/2/27.
//

#import <Foundation/Foundation.h>

#if __has_include(<TDStrategy/TDStrategyTriggerDelegate.h>)
#import <TDStrategy/TDStrategyTriggerDelegate.h>
#else
#import "TDStrategyTriggerDelegate.h"
#endif

#if __has_include(<TDStrategy/TDStrategySettings.h>)
#import <TDStrategy/TDStrategySettings.h>
#else
#import "TDStrategySettings.h"
#endif

#if __has_include(<TDStrategy/TDStrategyResult.h>)
#import <TDStrategy/TDStrategyResult.h>
#else
#import "TDStrategyResult.h"
#endif

NS_ASSUME_NONNULL_BEGIN

@interface TDStrategy : NSObject

/// enable SDK log
+ (void)enableLog:(BOOL)enable;

/// init SDK with settings
+ (void)startWithSettings:(TDStrategySettings *)settings;

/// init SDK with appid and server url
/// @param appId appId
/// @param url server url
+ (void)startWithAppId:(NSString *)appId serverUrl:(NSString *)url;

/// Sets the delegate of the trigger to receive callbacks for triggered events when they are triggered
+ (void)setTriggerDelegate:(id<TDStrategyTriggerDelegate>)delegate;

/// SDK version string
+ (NSString *)getSDKVersion;

/// update tasks from remote
+ (void)fetch;

// MARK: - Deprecated

/// add client params
+ (void)addClientParams:(NSDictionary *)params DEPRECATED_MSG_ATTRIBUTE("Deprecated. replace with: [TDRemoteConfig addClientParams:]");

/// remove client param for key
+ (void)removeClientParam:(NSString *)key DEPRECATED_MSG_ATTRIBUTE("Deprecated. replace with: [TDRemoteConfig removeClientParam:]");

/// SDK version string
+ (NSString *)version DEPRECATED_MSG_ATTRIBUTE("Deprecated. replace with: [TDStrategy getSDKVersion]");

// MARK: -

@end

NS_ASSUME_NONNULL_END
