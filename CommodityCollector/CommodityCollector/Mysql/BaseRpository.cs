using CommodityCollector.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Mysql
{
    public abstract class BaseRpository<TEntity> : IDisposable where TEntity : IEntity
    {
        protected IDbConnection sqlConnection { get; } = DapperService.MySqlConnection();
        public abstract Task InsertAsync(TEntity entity);

        public void Dispose()
        {
            sqlConnection.Dispose();
        }
    }
}
