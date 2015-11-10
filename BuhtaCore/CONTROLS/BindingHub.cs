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
            BaseModel obj = App.BindingModelList[modelBindingID];
            obj.Hub = this;
            Groups.Add(Context.ConnectionId, modelBindingID /*это groupName*/);

            obj.SetPropertyValue(propertyName, newValue);

            obj.FireOnChangeByBrowser(obj, propertyName, newValue);

        }

        public void SendEvent(string modelBindingID, string funcName, dynamic args)
        {
            BaseModel obj = App.BindingModelList[modelBindingID];
            obj.Hub = this;
            Groups.Add(Context.ConnectionId, modelBindingID /*это groupName*/);

            obj.InvokeMethod(funcName, args);
        }

        public void SubscribeBindedValueChanged(string modelBindingID, string propertyName)
        {
            BaseModel obj = App.BindingModelList[modelBindingID];
            obj.Hub = this;
            Groups.Add(Context.ConnectionId, modelBindingID /*это groupName*/);

            //var _obj =obj.GetPropertyObject(propertyName);

            //var lastName = propertyName.Split('.').Last();
            //_obj.OnChange += (sender, propName, newValue) =>
            //{
            //    if (propName == lastName)
            //    {
            //        Clients.Group(modelBindingID).receiveBindedValueChanged(modelBindingID, propertyName, newValue);
            //    }
            //};
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled); 
        }
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }
}