//
//  LineSDKAppDelegateListener.mm
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

#import "LineSDKAppDelegateListener.h"
#import "AppDelegateListener.h"
#import "LineSDKURLOpenning.h"

@interface LineSDKAppDelegateListener()<AppDelegateListener>

// Unity is using the parameter names as the dictionary key when sending a system event.
// So we cannot recognize it is statble all the time (because changing the parameter name would not break Unity working)
// This set of keys is for customizing if necessary in future.
// Check `-application:openURL:sourceApplication:annotation:` in UnityAppController.mm for the used keys.
@property (nonatomic, copy) NSString *urlKey;
@property (nonatomic, copy) NSString *sourceApplicationKey;
@property (nonatomic, copy) NSString *annotationKey;

@end

@implementation LineSDKAppDelegateListener

- (instancetype)init {
    self = [super init];
    if (self) {
        _urlKey = @"url";
        _sourceApplicationKey = @"sourceApplication";
        _annotationKey = @"annotation";
        UnityRegisterAppDelegateListener(self);
    }
    return self;
}

- (void)onOpenURL:(NSNotification *)notification {
    NSDictionary *dic = [notification userInfo];
    NSURL *url = dic[self.urlKey];
    NSString *sourceApplication = dic[self.sourceApplicationKey];
    NSString *annotation = dic[self.annotationKey];

    [LineSDKURLOpenning openURL:url sourceApplication:sourceApplication annotation:annotation];
}

@end
