using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Buhta
{
    public class BaseModel : ObservableObject
    {
        string bindingId;
        public string BindingId
        {
            get
            {
                if (bindingId == null)
                {
                    bindingId = Guid.NewGuid().ToString();
                    App.BindingModelList.Add(bindingId, this);
                }
                return bindingId;
            }
        }

        public PropertyInfo GetProperty(string propName)
        {
            var names = propName.Split('.');
            object obj = this;
            if (names.Length > 1)
            {
                for (int i = 0; i < names.Length - 1; i++)
                {
                    Type _type = obj.GetType();
                    PropertyInfo _prop = _type.GetProperty(names[i]);
                    obj = _prop.GetValue(obj);
                    if (obj == null)
                        return null;
                }
            }
            Type type = obj.GetType();
            PropertyInfo prop = type.GetProperty(names.Last());
            return prop;
        }

        public object GetPropertyValue(string propName)
        {
            var names = propName.Split('.');
            object obj = this;
            for (int i = 0; i < names.Length; i++)
            {
                Type _type = obj.GetType();
                PropertyInfo _prop = _type.GetProperty(names[i]);
                if (_prop == null)
                    throw new Exception("model." + nameof(GetPropertyValue) + ": не найдено свойство '" + names[i] + "' в '" + propName + "'");
                obj = _prop.GetValue(obj);
                if (obj == null)
                    return null;
                if (i == names.Length - 1)
                    return obj;
            }
            return null;
        }

        public ObservableObject GetPropertyObject(string propName)
        {
            var names = propName.Split('.');
            if (names.Length == 1)
                return this;
            object obj = this;
            for (int i = 0; i < names.Length; i++)
            {
                Type _type = obj.GetType();
                PropertyInfo _prop = _type.GetProperty(names[i]);
                if (_prop == null)
                    throw new Exception("model." + nameof(GetPropertyObject) + ": не найдено свойство '" + names[i] + "' в '" + propName + "'");
                obj = _prop.GetValue(obj);
                if (obj == null)
                    return null;
                if (i == names.Length - 2)
                {
                    if (obj is ObservableObject)
                        return obj as ObservableObject;
                    else
                        throw new Exception("model." + nameof(GetPropertyObject) + ": объект должен быть типа " + nameof(ObservableObject) + " в '" + propName + "'");
                }
            }
            return null;
        }

        public void SetPropertyValue(string propName, object value)
        {
            var names = propName.Split('.');
            object obj = this;
            for (int i = 0; i < names.Length; i++)
            {
                Type _type = obj.GetType();
                PropertyInfo _prop = _type.GetProperty(names[i]);
                if (_prop == null)
                    throw new Exception("model." + nameof(SetPropertyValue) + ": не найдено свойство '" + names[i] + "' в '" + propName + "'");
                if (i < names.Length - 1)
                {
                    obj = _prop.GetValue(obj);
                    if (obj == null)
                        throw new Exception("model." + nameof(SetPropertyValue) + ": объект '" + names[i] + "'==null в '" + propName + "'");
                }
                else
                {
                    _prop.SetValue(obj, value, null);
                }

            }
        }

        public void InvokeMethod(string propName, dynamic args)
        {
            var names = propName.Split('.');
            object obj = this;
            for (int i = 0; i < names.Length; i++)
            {
                if (i < names.Length - 1)
                {
                    Type _type = obj.GetType();
                    PropertyInfo _prop = _type.GetProperty(names[i]);
                    if (_prop == null)
                        throw new Exception("model." + nameof(InvokeMethod) + ": не найдено свойство '" + names[i] + "' в '" + propName + "'");
                    obj = _prop.GetValue(obj);
                    if (obj == null)
                        throw new Exception("model." + nameof(InvokeMethod) + ": объект '" + names[i] + "'==null в '" + propName + "'");
                }
                else
                {
                    Type _type = obj.GetType();
                    MethodInfo _method = _type.GetMethod(names[i]);
                    if (_method == null)
                        throw new Exception("model." + nameof(InvokeMethod) + ": не найден метод '" + names[i] + "' в '" + propName + "'");

                    _method.Invoke(obj, new dynamic[] { args });
                    return;

                }

            }
        }

    }
}