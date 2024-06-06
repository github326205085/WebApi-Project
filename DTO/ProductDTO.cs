using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO;

public class ProductDTO
{    
    public string? CategoryName { get; set; }
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public double? Price { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

}

