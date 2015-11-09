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
            Table.Name = "событие 33";
            SaveButtonDisabled = true;
        }

        [ChangeNotify]
        public bool SaveButtonDisabled { get; set; }

        [ChangeNotify]
        public string SaveButtonText { get; set; }
    }
}