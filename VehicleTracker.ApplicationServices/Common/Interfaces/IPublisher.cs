﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ApplicationServices.Common.Interfaces
{
    public interface IPublisher
    {
        Task Publish<T>(T message);
    }
}
