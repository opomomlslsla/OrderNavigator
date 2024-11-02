using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class FilterResultRepository(Context context) : BaseRepository<FilterResult>(context);