/// test.m
#import <Foundation/Foundation.h>

void outputAppendString (char *str1, char *str2)
{
	NSString *string1 = [[NSString alloc] initWithUTF8String:str1];
	NSString *string2 = [[NSString alloc] initWithUTF8String:str2];
	
	NSString *resultStr = [NSString stringWithFormat:@"%@ %@", string1, string2];
	NSLog(@"###%@", resultStr);
	
	UnitySendMessage("Game", "callback", resultStr.UTF8String);
}

typedef void (*ResultHandler) (const char *object);

void outputAppendString2 (char *str1, char *str2, ResultHandler resultHandler)
{
    NSString *string1 = [[NSString alloc] initWithUTF8String:str1];
    NSString *string2 = [[NSString alloc] initWithUTF8String:str2];
    
    NSString *resultStr = [NSString stringWithFormat:@"%@ %@", string1, string2];
    NSLog(@"###%@", resultStr);
    
    resultHandler (resultStr.UTF8String);
}