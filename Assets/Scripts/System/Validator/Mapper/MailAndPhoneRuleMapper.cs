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
using Core.Extensions.Array;
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
            var result = new Dictionary<string, object>();
            var patterns = new[] { EMAIL_ADRRESS_REGEX, PHONE_NUMBER_REGEX };
            patterns.ForEach(pattern =>
            {
                var builder = new RegexValidatorUnitBuilder();
                builder.AddPattern(pattern);
                ruleNodeList.ForEach(node =>
                {
                    node.Attributes.ForEach(attribute =>
                    {
                        var attributeValue = attribute.Value.ToLower();
                        if ((pattern.Equals(EMAIL_ADRRESS_REGEX) && attributeValue.Equals("mailerrorsummary")) || (pattern.Equals(PHONE_NUMBER_REGEX) && attributeValue.Equals("phoneerrorsummary")))
                        {
                            var summary = node.InnerText;
                            builder.AddMessage(new ErrorValidateMessage(summary));
                            return false;
                        }

                        return true;
                    });
                });
                var builderType = pattern.Equals(EMAIL_ADRRESS_REGEX) ? $"{builder.type}::mail" : $"{builder.type}::phone";
                result.Add(builderType, builder.Build());
            });
            return result;
        }
    }
}