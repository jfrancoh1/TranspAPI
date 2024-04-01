using Application.Dto;
using Application.Interface;
using AutoMapper;
using Domain;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Main
{
    public class Branches : IBranches
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public Branches(IUserRepository userRepository, IBranchRepository branchRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<ResultRequestDto<BranchDto>> Create(CreateBranchDto input)
        {
            Branch branch = _mapper.Map<Branch>(input);
            BranchDto branchDto = _mapper.Map<BranchDto>(branch);
            try
            {
                User user = _userRepository.Get(branch.UserId);

                if (user == null)
                {
                    return new ResultRequestDto<BranchDto> { Data = null, IsSuccess = true, Message = "User Not Found.", Response = "404" };
                }

                branch.CreateDate = DateTime.Now;
                branchDto.CreateDate = branch.CreateDate;
                branchDto.Id = _branchRepository.Create(branch);
                return new ResultRequestDto<BranchDto> { Data = branchDto, IsSuccess = true, Message = "Branch Created.", Response = "201" };
            }
            catch (Exception)
            {
                return new ResultRequestDto<BranchDto> { Data = branchDto, IsSuccess = true, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<BranchDto>> Get(int id)
        {
            try
            {
                Branch? branch = _branchRepository.Get(id);
                BranchDto branchDto = _mapper.Map<BranchDto>(branch);

                if (branch != null)
                {
                    return new ResultRequestDto<BranchDto> { Data = branchDto, IsSuccess = true, Message = "OK", Response = "200" };
                }
                else
                {
                    return new ResultRequestDto<BranchDto> { Data = new BranchDto(), IsSuccess = false, Message = "Branch Not Found.", Response = "404" };
                }
            }
            catch (Exception)
            {
                return new ResultRequestDto<BranchDto> { Data = new BranchDto(), IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<List<BranchDto>>> GetAll()
        {
            try
            {
                List<Branch> branches = _branchRepository.GetAll();
                List<BranchDto> lsbranchesDto = new();

                foreach (Branch branch in branches)
                {
                    var branchDto = _mapper.Map<BranchDto>(branch);
                    lsbranchesDto.Add(branchDto);
                }

                return new ResultRequestDto<List<BranchDto>> { Data = lsbranchesDto, IsSuccess = true, Message = "OK", Response = "200" };
            }
            catch (Exception)
            {
                return new ResultRequestDto<List<BranchDto>> { Data = new List<BranchDto>(), IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<List<BranchDto>>> GetByUser(int userId)
        {
            try
            {
                List<Branch> branches = _branchRepository.GetByUser(userId);
                List<BranchDto> lsBranchesDto = new();

                if (branches.Count > 0)
                {
                    foreach (Branch branch in branches)
                    {
                        BranchDto branchDto = _mapper.Map<BranchDto>(branch);
                        lsBranchesDto.Add(branchDto);
                    }
                    return new ResultRequestDto<List<BranchDto>> { Data = lsBranchesDto, IsSuccess = true, Message = "OK", Response = "200" };
                }
                return new ResultRequestDto<List<BranchDto>> { Data = new List<BranchDto>(), IsSuccess = false, Message = "Branch Not Found.", Response = "404" };

            }
            catch (Exception)
            {
                return new ResultRequestDto<List<BranchDto>> { Data = new List<BranchDto>(), IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<BranchDto>> Update(UpdateBranchDto input)
        {
            BranchDto branchDto = _mapper.Map<BranchDto>(input);
            try
            {
                Branch? branch = _branchRepository.Get(input.Id);
                if (branch == null)
                {
                    return new ResultRequestDto<BranchDto> { Data = branchDto, IsSuccess = false, Message = "Branch Not Found.", Response = "404" };
                }
                if (!string.IsNullOrEmpty(input.Name) && branch.Name != input.Name)
                    branch.Name = input.Name;
                if (!string.IsNullOrEmpty(input.Address) && branch.Address != input.Address)
                    branch.Address = input.Address;
                if (!string.IsNullOrEmpty(input.PhoneNumber) && branch.PhoneNumber != input.PhoneNumber)
                    branch.PhoneNumber = input.PhoneNumber;
                if (!string.IsNullOrEmpty(input.Email) && branch.Email != input.Email)
                    branch.Email = input.Email;
                if (!string.IsNullOrEmpty(input.Schedule) && branch.Schedule != input.Schedule)
                    branch.Schedule = input.Schedule;
                if (branch.UserId != input.UserId)
                    branch.UserId = input.UserId;
                if (branch.Exchange != input.Exchange)
                    branch.Exchange = input.Exchange;

                BranchDto branchDtoOk = _mapper.Map<BranchDto>(branch);
                branch.UpdateDate = DateTime.Now;
                branchDto.UpdateDate = branch.UpdateDate;
                _branchRepository.Update(branch);

                return new ResultRequestDto<BranchDto> { Data = branchDtoOk, IsSuccess = true, Message = "Branch Updated", Response = "201" };
            }
            catch (Exception)
            {
                return new ResultRequestDto<BranchDto> { Data = branchDto, IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }

        public async Task<ResultRequestDto<bool>> Delete(int id)
        {
            try
            {
                Branch? branch = _branchRepository.Get(id);
                BranchDto branchDto = _mapper.Map<BranchDto>(branch);

                if (branch != null)
                {
                    _branchRepository.Delete(branch);
                    return new ResultRequestDto<bool> { Data = true, IsSuccess = true, Message = "OK", Response = "200" };
                }
                else
                {
                    return new ResultRequestDto<bool> { Data = false, IsSuccess = false, Message = "Branch Not Found.", Response = "404" };
                }
            }
            catch (Exception)
            {
                return new ResultRequestDto<bool> { Data = false, IsSuccess = false, Message = "Internal Server Error! Try again later.", Response = "500" };
            }
        }
    }
}
