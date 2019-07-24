//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using System.Collections.Generic;
using System.Xml.Linq;
using Service.Integration.Table;
using UnityEngine;
namespace Service.Integration.Dto.Assembler
{
    public sealed class StoreAssembler : XmlAssembler<MStoreTable> {
    public const string VALID_PURCHASE_SUCCESS = "購入しました。";
    public const string VALID_PURCHASE_FAILD_NO_COIN = "コインが足りません。";
    public const string VALID_PURCHASE_FAILD_HAD_ITEM = "すでにそのアイテムは購入済みです。";
    public const string INVALID_PURCHASE_SUCCESS = "購入しません。";
    public const string INVALID_PURCHASE_FAILD_NO_COIN = "コインがあり余っています。";
    public const string INVALID_PURCHASE_FAILD_HAD_ITEM = "すでにそのアイテムは買ったかもしれませんが、実際の所どうでしょうか？？";
    public StoreAssembler() : base("Config/store") {}
    public override List<MStoreTable> WriteToTableList() {
        List<MStoreTable> ret = new List<MStoreTable>();
        IEnumerable<XElement> elementChildList = this.GetElementList();
        string url = string.Empty;
        foreach (XElement element in elementChildList) {
            if (Application.platform == RuntimePlatform.IPhonePlayer && element.Attribute("type").Value.ToLower().Equals("ios")) {
                url = element.Value;
                break;
            } else if (Application.platform == RuntimePlatform.Android && element.Attribute("type").Value.ToLower().Equals("android")) {
                url = element.Value;
                break;
            } else if (element.Attribute("type").Value.ToLower().Equals("pc")) {
                url = element.Value;
                break;
            }
        }
        MStoreTable record = new MStoreTable();
        record.url = url.Trim();
        ret.Add(record);
        return ret;
    }
}
}
