namespace Route256.ApiModels.V3.GetHistory;

public record GetHistoryResponse(CargoHistoryResponse[] CargoHistory);


public record CargoHistoryResponse(decimal Volume, decimal Weight, decimal Price);