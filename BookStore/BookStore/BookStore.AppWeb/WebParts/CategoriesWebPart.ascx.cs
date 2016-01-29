using Microsoft.SharePoint;
using System;
using System.Linq;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using BookStore.Data.Repositories;
using BookStore.Application.Services;
using BookStore.Unity;
using Microsoft.Practices.Unity;

namespace BookStore.WebParts.CategoriesWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class CategoriesWebPart : UnityWebPart
    {
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly IBooksRepository _booksRepo;
        private readonly IExternalSearchService _externalSearchService;

        public CategoriesWebPart()
        {
            _categoriesRepo = WebPartUnityContainer.Current.Container.Resolve<ICategoriesRepository>();
            _booksRepo = WebPartUnityContainer.Current.Container.Resolve<IBooksRepository>();
            _externalSearchService = WebPartUnityContainer.Current.Container.Resolve<IExternalSearchService>();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var categories = _categoriesRepo.GetAll();
            RepeaterCategories.DataSource = categories;
            RepeaterCategories.DataBind();
        }

        protected void CategoryLinkBtn_Click(object sender, EventArgs e)
        {
            var linkButton = sender as LinkButton;
            var hiddenId = linkButton.Parent.FindControl("HiddenCategoryId") as HiddenField;
            var categoryId = int.Parse(hiddenId.Value);

            var booksInCategory = _booksRepo.GetByCategory(categoryId);
           
            RepeaterProducts.DataSource = booksInCategory;
            RepeaterProducts.DataBind();

            TxtSelectedCategory.Text = linkButton.Text;
            CategoryDetail.Visible = true;
        }

        protected void BookLinkBtn_Click(object sender, EventArgs e)
        {
            var linkButton = sender as LinkButton;
            var bookTitle = linkButton.Text;

            var searchResults = _externalSearchService.Search(bookTitle);

            RepeaterWikipediaResults.DataSource = searchResults;
            RepeaterWikipediaResults.DataBind();

            PanelWikipedia.Visible = true;
        }
    }
}
