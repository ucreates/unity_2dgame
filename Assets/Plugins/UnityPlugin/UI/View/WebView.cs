using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
namespace UnityPlugin.UI.View {
public class WebView {
    [DllImport("__Internal")]
    private static extern void showWebBrowser(string url, float left, float top, float right, float bottom);
    [DllImport("__Internal")]
    private static extern void hideWebBrowser();
    private AndroidJavaObject androidPlugin {
        get;
        set;
    }
    public WebView() {
        if (RuntimePlatform.Android == Application.platform) {
            this.androidPlugin = new AndroidJavaObject("com.ui.view.WebBrowser");
        }
    }
    public void Show(string requestUrl, float left, float top, float right, float bottom) {
        if (RuntimePlatform.IPhonePlayer == Application.platform) {
            showWebBrowser(requestUrl, left, top, right, bottom);
        } else if (RuntimePlatform.Android == Application.platform) {
            if (null != this.androidPlugin) {
                this.androidPlugin.Call("create", requestUrl, (int)left, (int)top, (int)right, (int)bottom);
                this.androidPlugin.Call("show");
            }
        } else {
#if UNITY_STANDALONE
            UnityManagedPlugin.UI.View.WebBrowser browser = new UnityManagedPlugin.UI.View.WebBrowser();
            browser.Show(requestUrl);
#endif
        }
    }
    public void Hide() {
        if (RuntimePlatform.IPhonePlayer == Application.platform) {
            hideWebBrowser();
        } else if (RuntimePlatform.Android == Application.platform) {
            if (null != this.androidPlugin) {
                this.androidPlugin.Call("hide");
                this.androidPlugin.Call("destroy");
            }
        }
    }
}
}