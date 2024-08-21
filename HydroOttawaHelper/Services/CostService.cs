namespace HydroOttawaHelper.Services;

public class CostService
{
    public CostService(RateService rateService, IConfiguration configuration, ILogger<CostService> logger)
    {
        RateService = rateService;
        Logger = logger;

        ArgumentNullException.ThrowIfNull(configuration);

        IsRPP = configuration.GetValue<bool>("RPP");
        HST = configuration.GetValue<decimal>("HST");
        
        ChargeDefinitions? chargeDefinitions = configuration.GetSection("ChargeDefinitions").Get<ChargeDefinitions>();
        if (chargeDefinitions == null)
        {
            logger.LogError("Could not retrieve charge definitions from configuration.");
            throw new ApplicationException("Could not retrieve charge definitions from configuration.");   
        }
        ChargeDefinitions = chargeDefinitions;
    }

    private RateService RateService { get; }
    private bool IsRPP { get; }
    private decimal HST { get; }
    private ChargeDefinitions ChargeDefinitions { get; }
    private ILogger<CostService> Logger { get; }

    public async Task<CostResponse> GetCost(DateTime dateTime)
    {
        RateResponse rateResponse = await RateService.GetRate(dateTime);

        List<Charge> charges = [.. ChargeDefinitions.Charges];
        if (IsRPP)
            charges.Remove(charges.First(charge => charge.Name == "Non-RPP"));
        else
            charges.Remove(charges.First(charge => charge.Name == "RPP"));

        decimal total = rateResponse.Rate + charges.Sum(charge => charge.Value);
        total += total * HST;
        
        return new CostResponse
        { 
            Total = total,
            Rate = rateResponse.Rate,
            Charges = charges,
            HST = HST,
            DateTime = dateTime
        };
    }
}