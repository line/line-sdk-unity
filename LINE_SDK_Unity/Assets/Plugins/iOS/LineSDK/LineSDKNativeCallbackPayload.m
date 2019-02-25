//
//  LineSDKNativeCallbackPayload.m
//
//  Copyright (c) 2019-present, LINE Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LINE Corporation.
//
//  As with any software that integrates with the LINE Corporation platform, your use of this software
//  is subject to the LINE Developers Agreement [http://terms2.line.me/LINE_Developers_Agreement].
//  This copyright notice shall be included in all copies or substantial portions of the software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#import "LineSDKNativeCallbackPayload.h"
#import "LineSDKNativeInterface.h"

@interface LineSDKNativeCallbackPayload()

@property (nonatomic, copy) NSString *identifier;
@property (nonatomic, copy) NSString *value;

@end

@implementation LineSDKNativeCallbackPayload

+ (instancetype)payloadWithIdentifier:(NSString *)identifier value:(NSString *)value {
    return [[self alloc] initWithIdentifier:identifier value:value];
}

- (instancetype)initWithIdentifier:(NSString *)identifier value:(NSString *)value {
    self = [super init];
    if (self) {
        _identifier = identifier;
        _value = value;
    }
    return self;
}

- (NSString *)payloadJSON {
    if (!self.identifier || !self.value) {
        NSLog(@"[LINESDK] Either `identifier` and `value` is nil. Check response value to make sure a valid result.");
        return nil;
    }

    NSDictionary *dic = @{@"identifier": self.identifier, @"value": self.value};
    NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:kNilOptions error:nil];
    if (!data) { return nil; }

    return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

- (void)sendMessageOK {
    line_sdk_UnitySendMessage("LineSDK", "OnApiOk", [self payloadJSON]);
}

- (void)sendMessageError {
    line_sdk_UnitySendMessage("LineSDK", "OnApiError", [self payloadJSON]);
}

@end
