using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buhta
{
    public delegate void xControlOnChangeEventHandler<SenderT, NewValueT>(SenderT sender, NewValueT newValue);
    public delegate string xControlOnClickEventHandler<SenderT>(SenderT sender);

    public class xControlSettings
    {
        public BaseModel Model;
        public bool Visible = true;
        public bool Enabled = true;

        //public void BindProp(string modelProp, string controlProp);
        //public void BindFunc(string modelFunc, string controlFunc);

    }

    public class xControl<T> where T : xControlSettings
    {
        public T Settings;

        //public xControl()
        //{
        //    Settings = new T(); ;
        //}
        //public xControl(T _settings)
        //{
        //    Settings = _settings;
        //}
        //public xControl(Action<T> _settings)
        //{
        //    Settings = new T();
        //    _settings(Settings);
        //}

        public virtual string GetJqxName()
        {
            throw new Exception("метод '" + nameof(GetJqxName) + "' не реализован для " + GetType().Name);
        }

        public void EmitBeginScript(StringBuilder script)
        {
            Script.AppendLine("var tag =$('#" + UniqueId + "');");
            Script.AppendLine("tag." + GetJqxName() + "({});");
        }

        public void EmitSetProperty(StringBuilder script, string jqxPropertyName, object value)
        {
            if (value != null)
                Script.AppendLine("tag." + GetJqxName() + "({"+ jqxPropertyName + ":" + value.AsJavaScript() + "});");
        }

        //public void EmitSetProperty(StringBuilder script, string jqxPropertyName, int? value)
        //{
        //    if (value != null)
        //        Script.AppendLine("tag." + GetJqxName() + "({" + jqxPropertyName + ":" + value + "});");
        //}

        public void EmitSetPropertyPx(StringBuilder script, string jqxPropertyName, int? value)
        {
            if (value != null)
                Script.AppendLine("tag." + GetJqxName() + "({" + jqxPropertyName + ":'" + value + "px'});");
        }

        //public void EmitSetPropertyM(StringBuilder script, string jqxMethodName, int? value)
        //{
        //    if (value != null)
        //        Script.AppendLine("tag." + jqxMethodName + "("+ value + ");");
        //}

        public void EmitSetPropertyM(StringBuilder script, string jqxMethodName, object value)
        {
            if (value != null)
                Script.AppendLine("tag." + jqxMethodName + "(" + value.AsJavaScript() + ");");
        }


        public void EmitBindEvent(StringBuilder script, string modelMethodName, string jqxEventName)
        {
            if (modelMethodName != null)
            {
                Script.AppendLine("tag.on('"+ jqxEventName + "',function(event){");
                Script.AppendLine(" var args={}; if (event) {args=event.args || {}};");
                Script.AppendLine(" bindingHub.server.sendEvent('" + Settings.Model.BindingId + "','" + modelMethodName + "', args );");
                Script.AppendLine("});");

            }

        }

        public void EmitSubscribeModelPropertyChanged(StringBuilder script, string modelPropertyName, string jqxPropertyName)
        {
            if (modelPropertyName != null)
            {
                script.AppendLine("tag." + GetJqxName() + "({" + jqxPropertyName + ":"+ Settings.Model.GetPropertyValue(modelPropertyName).AsJavaScript() + "});");
                script.AppendLine("signalr.subscribeModelPropertyChanged('" + Settings.Model.BindingId + "', '" + modelPropertyName + "',function(newValue){");
                script.AppendLine("    tag." + GetJqxName() + "({" + jqxPropertyName + ":newValue});");
                script.AppendLine("});");
            }

        }

        public void EmitSubscribeModelPropertyChangedM(StringBuilder script, string modelPropertyName, string jqxMethodName)
        {
            if (modelPropertyName != null)
            {
                script.AppendLine("tag." + jqxMethodName + "(" +Settings.Model.GetPropertyValue(modelPropertyName).ToString().AsJavaScript() +");");
                script.AppendLine("signalr.subscribeModelPropertyChanged('" + Settings.Model.BindingId + "', '" + modelPropertyName + "',function(newValue){");
                script.AppendLine("    tag." + jqxMethodName + "(newValue);");
                script.AppendLine("});");
            }

        }

        public virtual string GetHtml()
        {
            return "<script>$(document).ready(function(){ " + Script + "});</script>" + Html;
        }

        string uniqueId;
        public string UniqueId
        {
            get
            {
                if (uniqueId == null)
                {
                    uniqueId = Guid.NewGuid().ToString().Substring(1, 6);
                }
                return uniqueId;
            }
        }

        protected StringBuilder Script = new StringBuilder();
        protected StringBuilder Html = new StringBuilder();
    }
}
