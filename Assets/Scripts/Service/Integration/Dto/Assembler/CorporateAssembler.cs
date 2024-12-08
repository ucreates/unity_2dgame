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
using Core.Extensions;
using Service.Integration.Table;

namespace Service.Integration.Dto.Assembler
{
    public sealed class CorporateAssembler : XmlAssembler<MCorporateTable>
    {
        public CorporateAssembler() : base("Config/corporate")
        {
        }

        public override List<MCorporateTable> WriteToTableList()
        {
            var ret = new List<MCorporateTable>();
            var elementList = GetElementList();
            var record = new MCorporateTable();
            elementList.ForEach(element =>
            {
                var value = element.Attribute("type").Value;
                if (value.ToLower().Equals("name"))
                    record.campanyName = value;
                else if (value.ToLower().Equals("business"))
                    record.buisiness = value;
                else if (value.ToLower().Equals("copyright"))
                    record.copyright = value;
            });

            ret.Add(record);
            return ret;
        }
    }
}