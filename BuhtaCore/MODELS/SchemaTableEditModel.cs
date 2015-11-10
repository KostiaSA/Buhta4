using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buhta
{
    public class SchemaTableEditModel : BaseModel
    {
        public SchemaTableEditModel()
        {
            SaveButtonText = "Сохранить таблицу?";
        }

        public SchemaTable Table { get; set; }

        public void SaveButtonClick(dynamic args)
        {
            var x = 33;
            Table.Description = Table.Description+"+";
            //SaveButtonDisabled = true;
            Update();
        }

        public bool SaveButtonDisabled { get { return Table.Name == "777"; } }

        public string SaveButtonText { get; set; }
    }
}