//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APTEventAssignment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventBooking
    {
        public EventBooking()
        {
            this.EventBookingSeat = new HashSet<EventBookingSeat>();
        }
    
        public int EventBooking_ID { get; set; }
        public Nullable<System.DateTime> EventBooking_Date { get; set; }
        public string EventBooking_UserID { get; set; }
        public Nullable<int> EventBooking_EventPerformanceID { get; set; }
        public bool EventBooking_Deleted { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual EventPerformance EventPerformance { get; set; }
        public virtual ICollection<EventBookingSeat> EventBookingSeat { get; set; }
    }
}
