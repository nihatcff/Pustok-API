using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions;
using Pustok.Business.Services.Abstractions;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    internal class CategoryService(ICategoryRepository _repository, IMapper _mapper) : ICategoryService
    {
        public async Task<ResultDto> CreateAsync(CategoryCreateDto dto)
        {
            var isExistCategory = await _repository.AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower());

            if (isExistCategory)
                throw new AlreadyExistException("Category with such name already exists");

            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            return new ResultDto()
            {
                IsSucced = true,
                Message = "Category Created Succesfully"
            };
        }

        public async Task<ResultDto> DeleteAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null)
                throw new NotFoundException("Category is not found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();

            return new ResultDto()
            {
                IsSucced = true,
                Message = "Category Deleted Succesfully"
            };
        }

        public async Task<ResultDto<List<CategoryGetDto>>> GetAllAsync()
        {
            var categories = await _repository.GetAll().Include(x => x.Products).ToListAsync();

            var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

            return new() { Data = dtos };
        }

        public async Task<ResultDto<CategoryGetDto>> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null)
                throw new NotFoundException("Category is not found");

            var dto = _mapper.Map<CategoryGetDto>(category);
            return new ResultDto<CategoryGetDto>()
            {
                IsSucced = true,
                Message = "Category Retrieved Succesfully",
                Data = dto
            };

        }

        public async Task<ResultDto> UpdateAsync(CategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(dto.Id);

            if (category is null)
                throw new NotFoundException("Category is not found");

            var isExistCategory = await _repository.AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower());
            if (isExistCategory)
                throw new AlreadyExistException("Category with such name already exists");

            category = _mapper.Map<CategoryUpdateDto, Category>(dto, category);

            _repository.Update(category);
            await _repository.SaveChangesAsync();

            return new ResultDto()
            {
                IsSucced = true,
                Message = "Category Updated Succesfully"
            };
        }
    }
}
