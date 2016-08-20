using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
namespace UnityPlugin.UI.Dialog {
public class ReviewModalDialog {
    [DllImport("__Internal")]
    private static extern void showReviewModalDialog(string storeUrl);
    public void Show(string storeUrl) {
        if (RuntimePlatform.IPhonePlayer == Application.platform) {
            showReviewModalDialog(storeUrl);
        } else if (RuntimePlatform.Android == Application.platform) {
            using(AndroidJavaObject plugin = new AndroidJavaObject("com.ui.dialog.ReviewModalDialog")) {
                plugin.Call("show", storeUrl);
            }
        } else {
#if UNITY_STANDALONE
            UnityManagedPlugin.UI.Dialog.ReviewModalDialog dialog = new UnityManagedPlugin.UI.Dialog.ReviewModalDialog();
            dialog.SetTitle("ダイアログテスト");
            dialog.Show();
#endif
        }
    }
}
}