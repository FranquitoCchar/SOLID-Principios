using Microsoft.Data.SqlClient;

namespace PaymentService.Repository;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}