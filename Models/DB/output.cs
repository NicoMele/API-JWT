using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace APITest_01.Models.DB
{
    public class DetalleOutput
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public double precio { get; set; }
    }
}
