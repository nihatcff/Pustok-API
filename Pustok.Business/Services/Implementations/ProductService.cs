using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions;
using Pustok.Business.Services.Abstractions;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Abstractions;

namespace Pustok.Business.Services.Implementations;

internal class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(ProductCreateDto dto)
    {
        var products = _mapper.Map<Product>(dto);
        await _repository.AddAsync(products);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException("Product is not found");
        _repository.Delete(product);
        await _repository.SaveChangesAsync();
    }


    public async Task<List<ProductGetDto>> GetAllAsync()
    {
        var products = await _repository.GetAll().Include(x => x.Category).ToListAsync();
        var dtos = _mapper.Map<List<ProductGetDto>>(products);
        return dtos;
    }

    public async Task<ProductGetDto?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product is null)
            throw new NotFoundException("Product is not found");
        var dto = _mapper.Map<ProductGetDto>(product);
        return dto;
    }

    public async Task UpdateAsync(ProductUpdateDto dto)
    {
        var existItem = await _repository.GetByIdAsync(dto.Id);
        if (existItem is null)
            throw new NotFoundException("Producst is not found");

        existItem = _mapper.Map(dto, existItem);

        _repository.Update(existItem);
        await _repository.SaveChangesAsync();
    }
}
