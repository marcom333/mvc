using Application.Entities;
using System.Data;

namespace Application.Interface.Service;

public interface IDapperService
{
    public IDbConnection GetConnection();
}