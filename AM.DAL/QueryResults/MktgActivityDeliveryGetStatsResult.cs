using System;
using System.Collections.Generic;

namespace AM.DAL.QueryResults
{
    public class MktgActivityDeliveryGetStatsResult
    {
        public int TotalBounces { get; set; }
        public int HardBounces { get; set; }
        public int SoftBounces { get; set; }
        public int EmailsDelivered { get; set; }
        public int EmailsOpenTotal { get; set; }
        public int EmailsOpenUnique { get; set; }
        public int EmailsClickedTotal { get; set; }
        public int EmailsClickedUnique { get; set; }
        public int EmailsForwardedTotal { get; set; }
        public int EmailsForwardedUnique { get; set; }
        public int UnsuscribesTotal { get; set; }
        public int UnsuscribesUnique { get; set; }
        public decimal DeliveryRate { get; set; }
        public decimal OpenRate { get; set; }
        public bool Reviewed { get; set; }
    }

}
