using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Presenter.Content
{
    public class DetailContext
    {
        DetailStrategyBase strategy;

        public DetailContext(DetailStrategyBase strategy)
        {
            this.strategy = strategy;
        }

        public object BuildDetail(EntityDTO dto)
        {
            return this.strategy.BuildDetail(dto);
        }
    }
}
