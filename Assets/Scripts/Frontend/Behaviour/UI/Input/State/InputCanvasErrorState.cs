//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using Core.Entity;
using Core.Validator.Entity;
using Frontend.Component.Asset.Renderer.UI.Builder;
using Frontend.Component.State;
using Frontend.Component.Vfx;
using UnityEngine;
using UnityEngine.UI;
namespace Frontend.Behaviour.State
{
    public sealed class InputCanvasErrorState : FiniteState<InputCanvasBehaviour> {
    private TimeLine alphaTimeLine {
        get;
        set;
    }
    private InputUIAssetErrorDialogBuilder builder {
        get;
        set;
    }
    private float previousAlpha {
        get;
        set;
    }
    public override void Create(Parameter parameter) {
        ValidatorResponse vres = parameter.Get<ValidatorResponse>("ValidateResponse");
        this.previousAlpha = 0f;
        this.alphaTimeLine = new TimeLine();
        Transform md = this.owner.transform.Find("ModalDialog");
        Image mdimg = md.GetComponent<Image>();
        mdimg.gameObject.SetActive(false);
        Transform ed = this.owner.transform.Find("ErrorDialog");
        Image edimg = ed.GetComponent<Image>();
        edimg.gameObject.SetActive(true);
        if (null == this.builder) {
            this.builder = new InputUIAssetErrorDialogBuilder();
        } else {
            this.builder.Reset();
        }
        this.builder
        .AddErrorMessage(vres.GetMessageList())
        .AddAlpha(0f)
        .AddTransform(ed)
        .Build();
    }
    public override void Update() {
        float alpha = Flash.Update(this.alphaTimeLine.currentTime);
        if (alpha < this.previousAlpha) {
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
