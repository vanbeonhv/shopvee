using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shopveeAPI.Common;
using shopveeAPI.DbContext;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.PaymentMethod.Dto.Request;
using shopveeAPI.Services.PaymentMethod.Dto.Response;

namespace shopveeAPI.Services.PaymentMethod.Dto;

internal class PaymentMethodService: GenericRepository<PaymentMethodEntity>, IPaymentMethodService
{
    private readonly ShopveeDbContext _shopveeDbContext;
    private readonly IMapper _mapper;
    public PaymentMethodService(ShopveeDbContext shopveeDbContext, IMapper mapper) : base(shopveeDbContext)
    {
        _shopveeDbContext = shopveeDbContext;
        _mapper = mapper;
    }
    public new async Task<ServiceResponse> GetAllAsync()
    {
        try
        {
          var entities =   _shopveeDbContext.PaymentMethodEntity.AsQueryable();
         var res =  _mapper.Map<List<PaymentMethodReponse>>(entities);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public new async Task<ServiceResponse> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await _shopveeDbContext.PaymentMethodEntity.FindAsync(id);
            var res =  _mapper.Map<PaymentMethodReponse>(entity);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public  async Task<ServiceResponse> Add(PaymentMethodCreateRequest request)
    {
        try
        {
            var entity = _mapper.Map<PaymentMethodEntity>(request); 
            await _shopveeDbContext.PaymentMethodEntity.AddAsync(entity);
           await _shopveeDbContext.SaveChangesAsync();
           var res = _mapper.Map<PaymentMethodReponse>(entity);
           return Created(res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public async Task<ServiceResponse> Update(PaymentMethodUpdateRequest request)
    {
        try
        {
            var entity = await _shopveeDbContext.PaymentMethodEntity.FindAsync(request.Id);
            if (entity == null)
            {
                return NotFound("Payment method doesn't exist!");
            }

            entity.Provider = request.Provider!;
            entity.AccountNumber = request.AccountNumber!;
            entity.ExpiryDate = request.ExpiryDate;
            entity.IsDefault = request.IsDefault;
            entity.PaymentType = request.PaymentType;
            
             _shopveeDbContext.PaymentMethodEntity.Update(entity);
            await _shopveeDbContext.SaveChangesAsync();
            var res = _mapper.Map<PaymentMethodReponse>(entity);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public new async Task<ServiceResponse> Delete(Guid id)
    {
        try
        {   var entity = await _shopveeDbContext.PaymentMethodEntity.FindAsync(id);
            if (entity == null)
            {
                return NotFound("Payment method doesn't exist!");
            } 
            _shopveeDbContext.PaymentMethodEntity.Remove(entity);
            await _shopveeDbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}