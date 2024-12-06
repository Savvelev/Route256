namespace Route256.ApiModels.V3.GetHistory;

public record GetHistoryResponse(CargoHistoryResponse[] CargoHistory);


public record CargoHistoryResponse(double Volume, double Weight, decimal Price, int Distance);