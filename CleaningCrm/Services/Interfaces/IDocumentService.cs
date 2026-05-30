using CleaningCrm.Entities;

namespace CleaningCrm.Services.Interfaces;

public interface IDocumentService
{
    byte[] GenerateAct(Order order);
    byte[] GenerateInvoice(Order order);
}