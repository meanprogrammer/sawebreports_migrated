using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter
{
    public class ClearCachePresenter
    {
        IClearCacheView view;

        public ClearCachePresenter(IClearCacheView view)
        {
            this.view = view;
        }

        public void ClearCache()
        {
            CacheHelper.ClearCache();
            this.view.NotifyClear(GlobalStringResource.ClearCacheMessage);
        }
    }
}
