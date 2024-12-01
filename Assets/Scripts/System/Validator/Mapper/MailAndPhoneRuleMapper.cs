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
    public sealed class MailAndPhoneRuleMapper : BaseRuleMapper
    {
        private const string EMAIL_ADRRESS_REGEX = "^[a-z|A-Z|0-9\\.\\-]+@[a-z|A-Z|0-9\\-]+\\.[a-z|A-Z|0-9\\.\\-]+$";
        private const string PHONE_NUMBER_REGEX = "^\\d{2,6}[\\-]?\\d{0,4}[\\-]?\\d{4}$";

        public override Dictionary<string, object> Map(XmlNodeList ruleNodeList)
        {
            var ret = new Dictionary<string, object>();
            for (var i = 0; i < 2; i++)
            {
                var builder = new RegexValidatorUnitBuilder();
                if (i == 0)
                    builder.AddPattern(EMAIL_ADRRESS_REGEX);
                else
                    builder.AddPattern(PHONE_NUMBER_REGEX);
                foreach (XmlNode ruleNode in ruleNodeList)
                foreach (XmlAttribute ruleAttr in ruleNode.Attributes)
                {
                    var attrValue = ruleAttr.Value.ToLower();
                    if (i == 0 && attrValue.Equals("mailerrorsummary"))
                    {
                        var summary = ruleNode.InnerText;
                        builder.AddMessage(new ErrorValidateMessage(summary));
                        break;
                    }

                    if (i == 1 && attrValue.Equals("phoneerrorsummary"))
                    {
                        var summary = ruleNode.InnerText;
                        builder.AddMessage(new ErrorValidateMessage(summary));
                        break;
                    }
                }

                var builderType = string.Empty;
                if (i == 0)
                    builderType = builder.type + "::mail";
                else
                    builderType = builder.type + "::phone";
                ret.Add(builderType, builder.Build());
            }

            return ret;
        }
    }
}