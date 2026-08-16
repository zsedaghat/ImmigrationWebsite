using ImmigrationWebsite.Web.Models;

namespace ImmigrationWebsite.Web.Services.Interfaces;

public interface IConsultationRequestManager
{
    Task<List<ConsultationRequest>> GetAllAsync();
    Task<ConsultationRequest?> GetByIdAsync(int id);
    Task AddAsync(ConsultationRequest request);
    Task DeleteAsync(int id);
}