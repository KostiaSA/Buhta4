﻿var bindingHub;
bindingHub = $.connection.bindingHub;

var signalr = {};

signalr.bindedValueChangedListeners = [];

signalr.subscribeModelPropertyChanged = function (modelBindingId, propertyName, callBack) {
  $.connection.hub.start().done(function() {
      $.connection.bindingHub.server.subscribeBindedValueChanged(modelBindingId, propertyName);
      //console.log('subscribeBindedValueChanged', modelBindingId, propertyName);
  });
  signalr.bindedValueChangedListeners.push({ modelBindingId: modelBindingId, propertyName: propertyName, callBack: callBack });
};

$.connection.bindingHub.client.receiveBindedValueChanged = function (modelBindingId, propertyName, newValue) {
    //console.log('receiveBindedValueChanged', modelBindingId, propertyName, newValue);
    for (var i = 0; i < signalr.bindedValueChangedListeners.length; i++) {
        var listener=signalr.bindedValueChangedListeners[i];
        if (listener.modelBindingId == modelBindingId && listener.propertyName == propertyName) {
            listener.callBack(newValue);
            //console.log('receiveBindedValueChanged.callBack', modelBindingId, propertyName, newValue);
        }
    }
};

