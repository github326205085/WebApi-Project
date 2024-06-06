using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO;

public class OrderGetDTO
{
    //public short OrderId { get; set; }

    public double? OrderSum { get; set; }
    public int? UserId { get; set; }
    public ICollection<OrderItemDTO> orderItemDTOs { get; set; } = new List<OrderItemDTO>(); 

}

public class OrderReturnDTO
{
    public short OrderId { get; set; }

    public double? OrderSum { get; set; }
    public int? UserId { get; set; }
}