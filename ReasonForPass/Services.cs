using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ReasonForPass
{
   public class Services
    {
       public static ClientContext Context { get; set; }

        public Services(string webSPOUrl,string userName,SecureString password) {

            // Starting with ClientContext, the constructor requires a URL to the 
            // server running SharePoint. 
            Context = new ClientContext(webSPOUrl);
            Context.Credentials = new SharePointOnlineCredentials(userName, password);
        }

        public ListItemCollection GetListData(string listName) {
            // Assume the web has a list named "Announcements". 
            List list = Context.Web.Lists.GetByTitle(listName);

            // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
            // so that it grabs all list items, regardless of the folder they are in. 
            CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
            ListItemCollection items = list.GetItems(query);
            // Assume there is a list item with ID=1. 
            ListItem listItem = list.GetItemById(1);

            // Write a new value to the Body field of the Announcement item.
            listItem["Title"] = "This is my new value!!";
            listItem.Update();

            // Retrieve all items in the ListItemCollection from List.GetItems(Query). 
            Context.Load(items);
            Context.ExecuteQuery();
            return items;
        }
    }
}
