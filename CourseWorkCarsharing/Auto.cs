//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseWorkCarsharing
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auto
    {
        public int Id_auto { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public int Year_of_release { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Type { get; set; }
        public string Mileage { get; set; }
        public string Fuel_type { get; set; }
        public string Transmission_box { get; set; }
        public string Status { get; set; }
        public System.DateTime Date_added { get; set; }
        public string Insurance { get; set; }
        public Nullable<System.DateTime> Date_of_last_service { get; set; }
    }
}
