using Newtonsoft.Json;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buhta
{

    [Serializable]
    public class ChangeNotifyAttribute : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            ObservableObject obj = (ObservableObject)args.Instance;
            obj.FireOnChange(obj, args.LocationName, args.Value);
            base.OnSetValue(args);
        }
    }

    public delegate void ObservableObjectOnChangeEventHandler(ObservableObject sender, string propertyName, object newValue);

    public class ObservableObject 
    {


        public ObservableObject()
        {
        }

        public event ObservableObjectOnChangeEventHandler OnChangeByHuman;

        public void FireOnChangeByHuman(ObservableObject sender, string propertyName, object newValue)
        {
            if (OnChangeByHuman != null)
                OnChangeByHuman(sender, propertyName, newValue);
        }

        public event ObservableObjectOnChangeEventHandler OnChange;
        public void FireOnChange(ObservableObject sender, string propertyName, object newValue)
        {
            if (OnChange != null)
                OnChange(sender, propertyName, newValue);
        }
    }
}
