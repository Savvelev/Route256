namespace Route256.ApiModels.V2.GetHistory;

public record GetHistoryResponse(CargoHistoryResponse[] CargoHistory);


public record CargoHistoryResponse(double Volume, double Weight, decimal Price);