using Application.Dto;

namespace Application.Interface
{
    public interface IBranches
    {
        Task<ResultRequestDto<BranchDto>> Create(CreateBranchDto input);
        Task<ResultRequestDto<BranchDto>> Get(int id);
        Task<ResultRequestDto<List<BranchDto>>> GetAll();
        Task<ResultRequestDto<BranchDto>> Update(UpdateBranchDto input);
        Task<ResultRequestDto<bool>> Delete(int id);

    }
}
