using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Buhta
{

    public class xGridColumnSettings : xControlSettings
    {

        public int? Width;
        public string Width_Bind;

        public string Caption;
        public string Caption_Bind;

        public string Field_Bind;

    }

    public class xGridColumn 
    {

        public xGridColumnSettings Settings;

        public xGrid Grid { get; private set; }

        public xGridColumn()
        {
            Settings = new xGridColumnSettings();
        }

        public string GetHtml()
        {

            //EmitProperty_Px(Script, "width", Settings.Width);
            //EmitProperty_Bind(Script, Settings.Width_Bind, "width");

            return "";
        }

    }
}
