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
    public abstract class BaseUpdator<TRpository, TEntity, TModel> : IUpdator 
        where TEntity : IEntity where TModel : IModel where TRpository : IRpository
    {
        public TRpository Rpository { get; }

        public BaseUpdator(TRpository rpository)
        {
            this.Rpository = rpository;
        }
    }
}
