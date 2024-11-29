namespace Route256.Models.GetHistory;

public record GetHistoryResponse(CargoHistoryResponse[] CargoHistory);


public record CargoHistoryResponse(decimal Volume, decimal Price);