using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class DistrictRepository(Context context) : BaseRepository<District>(context)
{
}
