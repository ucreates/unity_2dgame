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
    public sealed class RegexRuleMapper : BaseRuleMapper
    {
        public override Dictionary<string, object> Map(XmlNodeList ruleNodeList)
        {
            var ret = new Dictionary<string, object>();
            var builder = new RegexValidatorUnitBuilder();
            ruleNodeList.ForEach(node =>
            {
                node.Attributes.ForEach(attribute =>
                {
                    var attrValue = attribute.Value.ToLower();
                    if (attrValue.Equals("pattern"))
                    {
                        var pattern = node.InnerText;
                        builder.AddPattern(pattern);
                    }
                    else if (attrValue.Equals("summary"))
                    {
                        var summary = node.InnerText;
                        builder.AddMessage(new ErrorValidateMessage(summary));
                    }
                });
            });
            ret.Add(builder.type, builder.Build());
            return ret;
        }
    }
}