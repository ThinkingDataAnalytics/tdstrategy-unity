//
//  TDStrategyTriggerDelegate.h
//  TDStrategy
//
//  Created by 杨雄 on 2024/3/4.
//

#ifndef TDStrategyTriggerDelegate_h
#define TDStrategyTriggerDelegate_h

#import <Foundation/Foundation.h>
@class TDStrategyResult;

@protocol TDStrategyTriggerDelegate <NSObject>

@optional

- (void)tdStrategyDidTrigger:(TDStrategyResult *)result;

@end


#endif /* TDStrategyTriggerDelegate_h */
