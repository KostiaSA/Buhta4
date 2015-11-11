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

            var arr = new List<string[]>();
            arr.Add(new string[] { "бухта"," воронеж"});
            arr.Add(new string[] { "бухта-сбп", "питер" });
            arr.Add(new string[] { "дом", "москва","париж" });

            Hub.Clients.Group(BindingId).receiveBindedValuesChanged(BindingId, arr);

        }
    }
}