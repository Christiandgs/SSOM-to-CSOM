using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace BookStore.Features.BookStore_Lists
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("3d757a09-508c-4761-83ed-8fd52b0dceff")]
    public class BookStore_ListsEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var web = properties.Feature.Parent as SPWeb;

            if (web.Lists.TryGetList("Book Categories") == null)
            {
                var categoriesListId = web.Lists.Add("Book Categories", "", SPListTemplateType.GenericList);

                if (web.Lists.TryGetList("Books") == null)
                {
                    var booksListId = web.Lists.Add("Books", "", SPListTemplateType.GenericList);
                    var booksList = web.Lists[booksListId];

                    booksList.Fields.Add("Description", SPFieldType.Note, false);

                    var imageFieldName = booksList.Fields.Add("Image", SPFieldType.URL, true);
                    var imageField = booksList.Fields[imageFieldName] as SPFieldUrl;
                    imageField.DisplayFormat = SPUrlFieldFormatType.Image;
                    imageField.Update();

                    var categoryFieldName = booksList.Fields.AddLookup("Category", categoriesListId, true);
                    var categoryField = booksList.Fields[categoryFieldName] as SPFieldLookup;
                    categoryField.LookupField = "Title";
                    categoryField.Update();

                    booksList.DefaultView.ViewFields.Add(imageField);
                    booksList.DefaultView.ViewFields.Add(categoryField);
                    booksList.DefaultView.Update();
                }
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
