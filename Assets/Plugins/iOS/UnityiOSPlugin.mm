//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
#import <Foundation/Foundation.h>
#import <hetappybird-Swift.h>
static WebBrowser* browser;
extern "C" void showReviewModalDialog(char* appStoreUrl) {
    NSString* url = [NSString stringWithCString: appStoreUrl encoding:NSUTF8StringEncoding];
    [ReviewModalDialog show:url];
}
extern "C" void showWebBrowser(char* url, CGFloat left, CGFloat top, CGFloat right, CGFloat bottom) {
    if (nil != browser) {
        return;
    }
    NSString* requestUrl = [NSString stringWithCString: url encoding:NSUTF8StringEncoding];
    browser = [WebBrowser alloc];
    [browser create:requestUrl left:left top:top right:right bottom:bottom];
    [browser show];
}
extern "C" void hideWebBrowser() {
    if (nil != browser) {
        [browser hide];
        [browser destroy];
        browser = nil;
    }
}
