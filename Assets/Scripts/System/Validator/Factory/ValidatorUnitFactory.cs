//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Xml;
using Core.Extensions;
using Core.Validator.Unit;

namespace Core.Validator.Factory
{
    public sealed class ValidatorUnitFactory
    {
        public static ValidatorUnitEntity FactoryMethod(XmlNodeList ruleXmlNodeList)
        {
            var vfr = new ValidatorUnitEntity();
            ruleXmlNodeList.ForEach(node =>
            {
                node.Attributes.ForEach(attribute =>
                {
                    if (attribute.Name.ToLower().Equals("type"))
                    {
                        var attrValue = attribute.Value.ToLower();
                        var ruleMapper = RuleMapperFactory.FactoryMethod(attrValue);
                        vfr.validatorUnitList[attrValue] = ruleMapper.Map(node.ChildNodes);
                    }
                });
            });
            return vfr;
        }
    }
}