using MediatR;

namespace API.Movements.Commands.ReceiptCommand;

public class ReceiptCommand : IRequest
{
    public string Invoice { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<ReceiptItem> ReceiptItems { get; set; }
}