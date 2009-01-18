<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Administration - Groups" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="groups.aspx.cs" Inherits="admin_units" %>

<asp:Content ID="Content1" ContentPlaceHolderID="adminHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="adminBody" Runat="Server">
    
    
    <h4 class="header">
        <span class="title_header">Groups</span>
        <span class="clear"></span>
        <span class="smaller"><asp:Label ID="lblReport" runat="server" /></span>
    </h4>
    <div style="text-align:left;" class="header">
        Here you can set the groups and sub-groups that users are able to set within their profiles.
    </div>
    <div class="divider"></div>
    <asp:UpdatePanel ID="upUnit" runat="server">
        <ContentTemplate>
            <fieldset class="inner_color">
                <h4>
                    <span class="title_header">Groups</span>
                    <span class="smaller"><asp:Button ID="btnAddUnit" CssClass="smaller button" runat="server" Text="Add Group" /></span>
                    <span class="clear" >
                    <span class="smaller"><asp:Label ID="lblUnitReport" runat="server" /></span>
                    </span>
                </h4>
                <asp:ValidationSummary ID="valUnit" runat="server" CssClass="error_area border_color" ValidationGroup="unitEdit" />
                <asp:GridView ID="gvUnits" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="id" DataSourceID="ldsUnits" AllowPaging="True" 
                    AllowSorting="True" CssClass="list" GridLines="None"
                   OnRowUpdated= "gvUnits_RowUpdated"
                    EmptyDataText="No units set; users can not complete their profiles or use the system!" 
                    onrowdeleted="gvUnits_RowDeleted" >
                    <Columns>
                        <asp:TemplateField ShowHeader="False"  ItemStyle-Width="110px" >
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this group?');" CommandName="Delete" Text="Delete" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" ValidationGroup="unitEdit" />
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group" SortExpression="unit_name">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("unit_name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="txtUnit" ErrorMessage="Unit required" Display="None" ValidationGroup="unitEdit" />
                                <asp:TextBox ID="txtUnit" runat="server" Width="100%" Text='<%# Bind("unit_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                <asp:LinqDataSource ID="ldsUnits" runat="server" ContextTypeName="dbDataContext" EnableDelete="True" EnableUpdate="True" OrderBy="unit_name" TableName="units" />
                <asp:LinqDataSource ID="ldsAccessLevels" runat="server" ContextTypeName="dbDataContext" Select="new (id)" TableName="security_levels" />
                
                
                <ajax:ModalPopupExtender ID="mpeNewUnit" runat="server" TargetControlID="btnAddUnit" BackgroundCssClass="modal_background"
                    PopupControlID="pnlNewUnit" CancelControlID="btnCancelUnit" />

                <asp:Panel ID="pnlNewUnit" runat="server"  CssClass="modal_popup" style="display:none;" DefaultButton="btnNewUnitSubmit" >
                    
                    <div class="small_container border_color">
                        <fieldset class="inner_color">
                            <div>
                                <h3>
                                    <span class="title_header">Group</span>
                                    <asp:RequiredFieldValidator ID="rfvDisplay" runat="server" ControlToValidate="txtNewUnit" ErrorMessage="Required" CssClass="error" ValidationGroup="newUnit" />
                                </h3>
                                <asp:TextBox ID="txtNewUnit" Width="100%" runat="server" />
                            </div>
                            <br />
                            <div style="text-align:center;">
                                <asp:Button ID="btnNewUnitSubmit"  runat="server" Text="Save Group" 
                                    CssClass="button" ValidationGroup="newUnit" onclick="btnNewUnitSubmit_Click"  />&nbsp;
                                <asp:Button ID="btnCancelUnit" runat="server" Text="Cancel" CssClass="button" />
                            </div>
                        </fieldset>
                    </div>
                    
                </asp:Panel>
            </fieldset>
            <div class="divider"></div>
            
            <fieldset class="inner_color">
        
            
                <h4>
                    <span class="title_header">Sub-Groups</span>
                    <span class="smaller">
                        <asp:DropDownList ID="ddlUnitSelected" runat="server" CssClass="smaller" 
                        DataSourceID="ldsUnits" DataTextField="unit_name" DataValueField="id" 
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlUnitSelected_SelectedIndexChanged" />
                        &nbsp;
                        <asp:Button ID="btnAddSubUnit" CssClass="smaller button" runat="server" Text="Add Sub-Group" />
                    </span>
                    <span class="clear" ></span>
                    <span class="smaller"><asp:Label ID="lblSubUnitReport" runat="server" /></span>
                </h4>
                <asp:ValidationSummary ID="valSubUnitEdit" runat="server" CssClass="error_area border_color" ValidationGroup="subUnitEdit" />


                <asp:GridView ID="gvSubUnits" runat="server" DataSourceID="ldsSubUnits" DataKeyNames="id"  
                    AllowPaging="True" AllowSorting="True" CssClass="list" GridLines="None" AutoGenerateColumns="False"
                    EmptyDataText="No sub-groups set; users can not complete their profiles or use the system!" 
                                    onrowdeleted="gvSubUnits_RowDeleted" >
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            
                            <ItemTemplate>
                                <div class="gv_buttons">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this sub-group?')" />
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="gv_buttons">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"  CommandName="Update" Text="Update" ValidationGroup="subUnitEdit" />
                                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                </div>
                            </EditItemTemplate>
                            <ItemStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub-Group" SortExpression="sub_unit_name">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("sub_unit_name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:RequiredFieldValidator ID="rfvSubSunitEdit" ControlToValidate="txtSubUnitEdit" ValidationGroup="subUnitEdit" ErrorMessage="Sub-Group required" Display="None" runat="server" />
                                <asp:TextBox ID="txtSubUnitEdit" runat="server" Text='<%# Bind("sub_unit_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mail To <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>The email address that will be notified when a ticket is assigned to this group.</q></span></a>" SortExpression="mailto">
                            <ItemTemplate>
                                <a href="mailto:<%# Eval("mailto") %>">
                                    <%# utils.trimForSideBar(Eval("mailto").ToString(), 21) %>
                                </a>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:RequiredFieldValidator ID="rfvMailto" runat="server" ControlToValidate="txtMailto" ValidationGroup="subUnitEdit" ErrorMessage="Mail to email address required" CssClass="error" Display="None" />
                                <asp:RegularExpressionValidator ID="regEditMailTo" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtMailto" ForeColor="" ValidationGroup="subUnitEdit" 
                                    ValidationExpression="^[0-9]*[a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([a-zA-Z][-\w\.]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9}$" Display="None" CssClass="error" />
                                <asp:TextBox ID="txtMailto" runat="server" Text='<%# Bind("mailto") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField 
                            HeaderText="Req Access <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>This is the required access level a user must have in order to modify a ticket assigned to this group.</q></span></a>" 
                            SortExpression="security_level">
                            <EditItemTemplate>
                                <div class="gv_buttons" style="text-align:center;">
                                    <asp:DropDownList ID="ddlEditSecurityLevel" runat="server" SelectedValue='<%# Bind("access_level") %>'  
                                     DataSourceID="ldsAccessLevels" DataValueField="id" DataTextField="id" />
                                </div>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <div class="gv_buttons" style="text-align:center;"><asp:Label ID="Label1" runat="server" Text='<%# Bind("access_level") %>'></asp:Label></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:LinqDataSource ID="ldsSubUnits" runat="server" 
                    ContextTypeName="dbDataContext" EnableDelete="True" EnableUpdate="True" 
                    OrderBy="sub_unit_name" TableName="sub_units" Where="unit_ref == @unit_ref">
                    <WhereParameters>
                        <asp:ControlParameter ControlID="ddlUnitSelected" DefaultValue="0" 
                            Name="unit_ref" PropertyName="SelectedValue" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>

            </fieldset>
                        
            <ajax:ModalPopupExtender ID="mpeSubUnit" runat="server" TargetControlID="btnAddSubUnit" BackgroundCssClass="modal_background"
                PopupControlID="pnlAddSubUnit" CancelControlID="btnCancelSubUnit" />
            
            <asp:Panel ID="pnlAddSubUnit" runat="server"  CssClass="modal_popup" style="display:none;" DefaultButton="btnNewSubUnitSubmit" >
                
                <div class="large_container border_color">
                
                    <fieldset class="inner_color">
                        
                        <table class="by2">
                            <tr>
                                <td style="width:50%;">
                                    <h3><span class="title_header">Group</span>&nbsp;</h3>
                                    <asp:DropDownList ID="ddlNewSubUnit" runat="server" cssClass="half_table" DataSourceID="ldsUnits" DataTextField="unit_name" DataValueField="id" />
                                </td>
                                <td style="width:50%;">
                                    <h3>
                                        <span class="title_header">Sub-Group</span>
                                        <asp:RequiredFieldValidator ID="rfvNewSubUnit" runat="server" ControlToValidate="txtNewSubUnit" ErrorMessage="Required" CssClass="error" ValidationGroup="new" />
                                    </h3>
                                    <asp:TextBox ID="txtNewSubUnit" cssClass="half_table" runat="server" />
                                </td>
                             </tr>
                             <tr>
                                <td>
                                    <h3><span class="title_header">
                                        Required Access Level
                                        <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>This is the required access level a user must have in order to be able to join, assign tickets to, or modify a ticket assigned to this group.</q></span></a>
                                    </span>&nbsp;</h3>
                                    <asp:DropDownList ID="ddlSecurityLevel" runat="server" cssClass="half_table" />
                                </td>
                                <td>
                                    <h3>
                                        <span class="title_header">
                                            Mail To
                                            <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>The email address that will be notified when a ticket is assigned to this group.</q></span></a>
                                        </span>&nbsp;
                                        <asp:RequiredFieldValidator ID="rfvMailto" runat="server" ControlToValidate="txtMailto" ValidationGroup="new" ErrorMessage="Required" CssClass="error" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regEditMailTo" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtMailto" ForeColor="" ValidationGroup="new" 
                                            ValidationExpression="^[0-9]*[a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([a-zA-Z][-\w\.]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9}$" Display="Dynamic" CssClass="error" />
                                    </h3>
                                    <asp:TextBox runat="server" ID="txtMailto" cssClass="half_table" />
                                </td>
                             </tr>
                        </table>
                        <br />
                        <div style="text-align:center;">
                            <asp:Button ID="btnNewSubUnitSubmit"  runat="server" Text="Save Sub-Group" 
                                CssClass="button" ValidationGroup="new" onclick="btnNewSubUnitSubmit_Click" />&nbsp;
                            <asp:Button ID="btnCancelSubUnit" runat="server" Text="Cancel" CssClass="button" />
                        </div>
                    </fieldset>
                </div>
                
            </asp:Panel>
    
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="adminSidebar" Runat="Server">
    
</asp:Content>

