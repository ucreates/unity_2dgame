//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
import UIKit
public class Notifier : NSObject {
    public class func Notify(gameObjectName: String, method: String, msg: String) {
        UnitySendMessage(gameObjectName, method, msg)
    }
    
}