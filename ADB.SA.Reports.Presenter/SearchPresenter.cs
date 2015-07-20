using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Presenter
{
    public class SearchPresenter
    {
        ISearchView view;
        EntityData entityData;

        public SearchPresenter(ISearchView view)
        {
            this.view = view;
            entityData = new EntityData();
        }

        public void DoSearch(string filter)
        {
            List<SearchResultDTO> results = entityData.SearchEntity(filter);
            view.RenderSearchResult(results);
        }
    }
}
