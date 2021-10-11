using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.ViewModel;

namespace VehicleTracker.ApplicationServices.Extensions
{
    public static class VehicleStateExtensions
    {
        public static List<T> Search<T>(this List<T> models, Func<List<T>> query)
        {
            return query();
        }
    }
}
