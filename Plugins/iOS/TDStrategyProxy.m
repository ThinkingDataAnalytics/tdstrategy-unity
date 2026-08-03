#if __has_include(<TDStrategy/TDStrategy.h>)
#import <TDStrategy/TDStrategy.h>
#else
#import "TDStrategy.h"
#endif

typedef void (*StrategyResultHandler) (const char *type, const char *jsonData);
static StrategyResultHandler resultHandler;
void RegisterRecieveStrategyCallback(StrategyResultHandler handlerPointer)
{
    resultHandler = handlerPointer;
}

char* st_strdup(const char* string) {
    if (string == NULL)
        return NULL;
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

void st_convertToDictionary(const char *json, NSDictionary **properties_dict) {
    NSString *json_string = json != NULL ? [NSString stringWithUTF8String:json] : nil;
    if (json_string) {
        *properties_dict = [NSJSONSerialization JSONObjectWithData:[json_string dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:nil];
    }
}


void strategyResultCallback(TDStrategyResult *result) {
    NSMutableDictionary *dict = [[NSMutableDictionary alloc]init];
    dict[@"appId"]= result.appId;
    dict[@"taskId"] = result.taskId;
    dict[@"channelMsgType"] = result.channelMsgType;
    dict[@"pushId"] = result.pushId;
    dict[@"content"] = result.content;
    dict[@"userParams"] = result.userParams;
    dict[@"opsProperties"] = result.opsProperties;
    NSData *data = [NSJSONSerialization dataWithJSONObject:dict options:kNilOptions error:nil];
    NSString *jsonString = [[NSString alloc]initWithData:data encoding:NSUTF8StringEncoding];
    resultHandler("StrategyResultCallback",st_strdup([jsonString UTF8String]));
}

@interface StrategyDelegateObject : NSObject<TDStrategyTriggerDelegate>

@end

@implementation StrategyDelegateObject

- (void)tdStrategyDidTrigger:(TDStrategyResult *)result {
    strategyResultCallback(result);
}

@end

void st_enableLog(BOOL enableLog) {
    [TDStrategy enableLog:enableLog];
}

void st_init(const char *appId,const char *serverUrl,int mode){
    TDStrategySettings *settings = [[TDStrategySettings alloc]init];
    NSString *app_id_string = appId != NULL ? [NSString stringWithUTF8String:appId] : nil;
    settings.appId = app_id_string;
    NSString *server_url = serverUrl != NULL ? [NSString stringWithUTF8String:serverUrl] : nil;
    settings.serverUrl = server_url;
    if(mode == 1){
        settings.mode = TDStrategyModeDebug;
    }
    [TDStrategy startWithSettings:settings];
}

void st_setTriggerDelegate(){
    [TDStrategy setTriggerDelegate:[[StrategyDelegateObject alloc]init]];
}

void st_add_client_params(const char *defaultValues){
    NSDictionary *default_values = nil;
    st_convertToDictionary(defaultValues, &default_values);
    [TDStrategy addClientParams:default_values];
}

void st_remove_client_param(const char *key){
    NSString *key_string = key != NULL ? [NSString stringWithUTF8String:key] : nil;
    [TDStrategy removeClientParam:key_string];
}