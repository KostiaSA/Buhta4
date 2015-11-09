using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace Buhta
{

    public class BindingHub : Hub
    {
        
        public void SendBindedValueChanged(string modelBindingID, string propertyName, string newValue)
        {
            var obj = App.BindingModelList[modelBindingID];

            //Type type = obj.GetType();
            //PropertyInfo prop =   type.GetProperty(propertyName);
            //prop.SetValue(obj, newValue, null);
            obj.SetPropertyValue(propertyName, newValue);

            obj.FireOnChangeByHuman(obj, propertyName, newValue);

        }

        public void SendEvent(string modelBindingID, string funcName, dynamic args)
        {
            var obj = App.BindingModelList[modelBindingID];
            obj.InvokeMethod(funcName, args);

            //Type type = obj.GetType();
            //MethodInfo prop = type.GetMethod(funcName);
            //prop.Invoke(obj, null);

        }

        public void SubscribeBindedValueChanged(string modelBindingID, string propertyName)
        {
            BaseModel obj = App.BindingModelList[modelBindingID];
            var _obj =obj.GetPropertyObject(propertyName);
            var lastName = propertyName.Split('.').Last();
            _obj.OnChange += (sender, propName, newValue) =>
            {
                if (propName == lastName)
                {
                    Clients.Caller.receiveBindedValueChanged(modelBindingID, propertyName, newValue);
                }
            };
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var xx = 12;
            return base.OnDisconnected(stopCalled); 
            //var task = new Task7 (() => {
            //    va  d  dr xx = 12;
            //});w 
            //task.Start3();
            //ret urn task;
        }
        public override Task OnConnected()
        {
            var xx = 122222;
            return base.OnConnected();
        }
    }
}