using Microsoft.SharePoint;
using System;
using System.Linq;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using BookStore.Model;
using System.Web.UI.WebControls;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BookStore.WebParts.CategoriesWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class CategoriesWebPart : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public CategoriesWebPart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var web = SPContext.Current.Web;
            var categoriesList = web.Lists["Book Categories"];
            var items = categoriesList.GetItems(new SPQuery());
            var categories = items.OfType<SPListItem>().Select(it => new Category
                {
                    Id = it.ID,
                    Title = it.Title
                }).ToList();
            RepeaterCategories.DataSource = categories;
            RepeaterCategories.DataBind();
        }

        protected void CategoryLinkBtn_Click(object sender, EventArgs e)
        {
            var web = SPContext.Current.Web;
            var booksList = web.Lists["Books"];

            var linkButton = sender as LinkButton;
            var hiddenId = linkButton.Parent.FindControl("HiddenCategoryId") as HiddenField;
            var categoryId = int.Parse(hiddenId.Value);
            
            var query = new SPQuery();
            query.Query = string.Format(
@"<Where>
    <Eq>
        <FieldRef Name='Category' LookupId='TRUE' />
        <Value Type='Lookup'>{0}</Value>
    </Eq>
</Where>", categoryId);

            var items = booksList.GetItems(query);

            var booksInCategory = items.OfType<SPListItem>().Select(it => new Book
            {
                Id = it.ID,
                Title = it.Title,
                Description = it["Description"] as string,
                ImageUrl = new SPFieldUrlValue(it["Image"] as string).Url,
                CategoryId = categoryId
            }).ToList();

            RepeaterProducts.DataSource = booksInCategory;
            RepeaterProducts.DataBind();


            TxtSelectedCategory.Text = linkButton.Text;
            CategoryDetail.Visible = true;
        }

        protected void BookLinkBtn_Click(object sender, EventArgs e)
        {
            var linkButton = sender as LinkButton;
            var bookTitle = linkButton.Text;

            var client = new WebClient();
            var json = client.DownloadString(string.Format("https://en.wikipedia.org/w/api.php?action=query&generator=search&format=json&gsrprop=snippet&prop=info&inprop=url&gsrsearch={0}", HttpUtility.UrlEncode(bookTitle)));
            var result = JObject.Parse(json);
            var pages = result["query"]["pages"];

            var searchResults = new List<SearchResult>();

            foreach(JProperty page in pages)
            {
                var searchResult = new SearchResult
                {
                    Title = page.Value["title"].ToString(),
                    Url = page.Value["fullurl"].ToString()
                };
                searchResults.Add(searchResult);
            }

            RepeaterWikipediaResults.DataSource = searchResults;
            RepeaterWikipediaResults.DataBind();

            PanelWikipedia.Visible = true;
        }
    }
}
