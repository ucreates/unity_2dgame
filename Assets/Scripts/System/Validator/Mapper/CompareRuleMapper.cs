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
            foreach (XmlNode ruleNode in ruleNodeList)
            foreach (XmlAttribute ruleAttr in ruleNode.Attributes)
            {
                var attrValue = ruleAttr.Value.ToLower();
                if (attrValue.Equals("type"))
                {
                    var typeName = ruleNode.InnerText.ToLower();
                    builder.AddType(typeName);
                }
                else if (attrValue.Equals("comparevalue"))
                {
                    var compareValue = ruleNode.InnerText.ToLower();
                    builder.AddCompareValue(compareValue);
                }
                else if (attrValue.Equals("comparetype"))
                {
                    var option = ruleNode.InnerText.ToLower();
                    builder.AddCompareTypeOption(option);
                }
                else if (attrValue.Equals("equaloption"))
                {
                    var option = ruleNode.InnerText.ToLower();
                    builder.AddEqualOption(option);
                }
                else if (attrValue.Equals("summary"))
                {
                    var summary = ruleNode.InnerText.ToLower();
                    builder.AddMessage(new ErrorValidateMessage(summary));
                }
            }

            ret.Add(builder.type, builder.Build());
            return ret;
        }
    }
}