using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shopveeAPI.Common;
using shopveeAPI.DbContext;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.Address.Dto.Request;
using shopveeAPI.Services.Address.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopveeAPI.Services.Address
{
    public class AddressServices : GenericRepository<AddressEntity>, IAddressServices
    {
        private readonly ShopveeDbContext _shopveeDbContext;
        private readonly IMapper _mapper;

        private const int MaxAddress = 10;

        public AddressServices(ShopveeDbContext shopveeDbContext, IMapper mapper) : base(shopveeDbContext)
        {
            _shopveeDbContext = shopveeDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> Add(AddressRequest request)
        {
            try
            {
                var listAddressCount = await _shopveeDbContext.Address.CountAsync();

                if (listAddressCount >= MaxAddress)
                {
                    return BadRequest("No more addresses allowed.");
                }

                AddressEntity addressEntity = _mapper.Map<AddressEntity>(request);
                addressEntity.IsDefault = listAddressCount == 0 || request.IsDefault;

                if (addressEntity.IsDefault)
                {
                    if (listAddressCount>0)
                    {
                        await _shopveeDbContext.Address.Where(x => x.IsDefault).ForEachAsync(x => x.IsDefault = false);
                    }
                }

                await _shopveeDbContext.Address.AddAsync(addressEntity);
                await _shopveeDbContext.SaveChangesAsync();
                var response = _mapper.Map<AddressResponse>(addressEntity);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<ServiceResponse> Update(Guid id, AddressRequest request)
        {
            try
            {
                var checkEntity = await _shopveeDbContext.Address.FindAsync(id);
                if (checkEntity == null)
                {
                    return NotFound();
                }

                var entity = _mapper.Map<AddressEntity>(request);
                _shopveeDbContext.Address.Update(entity);
                await _shopveeDbContext.SaveChangesAsync();

                var response = _mapper.Map<AddressResponse>(entity);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<ServiceResponse> GetAllAddressAsync()
        {
            try
            {
                var entities = await _shopveeDbContext.Address.ToListAsync();
                var response = _mapper.Map<List<AddressResponse>>(entities);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<ServiceResponse> GetAddressAsync(Guid id)
        {
            var address = await _shopveeDbContext.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<AddressResponse>(address);
            return Ok(response);
        }

        public async Task<ServiceResponse> DeleteAddressAsync(Guid id)
        {
            var deleteAddress = await _shopveeDbContext.Address.FindAsync(id);

            if (deleteAddress != null)
            {
                _shopveeDbContext.Address.Remove(deleteAddress);
                await _shopveeDbContext.SaveChangesAsync();
                return Ok(deleteAddress);
            }

            return NotFound();
        }
    }
}
