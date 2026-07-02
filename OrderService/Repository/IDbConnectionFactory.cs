using Microsoft.Data.SqlClient;

namespace OrderService.Repository;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}