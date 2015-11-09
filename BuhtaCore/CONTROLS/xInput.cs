using Nustache.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Buhta
{
    public class xInputSettings: xControlSettings
    {
        public string Label;
        public string BindValueTo;
        public int Width;
        public int Height;
        public event InputControlOnChangeEventHandler OnChange;

        public void FireOnChange(xInput sender, object newValue)
        {
            if (OnChange != null)
                OnChange(sender, newValue);
        }

    }

    public static partial class HtmlHelperExtensions
    {
        public static MvcHtmlString xInput(this HtmlHelper helper, xInputSettings settings)
        {
            return new MvcHtmlString(new xInput(settings).GetHtml());
        }

        public static MvcHtmlString xInput(this HtmlHelper helper, Action<xInputSettings> settings)
        {

            return new MvcHtmlString(new xInput(settings).GetHtml());
        }

    }

    public delegate void InputControlOnChangeEventHandler(xInput sender, object newValue);

    public class xInput : xControl<xInputSettings>
    {
        xInputSettings settings;
        public xInput(xInputSettings _settings)
        {
            settings = _settings;
        }
        public xInput(Action<xInputSettings> _settings)
        {
            settings = new xInputSettings();
            _settings(settings);
        }

        public override string GetHtml()
        {
            var script = @"
<script>
    $(document).ready(function() {
        var countries = new Array('Россия', ""Армения"", 'Algeria', 'Andorra', 'Angola');
        var tag=$('#{{id}}');
        tag.jqxInput({ placeHolder: 'Enter a Country', height: {{settings.Height}}, width: {{settings.Width}}, minLength: 1,  source: countries, value: {{{value}}} });

        tag.on('change', function () { 
            //var value = $('#{{id}}').val(); alert('Ok');
            bindingHub.server.sendBindedValueChanged('{{settings.Model.BindingId}}', '{{settings.BindValueTo}}',tag.val());

            });

            bindingHub.client.receiveBindedValueChanged = function (modelBindingID, propertyName, newValue) {
               if (modelBindingID=='{{settings.Model.BindingId}}' && propertyName=='{{settings.BindValueTo}}'){
                 tag.val(newValue);
                //alert('Ok-'+name+' '+message);
               };
            }; 

        $.connection.hub.start().done(function () {
            bindingHub.server.subscribeBindedValueChanged('{{settings.Model.BindingId}}', '{{settings.BindValueTo}}');

        });

    });
</script>
   ";
            settings.Model.OnChangeByHuman += (sender, propertyName, newValue) =>
            {
                if (propertyName == settings.BindValueTo)
                    settings.FireOnChange(this, newValue);
            };

            //Type type = settings.Model.GetType();
            //PropertyInfo prop = type.GetProperty(settings.BindValueTo);
            //var value= HttpUtility.JavaScriptStringEncode(prop.GetValue(settings.Model).ToString(), true);
            var value= HttpUtility.JavaScriptStringEncode(settings.Model.GetPropertyValue(settings.BindValueTo).ToString(), true);

            script = Render.StringToString(script, new { id = UniqueId, settings = settings, value= value});

            var tag = @"<input type = 'text' id = '{{id}}' />";
            tag = Render.StringToString(tag, new { id = UniqueId });

            return tag + script;
        }

        private void InputControl_OnChange(xInput sender, string newValue)
        {
            throw new NotImplementedException();
        }


    }
}
