//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Waste_Tracker
{
    using System;
    using System.Collections.Generic;
    
    public partial class WasteTrackerDB
    {
        public int PID { get; set; }
        public int StationId { get; set; }
        public string MenuItem { get; set; }
        public Nullable<decimal> LeftOver { get; set; }
        public Nullable<decimal> Par { get; set; }
        public string UoM { get; set; }
    }
}
