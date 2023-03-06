﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BasketCheckoutEvent : IntegrationBaseEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
