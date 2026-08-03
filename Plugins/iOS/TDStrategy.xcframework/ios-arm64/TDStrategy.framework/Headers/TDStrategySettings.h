//
//  TDStrategySettings.h
//  TDStrategy
//
//  Created by 杨雄 on 2024/5/30.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef NS_OPTIONS(NSUInteger, TDStrategyMode) {
    TDStrategyModeNormal = 0,
    TDStrategyModeDebug = 1,
};

@interface TDStrategySettings : NSObject
@property (nonatomic, assign) TDStrategyMode mode;
@property (nonatomic, copy) NSString *appId;
@property (nonatomic, copy) NSString *serverUrl;

@end

NS_ASSUME_NONNULL_END
