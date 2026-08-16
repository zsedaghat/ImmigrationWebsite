using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.Pagination;

namespace ImmigrationWebsite.Web.Services.Interfaces;

public interface IConsultationRequestManager
{
    Task<PagedResult<ConsultationRequest>> GetPagedAsync(
      int pageNumber,
      int pageSize);
    Task<ConsultationRequest?> GetByIdAsync(int id);
    Task AddAsync(ConsultationRequest request);
    Task DeleteAsync(int id);
    Task UpdateAsync(ConsultationRequest request);
}