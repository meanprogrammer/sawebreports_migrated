using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Presenter.Content
{
    public class MainContentContext
    {
        MainContentStrategyBase strategy;

        public MainContentContext(MainContentStrategyBase strategy)
        {
            this.strategy = strategy;
        }

        public object BuildContent(EntityDTO dto)
        {
            return this.strategy.BuildContent(dto);
        }
    }
}
