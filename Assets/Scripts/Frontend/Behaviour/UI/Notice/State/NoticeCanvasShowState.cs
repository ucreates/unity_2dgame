//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;
namespace Frontend.Behaviour.State
{
    public sealed class NoticeCanvasShowState : FiniteState<NoticeCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private NoticeCanvasBuilder builder {
        get;
        set;
    }
    private float previousAlpha {
        get;
        set;
    }
    public override void Create() {
        Vector3[] corners = new Vector3[ 4 ];
        this.owner.webViewAreaImage.rectTransform.GetWorldCorners(corners);
        Vector2 luScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, corners[1]);
        Vector2 rdScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, corners[3]);
        int leftMargin = (int)luScreenPoint.x;
        int rightMargin = (int)(UnityEngine.Screen.width - rdScreenPoint.x);
        int topMargin = (int)(UnityEngine.Screen.height - luScreenPoint.y);
        int bottomMargin = (int)rdScreenPoint.y;
        this.owner.webViewPlugin.Show("http://www.yahoo.co.jp/", leftMargin, topMargin, rightMargin, bottomMargin, UnityEngine.Screen.width, UnityEngine.Screen.height);
        Rect rect = new Rect();
        rect.x = leftMargin;
        rect.width = Screen.width - leftMargin - rightMargin;
        rect.y = topMargin;
        rect.height = Screen.height - topMargin - bottomMargin;
        this.owner.screenRect = rect;
        Canvas canvas = this.owner.GetComponent<Canvas>();
        if (null != canvas) {
            canvas.enabled = true;
        }
        this.alphaTimeLine = new TimeLine();
        this.previousAlpha = 0f;
        this.builder = new NoticeCanvasBuilder();
        this.builder
        .AddTransform(this.owner.transform)
        .AddAlpha(0f)
        .AddEnabled(false)
        .Build();
    }
    public override void Update() {
        float alpha = Flash.Update(this.alphaTimeLine.currentTime);
        if (alpha < this.previousAlpha) {
            this.owner.stateMachine.Change("stay");
            return;
        }
        this.builder
        .AddAlpha(alpha)
        .Update();
        this.previousAlpha = alpha;
        this.alphaTimeLine.Next(1.5f);
    }
}
}
