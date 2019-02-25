//
//  LineSDKNativeInterface.mm
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

#import <Foundation/Foundation.h>
#import "LineSDKWrapper.h"

#define LINE_SDK_EXTERNC extern "C"

// MARK: - Helpers

// Helper method to create NSString copy from C String
NSString* LineSDKMakeNSString (const char* string) {
    if (string) {
        return [NSString stringWithUTF8String: string];
    } else {
        return [NSString stringWithUTF8String: ""];
    }
}

// Helper method to create C String copy from NSString
char* LineSDKMakeCString(NSString *str) {
    const char* string = [str UTF8String];
    if (string == NULL) {
        return NULL;
    }

    char *buffer = (char*)malloc(strlen(string) + 1);
    strcpy(buffer, string);
    return buffer;
}

LINE_SDK_EXTERNC void line_sdk_UnitySendMessage(const char *name, const char *method, NSString *params) {
    UnitySendMessage(name, method, LineSDKMakeCString(params));
}

// MARK: - Extern APIs

LINE_SDK_EXTERNC void line_sdk_setup(const char* channelId, const char* universalLinkURL);
void line_sdk_setup(const char* channelId, const char* universalLinkURL) {
    NSString *nsChannelID = LineSDKMakeNSString(channelId);
    NSString *nsUniversalLinkURL = LineSDKMakeNSString(universalLinkURL);

    [[LineSDKWrapper sharedInstance] setupChannelID:nsChannelID universalLink:nsUniversalLinkURL];
}

LINE_SDK_EXTERNC void line_sdk_login(const char* scope, bool onlyWebLogin, const char* botPrompt, const char* identifier);
void line_sdk_login(const char* scope, bool onlyWebLogin, const char* botPrompt, const char* identifier) {
    NSString *nsScope = LineSDKMakeNSString(scope);
    NSString *nsBotPrompt = LineSDKMakeNSString(botPrompt);
    NSString *nsIdentifier = LineSDKMakeNSString(identifier);

    [[LineSDKWrapper sharedInstance] loginWithIdentifier:nsIdentifier scopes:nsScope onlyWebLogin:onlyWebLogin botPrompt:nsBotPrompt];
}

LINE_SDK_EXTERNC void line_sdk_logout(const char* identifier) {
    NSString *nsIdentifier = LineSDKMakeNSString(identifier);
    [[LineSDKWrapper sharedInstance] logoutWithIdentifier:nsIdentifier];
}

LINE_SDK_EXTERNC void line_sdk_refreshAccessToken(const char* identifier) {
    NSString *nsIdentifier = LineSDKMakeNSString(identifier);
    [[LineSDKWrapper sharedInstance] refreshAccessTokenWithIdentifier:nsIdentifier];
}

LINE_SDK_EXTERNC void line_sdk_revokeAccessToken(const char* identifier) {
    NSString *nsIdentifier = LineSDKMakeNSString(identifier);
    [[LineSDKWrapper sharedInstance] revokeAccessTokenWithIdentifier:nsIdentifier];
}

LINE_SDK_EXTERNC void line_sdk_verifyAccessToken(const char* identifier);
void line_sdk_verifyAccessToken(const char* identifier) {
    NSString *nsIdentifier = LineSDKMakeNSString(identifier);
    [[LineSDKWrapper sharedInstance] verifyAccessTokenWithIdentifier:nsIdentifier];
}

LINE_SDK_EXTERNC void line_sdk_getProfile(const char* identifier);
void line_sdk_getProfile(const char* identifier) {
    NSString *nsIdentifier = LineSDKMakeNSString(identifier);
    [[LineSDKWrapper sharedInstance] getProfileWithIdentifier:nsIdentifier];
}

LINE_SDK_EXTERNC void line_sdk_getBotFriendshipStatus(const char* identifier);
void line_sdk_getBotFriendshipStatus(const char* identifier) {
    NSString *nsIdentifier = LineSDKMakeNSString(identifier);
    [[LineSDKWrapper sharedInstance] getBotFriendshipStatusWithIdentifier:nsIdentifier];
}

LINE_SDK_EXTERNC const char* line_sdk_getCurrentAccessToken();
const char* line_sdk_getCurrentAccessToken() {
    NSString *result = [[LineSDKWrapper sharedInstance] currentAccessToken];
    return LineSDKMakeCString(result);
}

