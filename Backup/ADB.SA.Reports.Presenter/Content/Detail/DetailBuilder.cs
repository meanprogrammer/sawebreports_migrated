using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public class DetailBuilder2
    {
        public static object BuildDetail(EntityDTO dto)
        {
            DetailContext context = null;
            switch (dto.Type)
            {
                case 175:
                    context = new DetailContext(new AcronymDetailStrategy());
                    break;
                case 436:
                    context = new DetailContext(new SectionNameDetailStrategy());
                    break;
                case 156:
                    context = new DetailContext(new RoleDetailStrategy());
                    break;
                case 701:
                    context = new DetailContext(new ReviewerApproverPositionDetailStrategy());
                    break;
                case 603:
                    DetailStrategyBase strategy = ResolveBpmnDetailStrategy(dto);
                    context = new DetailContext(strategy);
                    break;
                case 321:
                    context = new DetailContext(new OrganizationUnitDetailStrategy());
                    break;
                case 663:
                    context = new DetailContext(new PersonDetailStrategy());
                    break;
                case 3:
                    context = new DetailContext(new ProcessDetailStrategy());
                    break;
                case 178:
                    context = new DetailContext(new ControlOwnerDetailStrategy());
                    break;
                case 172:
                    context = new DetailContext(new ControlObjectiveDetailStrategy());
                    break;
                case 440:
                    context = new DetailContext(new FrequencyforControlDetailStrategy());
                    break;
                case 441:
                    context = new DetailContext(new ControlApplicationNameDetailStrategy());
                    break;
                default:
                    context = new DetailContext(null);
                    break;
            }

            return context.BuildDetail(dto);
        }

        private static DetailStrategyBase ResolveBpmnDetailStrategy(EntityDTO dto)
        {
            if (SAWebContext.Request["ctl"] != null && SAWebContext.Request["ctl"] == "true")
            {
                return new BpmnDetailCTLStrategy();
            }
            return new BpmnDetailStrategy();
        }
    }
}
