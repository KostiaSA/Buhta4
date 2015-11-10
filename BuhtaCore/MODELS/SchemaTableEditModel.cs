using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buhta
{
    public class SchemaTableEditModel : SchemaObjectEditModel<SchemaTable>
    {
        public SchemaTable Table { get { return EditedObject; } }

        public void Test1(dynamic args)
        {
            Table.Name = "Жопа";
            Hub.Clients.Group(BindingId).receiveBindedValuesChanged(BindingId, "Table.Columns");

        }
    }
}