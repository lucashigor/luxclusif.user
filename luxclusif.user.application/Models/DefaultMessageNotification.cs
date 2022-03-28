﻿using luxclusif.user.application.Constants;
using luxclusif.user.application.Exceptions;
using MediatR;

namespace luxclusif.user.application.Models
{
    public class DefaultMessageNotification : INotification
    {
        public DefaultMessageNotification(string eventName, object data)
        {
            EventName = eventName;
            Data = data;

            Validade();
        }

        public string EventName { get; private set; }
        public object Data { get; private set; }

        private void Validade()
        {
            if(Data is null || EventName is null)
            {
                throw new BusinessException(ErrorCodeConstant.NotificationValuesError);
            }
        }

    }
}
