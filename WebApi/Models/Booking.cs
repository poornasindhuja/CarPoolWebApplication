//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int RideId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int UserId { get; set; }
        public System.DateTime BookingDate { get; set; }
        public short Status { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public decimal CostOfBooking { get; set; }
        public int NumberSeatsSelected { get; set; }
    }
}
