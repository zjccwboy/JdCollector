using CommodityCollector.Entity;
using CommodityCollector.Models;
using CommodityCollector.Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Updator
{
    public abstract class BaseUpdator<TEntity, TModel> : IUpdator where TEntity : IEntity where TModel : IModel
    {        

        protected IRpository<TEntity> Rpository { get;}

        public BaseUpdator(IRpository<TEntity> rpository)
        {
            this.Rpository = rpository;
        }

        public abstract Task AddOne(TModel model);
    }
}
