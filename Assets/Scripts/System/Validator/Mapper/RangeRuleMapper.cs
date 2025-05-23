//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      he code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Xml;
using Core.Extensions;
using Core.Validator.Builder;
using Core.Validator.Message;

namespace Core.Validator.Mapper
{
    public sealed class RangeRuleMapper : BaseRuleMapper
    {
        public override Dictionary<string, object> Map(XmlNodeList ruleNodeList)
        {
            var result = new Dictionary<string, object>();
            var builder = new RangeValidatorUnitBuilder();
            ruleNodeList.ForEach(node =>
            {
                node.Attributes.ForEach(attribute =>
                {
                    var attributeValue = attribute.Value.ToLower();
                    if (attributeValue.Equals("type"))
                    {
                        var typeName = node.InnerText.ToLower();
                        builder.AddType(typeName);
                    }
                    else if (attributeValue.Equals("min"))
                    {
                        var minValue = node.InnerText.ToLower();
                        builder.AddMin(minValue);
                    }
                    else if (attributeValue.Equals("max"))
                    {
                        var maxValue = node.InnerText.ToLower();
                        builder.AddMax(maxValue);
                    }
                    else if (attributeValue.Equals("option"))
                    {
                        var option = node.InnerText.ToLower();
                        builder.AddOption(option);
                    }
                    else if (attributeValue.Equals("summary"))
                    {
                        var summary = node.InnerText.ToLower();
                        builder.AddMessage(new ErrorValidateMessage(summary));
                    }
                });
            });
            result.Add(builder.type, builder.Build());
            return result;
        }
    }
}