using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using shopveeAPI.Common;
using shopveeAPI.DbContext;
using shopveeAPI.Models;
using shopveeAPI.Repository;
using shopveeAPI.Services.Address.Dto.Request;
using shopveeAPI.Services.Address.Dto.Response;
using shopveeAPI.Services.Product;

namespace shopveeAPI.Services.Address
{
    public class AddressServices: GenericRepository<AddressEntity>, IAddressServices
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
                var checkFirstAddress = _shopveeDbContext.Address.Count();
                if (checkFirstAddress == 0 )
                {                
                    AddressEntity addressEntity = _mapper.Map<AddressEntity>(request);
                    addressEntity.IsDefault = true;
                    await _shopveeDbContext.Address.AddAsync(addressEntity);
                    await _shopveeDbContext.SaveChangesAsync();

                    var getResp = _mapper.Map<AddressResponse>(addressEntity);
                    return Ok(getResp, "success");
                }
                else if (checkFirstAddress > MaxAddress)
                {
                    return BadRequest("No more allowed");
                }
                else
                {
                    if (request.IsDefault)
                    {
                        await _shopveeDbContext.Address.Where(x => x.IsDefault).ForEachAsync(x=>x.IsDefault = false);
                        AddressEntity addressEntity = _mapper.Map<AddressEntity>(request);
                        await _shopveeDbContext.Address.AddAsync(addressEntity);
                        await _shopveeDbContext.SaveChangesAsync();
                        var getResp = _mapper.Map<AddressResponse>(addressEntity);
                        return Ok(getResp, "success");
                    }
                    else
                    {
                        AddressEntity addressEntity = _mapper.Map<AddressEntity>(request);
                        await _shopveeDbContext.Address.AddAsync(addressEntity);
                        await _shopveeDbContext.SaveChangesAsync();
                        var getResp = _mapper.Map<AddressResponse>(addressEntity);
                        return Ok(getResp, "success");
                    }
                }
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
                if (id != request.UserId)
                {
                    return NotFound();
                }

                var newEnt = _mapper.Map<AddressEntity>(request);
                _shopveeDbContext.Address.Update(newEnt);
                await _shopveeDbContext.SaveChangesAsync();
                var repo = _mapper.Map<AddressResponse>(newEnt);
                return Ok(repo,"success");
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
                var getT = await _shopveeDbContext.Address.ToListAsync();
                var response = _mapper.Map<List<AddressResponse>>(getT);
                return Ok(response!);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<ServiceResponse> GetAddressAsync(Guid id)
        {
            var address = await _shopveeDbContext.Address!.FindAsync(id);
            var returResp = _mapper.Map<List<AddressResponse>>(address);
            return Ok(returResp!,"success");
        }

        public async Task<ServiceResponse> DeleteAddressAsync(Guid id)
        {
            var deleteAddress = _shopveeDbContext.Address!.SingleOrDefaultAsync(x => x.Id == id).Result;
            if (deleteAddress != null)
            {
                _shopveeDbContext.Address!.Remove(deleteAddress);
            }
            await _shopveeDbContext.SaveChangesAsync();
            return Ok("success");
        }
    }
}
