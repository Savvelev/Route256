namespace Route256.Models.V1.GetHistory;

public record GetHistoryResponse(CargoHistoryResponse[] CargoHistory);


public record CargoHistoryResponse(decimal Volume, decimal Price);