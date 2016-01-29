using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;

namespace BookStore.AppWeb.Services
{
    /* In order to debug a RemoteEventReceiver on SharePoint Online, you need to create an Azure Service Bus using ACS authentication:
     * 
     * You need to download Azure PowerShell:
     * https://azure.microsoft.com/es-es/documentation/articles/powershell-install-configure/

     * And then run the following commands:
     
    Import-Module Azure
    Add-AzureAccount
    Get-Azuresubscription
    Select-Azuresubscription <name of the subscription you need>
    New-AzureSBNamespace <name for the service bus> "West Europe" -CreateACSNamespace $true -NamespaceType Messaging

     * During the workshop you'll have an Spenta Service Bus already created and available to use:
     
     Endpoint=sb://spentatraining.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=0KXoQh2QGn4lJGCeS67lLxF5adPJECS9gzikS6SpgYM=
     
     */
    public class AppEventReceiver : IRemoteEventService
    {
        /// <summary>
        /// Handles app events that occur after the app is installed or upgraded, or when app is being uninstalled.
        /// </summary>
        /// <param name="properties">Holds information about the app event.</param>
        /// <returns>Holds information returned from the app event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();

            using (ClientContext clientContext = TokenHelper.CreateAppEventClientContext(properties, useAppWeb: false))
            {
                var web = clientContext.Web;
                
                if (!web.ListExists("Book Categories"))
                {
                    var categoriesList = web.Lists.Add(new ListCreationInformation
                    {
                        Title = "Book Categories",
                        TemplateType = (int)ListTemplateType.GenericList
                    });

                    if (!web.ListExists("Books"))
                    {
                        var booksList = web.Lists.Add(new ListCreationInformation
                        {
                            Title = "Books",
                            TemplateType = (int)ListTemplateType.GenericList
                        });
                        
                        //https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
                        var schemaMultilineTextField = string.Format("<Field Type='Note' Name='{0}' StaticName='{0}' DisplayName='{0}' NumLines='6' RichText='FALSE' Sortable='FALSE' />",
                            "Description");

                        booksList.Fields.AddFieldAsXml(schemaMultilineTextField, true, AddFieldOptions.DefaultValue);

                        var schemaPictureField = string.Format("<Field Type='URL' Name='{0}' StaticName='{0}' DisplayName='{0}' Format='Image'/>",
                            "Image");

                        booksList.Fields.AddFieldAsXml(schemaPictureField, true, AddFieldOptions.DefaultValue);

                        clientContext.Load(categoriesList, l => l.Id);
                        clientContext.ExecuteQueryRetry();

                        var lookupFieldXml = string.Format("<Field DisplayName='{0}' Type='Lookup' />", "Category");
                        var field = booksList.Fields.AddFieldAsXml(lookupFieldXml, true, AddFieldOptions.DefaultValue);
                        var lookupField = clientContext.CastTo<FieldLookup>(field);
                        lookupField.LookupList = categoriesList.Id.ToString();
                        lookupField.LookupField = "Title";
                        field.Update();

                        clientContext.ExecuteQueryRetry();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This method is a required placeholder, but is not used by app events.
        /// </summary>
        /// <param name="properties">Unused.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
        }

    }
}
