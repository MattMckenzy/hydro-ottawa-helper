namespace HydroOttawaHelper.Models;

public class CostResponse
{
    public decimal Total { get; set; }
    public decimal Rate { get; set; }
    public List<Charge> Charges { get; set; } = [];
    public decimal HST { get; set; }
    public DateTime DateTime{ get; set; }
}
