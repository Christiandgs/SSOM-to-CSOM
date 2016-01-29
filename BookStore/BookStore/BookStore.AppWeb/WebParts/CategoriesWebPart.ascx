<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesWebPart.ascx.cs" Inherits="BookStore.WebParts.CategoriesWebPart.CategoriesWebPart" %>


<ul>

    <asp:Repeater ID="RepeaterCategories" runat="server">
        <ItemTemplate>
            <li>
                <asp:LinkButton runat="server" ID="CategoryLinkBtn" Text='<%# Eval("Title") %>' OnClick="CategoryLinkBtn_Click" />
                <asp:HiddenField runat="server" ID="HiddenCategoryID" Value='<%# Eval("Id") %>' /> 
            </li>
        </ItemTemplate>
    </asp:Repeater>

</ul> 

<asp:Panel runat="server" ID="CategoryDetail" Visible="False">
    <asp:Label runat="server" ID="TxtSelectedCategory" Font-Size="24"></asp:Label>

    <asp:Repeater ID="RepeaterProducts" runat="server">
        <ItemTemplate>
            <li>
                <asp:LinkButton runat="server" ID="BookLinkBtn" Text='<%# Eval("Title") %>' OnClick="BookLinkBtn_Click" />
                <asp:Image runat="server" ImageUrl='<%# Eval("ImageUrl") %>' />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>

<asp:Panel runat="server" ID="PanelWikipedia" Visible="False">
    <asp:Label runat="server" ID="TxtSearch" Font-Size="24" Text="Wikipedia Results"></asp:Label>

    <asp:Repeater ID="RepeaterWikipediaResults" runat="server">
        <ItemTemplate>
            <li>
                <asp:HyperLink runat="server" text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("Url") %>' Target="_blank" />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>