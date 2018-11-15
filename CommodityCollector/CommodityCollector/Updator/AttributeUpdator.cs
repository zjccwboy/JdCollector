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
    public class AttributeUpdator : BaseUpdator<ecs_attribute, JdModel>
    {

        public AttributeUpdator(AttributeRpository rpository) : base(rpository) { }

        public override Task AddOne(JdModel model)
        {
            throw new NotImplementedException();
        }
    }
}
