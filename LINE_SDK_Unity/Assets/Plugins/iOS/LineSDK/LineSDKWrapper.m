//
//  LineSDKWrapper.m
//
//  Copyright (c) 2019-present, LY Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LY Corporation.
//
//  As with any software that integrates with the LY Corporation platform, your use of this software
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

#import "LineSDKWrapper.h"
#import "LineSDKAppDelegateListener.h"
#import "LineSDKNativeCallbackPayload.h"
#import <LineSDKObjC/LineSDKObjC-Swift.h>

@interface LineSDKWrapper()

@property (nonatomic, assign) BOOL setup;
@property (nonatomic, strong) LineSDKAppDelegateListener *appDelegateListener;

@end

@implementation LineSDKWrapper

+ (instancetype)sharedInstance
{
    static LineSDKWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[LineSDKWrapper alloc] init];
    });
    return sharedInstance;
}

- (void)setupChannelID:(NSString *)channelID universalLink:(NSString *)universalLink {
    if (self.setup) { return; }
    self.setup = YES;

    NSURL *link = nil;
    if (universalLink.length != 0) {
        link = [NSURL URLWithString:universalLink];
    }

    [[LineSDKLoginManager sharedManager] setupWithChannelID:channelID universalLinkURL:link];
    self.appDelegateListener = [[LineSDKAppDelegateListener alloc] init];
}

- (void)loginWithIdentifier:(NSString *)identifier
                     scopes:(NSString *)scopes
               onlyWebLogin:(BOOL)onlyWebLogin
                  botPrompt:(NSString *)botPrompt
               IDTokenNonce:(NSString *)nonce
{
    NSSet* permissions = [LineSDKLoginPermission permissionsFrom:scopes];

    LineSDKLoginManagerParameters *parameters = [[LineSDKLoginManagerParameters alloc] init];
    if (onlyWebLogin) {
        parameters.onlyWebLogin = YES;
    }

    if ([botPrompt isEqualToString:@"normal"]) {
        parameters.botPromptStyle = LineSDKLoginManagerBotPrompt.normal;
    } else if ([botPrompt isEqualToString:@"aggressive"]) {
        parameters.botPromptStyle = LineSDKLoginManagerBotPrompt.aggressive;
    }

    if (nonce) {
        parameters.IDTokenNonce = nonce;
    }

    [[LineSDKLoginManager sharedManager]
     loginWithPermissions:permissions
     inViewController:nil
     parameters:parameters
     completionHandler:^(LineSDKLoginResult * result, NSError *error)
     {
        if (error) {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        } else {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)loginWithIdentifier:(NSString *)identifier
                     scopes:(NSString *)scopes
               onlyWebLogin:(BOOL)onlyWebLogin
                  botPrompt:(NSString *)botPrompt
{
    [self loginWithIdentifier:identifier
                       scopes:scopes
                 onlyWebLogin:onlyWebLogin
                    botPrompt:botPrompt
                 IDTokenNonce:nil
    ];
}

- (void)logoutWithIdentifier:(NSString *)identifier {
    [[LineSDKLoginManager sharedManager] logoutWithCompletionHandler:^(NSError *error) {
        if (error) {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        } else {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:@""];
            [payload sendMessageOK];
        }
    }];
}

- (void)refreshAccessTokenWithIdentifier:(NSString *)identifier {
    [LineSDKAuthAPI refreshAccessTokenWithCompletionHandler:^(LineSDKAccessToken * token, NSError * error) {
        if (error) {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        } else {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[token json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)revokeAccessTokenWithIdentifier:(NSString *)identifier {
    [LineSDKAuthAPI revokeAccessTokenWithCompletionHandler:^(NSError * error) {
        if (error) {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        } else {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:@""];
            [payload sendMessageOK];
        }
    }];
}

- (void)verifyAccessTokenWithIdentifier:(NSString *)identifier {
    [LineSDKAuthAPI verifyAccessTokenWithCompletionHandler:^(LineSDKAccessTokenVerifyResult *result, NSError *error) {
        if (error) {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        } else {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)getProfileWithIdentifier:(NSString *)identifier {
    [LineSDKAPI getProfileWithCompletionHandler:^(LineSDKUserProfile *profile, NSError *error) {
        if (error) {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        } else {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[profile json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)getBotFriendshipStatusWithIdentifier:(NSString *)identifier {
    [LineSDKAPI getBotFriendshipStatusWithCompletionHandler:^(LineSDKGetBotFriendshipStatusResponse *response, NSError *error) {
        if (error) {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        } else {
            LineSDKNativeCallbackPayload *payload =
            [LineSDKNativeCallbackPayload payloadWithIdentifier:identifier value:[response json]];
            [payload sendMessageOK];
        }
    }];
}

- (NSString *)currentAccessToken {
    return [[[LineSDKAccessTokenStore sharedStore] currentToken] json];
}

- (NSString *)wrapError:(NSError *)error {
    NSDictionary *dic = @{@"code": @(error.code), @"message": error.localizedDescription};
    NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:kNilOptions error:nil];
    if (!data) { return nil; }
    return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

@end
