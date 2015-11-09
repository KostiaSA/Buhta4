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
        public static MvcHtmlString xButton(this HtmlHelper helper, xButtonSettings settings)
        {
            return new MvcHtmlString(new xButton(settings).GetHtml());
        }

        public static MvcHtmlString xButton(this HtmlHelper helper, Action<xButtonSettings> settings)
        {

            return new MvcHtmlString(new xButton(settings).GetHtml());
        }

    }

    public class xButtonSettings : xControlSettings
    {
        public string Text;
        public string BindTextTo;
        public int? Width = 150;
        public int? Height;
        public string BindOnClickTo;
        public string BindDisabledTo;

        //public event xControlOnClickEventHandler<xButton> OnClick;

        //public string FireOnOnClick(xButton sender)
        //{
        //    if (OnClick != null)
        //        return OnClick(sender);
        //    else
        //        return null;
        //}

    }

    public class xButton : xControl<xButtonSettings>
    {
        public override string GetJqxName()
        {
            return "jqxButton";
        }

        public xButton(xButtonSettings settings)
        {
            Settings = settings;
        }

        public xButton(Action<xButtonSettings> settings)
        {
            Settings = new xButtonSettings();
            settings(Settings);
        }

        public override string GetHtml()
        {
            EmitBeginScript(Script);

            EmitSetPropertyM(Script, "val", Settings.Text);

            EmitSetPropertyPx(Script, "width", Settings.Width);

            EmitBindEvent(Script, Settings.BindOnClickTo, "click");

            EmitSubscribeModelPropertyChanged(Script, Settings.BindDisabledTo, "disabled");

            Html.Append("<input type='button'  id='" + UniqueId + "'/>");

            return base.GetHtml();
    }

}
}
