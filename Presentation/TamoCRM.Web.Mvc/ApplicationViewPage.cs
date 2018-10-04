using System.Web.Mvc;

namespace TamoCRM.Web.Mvc
{
    public abstract class MyBaseViewPage<T> : WebViewPage<T>
    {
        // haihm comment thu
        public string Title
        {
            get { return (string)ViewBag.Title; }
            set { ViewBag.Title = value; }
        }

        public long CurrentUserId
        {
            get { return (long)ViewBag.CurrentUserId; }
        }

    }

    public abstract class MyBaseViewPage : MyBaseViewPage<dynamic>
    {
        protected override void InitializePage()
        {
            SetViewBagDefaultProperties();
            base.InitializePage();
        }

        private void SetViewBagDefaultProperties()
        {
            ViewBag.GlobalProperty = "MyValue";
        }
    }
}