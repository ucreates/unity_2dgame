//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
#import "UnityAppController.h"
#import "UnityNativePlugin.h"
#import "UnityiOSPlugin.h"
#import <hetappybird-Swift.h>
@interface UnityiOSPluginAppController : UnityAppController
@end
@implementation UnityiOSPluginAppController
- (void)preStartUnity {
}
- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    BOOL ret = [super application:application didFinishLaunchingWithOptions:launchOptions];
    if (NO == ret) {
        return ret;
    }
    return [UnityiOSPluginAppDelegate application:application didFinishLaunchingWithOptions:launchOptions];
}
- (void)applicationDidBecomeActive:(UIApplication *)application {
    [UnityiOSPluginAppDelegate reset];
    [super applicationDidBecomeActive:application];
    return;
}
- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options {
    return [UnityiOSPluginAppDelegate application:app url:url options:options];
}
#if __IPHONE_OS_VERSION_MAX_ALLOWED <= __IPHONE_10_0
- (BOOL)application:(UIApplication *)app handleOpenURL:(NSURL *)url {
    return [UnityiOSPluginAppDelegate application:app url:url];
}
#endif
- (void)applicationWillEnterForeground:(UIApplication *)application {
    [super applicationWillEnterForeground:application];
    return;
}
#if __IPHONE_OS_VERSION_MAX_ALLOWED <= __IPHONE_10_0
- (void)application:(UIApplication *)application didReceiveLocalNotification:(UILocalNotification *)notification {
    return;
}
#endif
#if __IPHONE_OS_VERSION_MAX_ALLOWED <= __IPHONE_10_0
- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult))completionHandler {
    return;
}
#endif
#if __IPHONE_OS_VERSION_MAX_ALLOWED <= __IPHONE_10_0
- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo {
    return;
}
#endif
#if __IPHONE_OS_VERSION_MAX_ALLOWED <= __IPHONE_10_0
- (void) application:(UIApplication *)application didRegisterUserNotificationSettings:(UIUserNotificationSettings *)notificationSettings {
    return;
}
#endif
- (void) application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
    NSString* devToken = [DeviceTokenPlugin toStringWithDeviceToken:deviceToken];
    NSLog(@"deviceToken::%@", devToken);
    return;
}
- (void) application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error {
    return;
}
- (void)shouldAttachRenderDelegate {
    UnityRegisterRenderingPluginV5(&UnityPluginLoad, &UnityPluginUnload);
    return;
}
@end
IMPL_APP_CONTROLLER_SUBCLASS(UnityiOSPluginAppController)
