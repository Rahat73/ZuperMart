//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zuper_Mart.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
        public int MessageID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Message1 { get; set; }
        public Nullable<int> CustomerID { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
