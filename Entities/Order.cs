using System.Text.Json.Serialization;

namespace Entities;

public partial class Order
{
    public short OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public double? OrderSum { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }



}
