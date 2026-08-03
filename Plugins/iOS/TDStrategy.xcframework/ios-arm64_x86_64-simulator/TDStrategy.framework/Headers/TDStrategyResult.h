//
//  TDStrategyResult.h
//  TDStrategy
//
//  Created by 杨雄 on 2024/3/18.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface TDStrategyResult : NSObject
@property (nonatomic, copy) NSString *channelMsgType;
@property (nonatomic, copy) NSString *appId;
@property (nonatomic, copy) NSString *pushId;
@property (nonatomic, copy) NSString *taskId;
@property (nonatomic, strong, nullable) NSDictionary *content;
@property (nonatomic, strong, nullable) NSDictionary *userParams;
/// { "#ops_receipt_properties" : { ... } }
@property (nonatomic, strong, nonnull) NSDictionary *opsProperties;

@end

NS_ASSUME_NONNULL_END
