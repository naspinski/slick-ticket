<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Administration - User Groups" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="permissions.aspx.cs" Inherits="admin_user_groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="adminBody" Runat="Server">

<asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
    
    <h4 class="header">
        <span class="title_header">Permissions</span>
        <asp:Button ID="btnAddGroup" runat="server" CssClass="button smaller" Text="Add Group" />&nbsp;
        <asp:Button ID="btnResetADxml" runat="server" CssClass="button smaller" Text="Refresh AD" onclick="btnResetADxml_Click"
             OnClientClick="return confirm('Depending on your network settings, this often is required to be run from the web server and with an elevated account as it needs access to Active Directory.  Proceed?')"  />
        <span class="smaller"><asp:Label ID="lblReport" runat="server" /></span>
    </h4>
    <div style="text-align:left;" class="header">
        Here you can set what access levels your active directory groups will have for write permissions; highest level of access takes precedence.
    </div>
    <div class="divider"></div>
    
    <fieldset class="inner_color">
        
        <asp:ValidationSummary ID="valSumEdit" runat="server" ValidationGroup="edit" CssClass="error_area" />
    
        <asp:GridView ID="gvADGroups" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ldsADGroups"
            CssClass="list"  AlternatingRowStyle-BackColor="#EFF5FF" AllowSorting="True" GridLines="None"
            EmptyDataText="&lt;h5&gt;No one has any access!&lt;/h5&gt;">
            <Columns>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="110px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Remove"
                            OnClientClick="return confirm('Are you sure you want to remove this group?');" />     
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="True"  ValidationGroup="edit" CommandName="Update" Text="Update" />&nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <ItemStyle Width="110px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AD Group <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>The Active Directory Group that will be associated with an access level in this system.</q></span></a>" 
                    SortExpression="ad_group">
                    <ItemTemplate>
                        <asp:Label ID="lblADGroup" runat="server" Text='<%# Bind("ad_group") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtADGroup" Width="100%" runat="server" Text='<%# Bind("ad_group") %>' />
                        <asp:RequiredFieldValidator ID="rfvEditDisplay" runat="server" ValidationGroup="edit" ControlToValidate="txtADGroup" ErrorMessage="AD Group Required" Display="None" />
                        <ajax:AutoCompleteExtender runat="server" ID="aceEditDisplay" TargetControlID="txtADGroup" ServiceMethod="getGroups" ServicePath="~/web_services/services.asmx" MinimumPrefixLength="1" 
                            EnableCaching="true" CompletionListItemCssClass="autoSuggest" CompletionListHighlightedItemCssClass="autoSuggest autoSuggestSelect"  CompletionInterval="1000" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="120px" 
                    HeaderText="Access Level <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>The access level users in this AD Group will have.</q></span></a>" 
                    SortExpression="security_level">
                    <EditItemTemplate>
                        <div style="text-align:center;">
                            <asp:DropDownList ID="ddlEditSecurityLevel" runat="server" SelectedValue='<%# Bind("security_level") %>'  
                             DataSourceID="ldsAccessLevels" DataValueField="id" DataTextField="id" />
                        </div>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <div style="text-align:center;"><asp:Label ID="Label1" runat="server" Text='<%# Bind("security_level") %>'></asp:Label></div>
                    </ItemTemplate>
                    <HeaderStyle Width="120px" />
                </asp:TemplateField>
            </Columns>

            <AlternatingRowStyle BackColor="#EFF5FF"></AlternatingRowStyle>
        </asp:GridView>
        
        <asp:LinqDataSource ID="ldsAccessLevels" runat="server" 
            ContextTypeName="dbDataContext" EnableDelete="True" EnableUpdate="True" 
            OrderBy="id" TableName="security_levels" />
        
        <asp:LinqDataSource ID="ldsADGroups" runat="server" 
            ContextTypeName="dbDataContext" EnableDelete="True" EnableUpdate="True" 
            OrderBy="security_level, ad_group" TableName="user_groups" />
        
        
    </fieldset>
    
    <ajax:ModalPopupExtender ID="mpeNewGroup" runat="server" TargetControlID="btnAddGroup" BackgroundCssClass="modal_background"
        PopupControlID="pnlNew" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlNew" runat="server"  CssClass="modal_popup" style="display:none;" DefaultButton="btnNewSubmit">
        
        <div class="small_container border_color">
            <fieldset class="inner_color">
                <div>
                    <h3>
                        <span class="title_header">
                            AD Group 
                            <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>The Active Directory Group that will be associated with an access level in this system.</q></span></a>    
                        </span>
                        <asp:RequiredFieldValidator ID="rfvDisplay" runat="server" ControlToValidate="txtADGroup" ErrorMessage="Group Required" CssClass="error" ValidationGroup="new" />
                    </h3>
                    <asp:TextBox ID="txtADGroup" Width="100%" runat="server" />
                    <ajax:AutoCompleteExtender runat="server" ID="aceEditDisplay" TargetControlID="txtADGroup" ServiceMethod="getGroups" ServicePath="~/web_services/services.asmx" MinimumPrefixLength="1" 
                        EnableCaching="true" CompletionListItemCssClass="autoSuggest" CompletionListHighlightedItemCssClass="autoSuggest autoSuggestSelect"  CompletionInterval="1000" />
                </div>
                <div>
                    <h3>
                        <span class="title_header">
                            Access Level
                            <%--<a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>The access level users in this AD Group will have.</q></span></a>    --%>
                        </span>&nbsp;</h3>
                    <asp:DropDownList ID="ddlSecurityLevel" runat="server"   Width="100%" />
                </div>
                <br />
                <div style="text-align:center;">
                    <asp:Button ID="btnNewSubmit"  runat="server" Text="Save Group" CssClass="button" ValidationGroup="new" onclick="btnNewSubmit_Click" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" />
                </div>
            </fieldset>
        </div>
        
    </asp:Panel>
    
    </ContentTemplate>
</asp:UpdatePanel>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="adminSidebar" Runat="Server">
</asp:Content>

