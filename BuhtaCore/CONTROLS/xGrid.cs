using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Buhta
{
    public static partial class HtmlHelperExtensions
    {
        public static MvcHtmlString xGrid(this HtmlHelper helper, xGridSettings settings)
        {
            return new MvcHtmlString(new xGrid(helper.ViewData.Model, settings).GetHtml());
        }

        public static MvcHtmlString xGrid(this HtmlHelper helper, Action<xGridSettings> settings)
        {

            return new MvcHtmlString(new xGrid(helper.ViewData.Model, settings).GetHtml());
        }

    }

    public class xGridSettings : xControlSettings
    {
        public bool? Disabled;
        public string Disabled_Bind;

        public int? Width;
        public string Width_Bind;

        public int? Height;
        public string Height_Bind;

        public string DataSource_Bind;

        List<xGridColumnSettings> columns = new List<xGridColumnSettings>();
        public List<xGridColumnSettings> Columns { get { return columns; } }

        public void AddColumn(Action<xGridColumnSettings> settings)
        {
            var col = new xGridColumnSettings();
            settings(col);
            columns.Add(col);
        }

    }

    public class xGrid : xControl<xGridSettings>
    {
        public override string GetJqxName()
        {
            return "jqxGrid";
        }

        public xGrid(object model, xGridSettings settings) : base(model, settings) { }
        public xGrid(object model, Action<xGridSettings> settings) : base(model, settings) { }



        //public xGrid(xGridSettings settings)
        //{
        //    Settings = settings;
        //}

        //public xGrid(Action<xGridSettings> settings)
        //{
        //    Settings = new xGridSettings();
        //    settings(Settings);
        //}

        public override string GetHtml()
        {
            EmitBeginScript(Script);

            EmitProperty_Px(Script, "width", Settings.Width);
            EmitProperty_Bind(Script, Settings.Width_Bind, "width");

            EmitProperty_Px(Script, "height", Settings.Height);
            EmitProperty_Bind(Script, Settings.Height_Bind, "height");

            EmitProperty(Script, "disabled", Settings.Disabled);
            EmitProperty_Bind(Script, Settings.Disabled_Bind, "disabled");



            Script.AppendLine("var columns=[];");
            Script.AppendLine("var col;");
            foreach (var col in Settings.Columns)
            {
                Script.AppendLine("col={};");
                if (col.Caption != null)
                    Script.AppendLine("col.text=" + col.Caption.AsJavaScript() + ";");
                if (col.Field_Bind != null)
                    Script.AppendLine("col.displayfield=" + col.Field_Bind.AsJavaScript() + ";");
                Script.AppendLine("columns.push(col);");
            }
            Script.AppendLine("tag." + GetJqxName() + "({columns:columns});");


            Html.Append("<div id='" + UniqueId + "'/>");

            return base.GetHtml();
        }

    }
}
