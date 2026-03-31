using System;
using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementApp.Entity
{
    public class QuantityInputDTO
    {
        [Required]
        public QuantityDTO ThisQuantityDTO { get; set; } = new QuantityDTO();
        
        public QuantityDTO? ThatQuantityDTO { get; set; }
    }
}
