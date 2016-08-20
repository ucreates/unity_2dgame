//======================================================================
// Project Name    : unity plugin
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
import Foundation
import UIKit
public class WebBrowser : NSObject {
    var view : UIWebView!
    public func create(url:String,left:CGFloat,top:CGFloat,right:CGFloat,bottom:CGFloat){
        let controller = GLViewController.GetController();
        self.view = UIWebView()
        let width = controller.view.frame.width
        let height = controller.view.frame.height
        
        //left,top,width-left-right,height-top-bottom
        self.view.frame = CGRectMake(left,top,width-left-right,height-top-bottom);
        self.view.backgroundColor = UIColor.clearColor();
        self.view.opaque = false;
        controller.view.addSubview(self.view)
        let req : NSURLRequest = NSURLRequest(URL:NSURL(string:url)!)
        self.view.loadRequest(req)
    }
    public func show() {
        self.setVisible(true);
    }
    public func hide() {
        self.setVisible(false);
    }
    
    public func setVisible(visible:Bool) {
        if (nil == self.view) {
            return;
        }
        self.view.hidden = !visible;
    }
    
    public func destroy(){
        if (nil == self.view) {
            return;
        }
        self.view.removeFromSuperview()
    }
}