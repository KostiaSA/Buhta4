using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buhta
{
    public class SchemaTableEditModel : SchemaObjectEditModel<SchemaTable>
    {
        public SchemaTable Table { get { return EditedObject; } }

        public void EditFirstColumnButtonClick(dynamic args)
        {
            var model = new SchemaTableColumnEditModel();
            model.Column = Table.Columns[0];

//            var xxx = RenderPartialViewToString(@"C:\Buhta4Git\Buhta4\Buhta4\Views\Shared\BuhtaCore\TableColumnEditorWindow1.cshtml", model);
            var xxx = RenderPartialViewToString(@"TableColumnEditorWindow", model);
        }

        public void Test1ButtonClick(dynamic args)
        {
            Table.Name = "Жопа";

            SchemaTableColumn col;

            col = new SchemaTableColumn(); col.Table = Table; Table.Columns.Add(col);
            col.Name = "новая колонка";
            col.Description = "давай!";


            UpdateCollection(nameof(Table) + "." + nameof(Table.Columns));

            //var arr = new List<string[]>();
            //arr.Add(new string[] { "бухта"," воронеж"});
            //arr.Add(new string[] { "бухта-сбп", "питер" });
            //arr.Add(new string[] { "дом", "москва","париж" });

            //Hub.Clients.Group(BindingId).receiveBindedValuesChanged(BindingId, arr);

        }
    }
}