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
using System.Xml;
using Core.Extensions;
using Core.Validator.Builder;
using Core.Validator.Message;

namespace Core.Validator.Mapper
{
    public sealed class CompareRuleMapper : BaseRuleMapper
    {
        public override Dictionary<string, object> Map(XmlNodeList ruleNodeList)
        {
            var ret = new Dictionary<string, object>();
            var builder = new CompareValidatorUnitBuilder();
            ruleNodeList.ForEach(node =>
            {
                node.Attributes.ForEach(attribute =>
                {
                    var attrValue = attribute.Value.ToLower();
                    if (attrValue.Equals("type"))
                    {
                        var typeName = node.InnerText.ToLower();
                        builder.AddType(typeName);
                    }
                    else if (attrValue.Equals("comparevalue"))
                    {
                        var compareValue = node.InnerText.ToLower();
                        builder.AddCompareValue(compareValue);
                    }
                    else if (attrValue.Equals("comparetype"))
                    {
                        var option = node.InnerText.ToLower();
                        builder.AddCompareTypeOption(option);
                    }
                    else if (attrValue.Equals("equaloption"))
                    {
                        var option = node.InnerText.ToLower();
                        builder.AddEqualOption(option);
                    }
                    else if (attrValue.Equals("summary"))
                    {
                        var summary = node.InnerText.ToLower();
                        builder.AddMessage(new ErrorValidateMessage(summary));
                    }
                });
            });
            ret.Add(builder.type, builder.Build());
            return ret;
        }
    }
}