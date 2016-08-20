//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections;
namespace Core.Validator.Config {
public sealed class ValidationConfigEntity {
    //must change for your env.
    public string xmlSouceName {
        get;
        set;
    }
    //must change for your env.
    public ValidationConfig.SourceType sourceType {
        get {
            return ValidationConfig.SourceType.UnityAsset;
        }
    }
}
}
