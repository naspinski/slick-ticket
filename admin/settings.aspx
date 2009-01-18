<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Administration - Settings" Language="C#" MasterPageFile="~/admin/admin.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="settings.aspx.cs" Inherits="admin_settings" %>

<asp:Content ID="Content3" ContentPlaceHolderID="adminHead" Runat="Server">

<script type="text/javascript" src="../js/jquery.js"></script>
 <script type="text/javascript" src="../js/farbtastic.js"></script>
 <link rel="stylesheet" href="../css/farbtastic.css" type="text/css" />
 <style type="text/css" media="screen">
   .colorwell {
     border: 2px solid #fff;
     width: 6em;
     text-align: center;
     cursor: pointer;
   }
   body .colorwell-selected {
     border: 2px solid #000;
     font-weight: bold;
   }
 </style>
 
 <script type="text/javascript" charset="utf-8">
     $(document).ready(function() {
         var f = $.farbtastic('#picker');
         var p = $('#picker').css('opacity', 0.25);
         var selected;
         $('.colorwell')
      .each(function() { f.linkTo(this); $(this).css('opacity', 0.75); })
      .focus(function() {
          if (selected) {
              $(selected).css('opacity', 0.75).removeClass('colorwell-selected');
          }
          f.linkTo(this);
          p.css('opacity', 1);
          $(selected = this).css('opacity', 1).addClass('colorwell-selected');

      });
     });
 </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="adminBody" Runat="Server">
    
    <h2 class="header">
        Settings
        <span class="clear"></span>
        <asp:Label ID="Label1" runat="server" />
    </h2>
    <div style="text-align:left;" class="header">
        Various settings for system behavior and appearance; click on the settings to expand/collapse.
    </div>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <asp:UpdatePanel ID="pnlSettings" runat="server">
            <ContentTemplate>
            
                <ajax:CollapsiblePanelExtender ID="cpeSettings" runat="server" CollapseControlID="lblSettings" ExpandControlID="lblSettings" TargetControlID="stdSettings" Collapsed="true" />
            
                <h4>
                    <span id="stSettings" class="title_header pointer">
                        <asp:label ID="lblSettings" runat="server">General Settings</asp:label>
                        <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Basic settings required for your system to run correctly.</q></span></a>
                    </span>
                    <span class="smaller clear">
                        <asp:Label ID="lblAttachmentReport" runat="server" />
                    </span>
                </h4>
                <asp:Panel runat="server" id="stdSettings" class="collapse">
                    <asp:Panel ID="pnlTitle" runat="server" DefaultButton="btnTitle">
                        <h3 style="text-align:left;">
                            <span class="title_header">System Title</span><span class="clear"></span>
                            <asp:TextBox ID="txtTitle" runat="server" style="width:88%;" /> 
                            <asp:Button CssClass="button smaller" ID="btnTitle" runat="server" Text="Update" CommandArgument="title" onclick="updateSettings_Click" CommandName="txtTitle"  />
                        </h3>
                    </asp:Panel>
                    <asp:Panel ID="pnlImage" runat="server" DefaultButton="btnImage">
                        <h3 style="text-align:left;"><span class="clear"></span>
                            <span class="title_header">System Title Image</span>
                            <asp:TextBox ID="txtImage" runat="server" style="width:88%;" /> 
                            <asp:Button CssClass="button smaller" ID="btnImage" runat="server" Text="Update"  CommandArgument="image" onclick="updateSettings_Click" CommandName="txtImage" />
                        </h3>
                    </asp:Panel>
                    <asp:Panel ID="pnlAdminEmail" runat="server" DefaultButton="btnAdminEmail">
                        <h3 style="text-align:left;">
                            <span class="title_header">Admin Contact Email Address</span>
                            <span style="float:right;padding-right:66px;">
                                <asp:RequiredFieldValidator ID="rfvMailto" runat="server" ControlToValidate="txtAdminEmail" ValidationGroup="adminEmail" ErrorMessage="Required" CssClass="error" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="regEditMailTo" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtAdminEmail" ForeColor="" ValidationGroup="adminEmail" 
                                    ValidationExpression="^[0-9]*[a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([a-zA-Z][-\w\.]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9}$" Display="Dynamic" CssClass="error" />
                            </span>
                            <span class="clear"></span>
                            <asp:TextBox ID="txtAdminEmail" runat="server" style="width:88%;" />
                            <asp:Button CssClass="button smaller" ID="btnAdminEmail" runat="server" Text="Update" CommandArgument="admin_email" onclick="updateSettings_Click" CommandName="txtAdminEmail" 
                                ValidationGroup="adminEmail" />
                        </h3>
                    </asp:Panel>
                    <asp:Panel ID="pnlSystemEmail" runat="server" DefaultButton="btnSysEmail">
                        <h3 style="text-align:left;">
                            <span class="title_header">System Email Address</span>
                            <span style="float:right;padding-right:66px;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtSysEmail" ValidationGroup="sysEmail" ErrorMessage="Required" CssClass="error" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtSysEmail" ForeColor="" ValidationGroup="sysEmail" 
                                    ValidationExpression="^[0-9]*[a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([a-zA-Z][-\w\.]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9}$" Display="Dynamic" CssClass="error" />
                            </span>
                            <span class="clear"></span>
                            <asp:TextBox ID="txtSysEmail" runat="server" style="width:88%;" />
                            <asp:Button CssClass="button smaller" ID="btnSysEmail" runat="server" Text="Update" CommandArgument="system_email" onclick="updateSettings_Click" CommandName="txtSysEmail" 
                                ValidationGroup="sysEmail" />
                        </h3>
                    </asp:Panel>
                    <asp:Panel ID="pnlDC" runat="server" DefaultButton="btnDC">
                        <h3 style="text-align:left;">
                            <span class="title_header">Domain Controller</span>
                            <span style="float:right;padding-right:66px;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Required" CssClass="error" ControlToValidate="txtDC" ValidationGroup="dc" />
                            </span>
                            <asp:TextBox ID="txtDC" runat="server" style="width:88%;" /> 
                            <asp:Button CssClass="button smaller" ID="btnDC" runat="server" CommandArgument="domain_controller" Text="Update" onclick="updateSettings_Click" CommandName="txtDC" ValidationGroup="dc" />
                        </h3>
                    </asp:Panel>
                    <asp:Panel ID="pnlSmtp" runat="server" DefaultButton="btnSmtp">
                        <h3 style="text-align:left;">
                            <span class="title_header">SMTP Server</span>
                            <span style="float:right;padding-right:66px;">
                                <asp:RequiredFieldValidator ID="rfvSmtp" runat="server" ErrorMessage="Required" CssClass="error" ControlToValidate="txtSmtp" ValidationGroup="smtp" />
                            </span>
                            <asp:TextBox ID="txtSmtp" runat="server" style="width:88%;" /> 
                            <asp:Button CssClass="button smaller" ID="btnSmtp" runat="server" CommandArgument="smtp" Text="Update" onclick="updateSettings_Click" CommandName="txtSmtp" ValidationGroup="smtp" />
                        </h3>
                    </asp:Panel>
                    <asp:Panel ID="pnlAttachment" runat="server" DefaultButton="btnAttachment">
                        <h3 style="text-align:left;">
                            <span class="title_header">Attachment Storage Directory</span>
                            <asp:TextBox ID="txtAttachment" runat="server" style="width:88%;" /> 
                            <asp:Button CssClass="button smaller" ID="btnAttachment" runat="server" Text="Update" onclick="btnAttachment_Click" />
                        </h3>
                    </asp:Panel>
                    <br />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <asp:UpdatePanel ID="pnlEmail" runat="server">
            <ContentTemplate>
            
                <ajax:CollapsiblePanelExtender ID="cpeEmail" runat="server" CollapseControlID="lbl_Email" ExpandControlID="lbl_Email" TargetControlID="stdEmail" Collapsed="true" />
                
                <h4>
                        <span class="title_header">
                            <asp:Label ID="lbl_Email" runat="server" CssClass="pointer">User Email Settings</asp:Label>
                            <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Enable/disable email notifications and domain restriction, allowing users to only select from specified domains.</q></span></a>
                        </span>
                        <span class="smaller">
                          <asp:Button  ID="btnDomainPopup" CssClass="button smaller" runat="server" Text="Add Domain" />
                        </span>                        
                        <asp:Label ID="lblEmail" runat="server" />
                    </h4>
                    <asp:Panel id="stdEmail" runat="server" class="collapse clear">
                        <h3>
                            <span class="title_header">
                                <asp:CheckBox ID="chkEmail" runat="server" AutoPostBack="true" 
                                Text=" Automatic Email Notifications" 
                                oncheckedchanged="chkEmail_CheckedChanged" />&nbsp;
                            </span>
                            <br /><br />
                        </h3>
                        <h3>
                            <span class="title_header">
                                <asp:CheckBox ID="chkRestrictDomains" runat="server" AutoPostBack="true" Text=" Restrict User Email Domains" oncheckedchanged="chkRestrictDomains_CheckedChanged" />&nbsp;
                            </span>
                            <br /><br />
                        </h3>
                        <span class="clear"></span>
                        <asp:Panel ID="pnlDomains" runat="server">
                            <asp:GridView ID="gvDomains" runat="server" AutoGenerateColumns="False" 
                                GridLines="None" DataKeyNames="id" DataSourceID="ldsDomains" CssClass="list"
                                
                                EmptyDataText="<h5>No Domains specified</h5><div style='text-align:center;'>This will cause users to be unable to edit their profiles</div>" 
                                AllowSorting="True" onrowdeleted="gvDomains_RowDeleted">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Remove" OnClientClick="return confirm('Are you sure you want to remove this domain?');" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="domain" HeaderText="Available Email Domains" SortExpression="domain" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#EFF5FF" />
                            </asp:GridView>
                            <br />
                            <asp:LinqDataSource ID="ldsDomains" runat="server" ContextTypeName="dbDataContext" EnableDelete="True" OrderBy="domain" 
                                TableName="allowed_email_domains" />
                        </asp:Panel>
                    </asp:Panel>
                
                <ajax:ModalPopupExtender ID="mpeAddDomain" runat="server" TargetControlID="btnDomainPopup" BackgroundCssClass="modal_background"
                    PopupControlID="pnlAddDomain" CancelControlID="btnCancel" />
                
                <asp:Panel ID="pnlAddDomain" runat="server"  CssClass="modal_popup" style="display:none;" DefaultButton="btnAddDomain">
                    
                    <div class="small_container border_color">
                        <fieldset class="inner_color">
                            <h3>
                                <span class="title_header">Domain</span>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvDomainAdd" runat="server" ErrorMessage="Required" Display="Dynamic" ForeColor="" CssClass="error"
                                    ValidationGroup="addDomain" ControlToValidate="txtDomainAdd" />
                                <asp:RegularExpressionValidator ID="regEmailDomain" runat="server" ErrorMessage="Invalid Domain" Display="Dynamic" ForeColor="" CssClass="error"
                                    ValidationGroup="addDomain" ControlToValidate="txtDomainAdd" ValidationExpression="((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$" />
                            </h3>
                            <asp:TextBox ID="txtDomainAdd" runat="server" CssClass="inside_small_container" />
                            <br /><br />
                            <div style="text-align:center;">
                                <asp:Button ID="btnAddDomain" runat="server" Text="Add" CssClass="button"  ValidationGroup="addDomain" onclick="btnAddDomain_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" />
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <asp:UpdatePanel ID="pnlAccess" runat="server">
            <ContentTemplate>
            
                <ajax:CollapsiblePanelExtender ID="cpeAccess" runat="server" CollapseControlID="lblAccess" ExpandControlID="lblAccess" TargetControlID="stdAccess" Collapsed="true" />
            
                <h4>
                    <span class="title_header">
                        <asp:Label CssClass="pointer" runat="server" ID="lblAccess">
                            Access Level Names and Descriptions
                            <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>These have no functional purpose, but should make it easier for the administrators and users to understand the levels.</q></span></a>
                        </asp:Label>
                    </span>
                    <span class="clear"></span>
                    <asp:Label ID="lblAccessReport" runat="server" />
                </h4>
                <asp:Panel ID="stdAccess" runat="server" class="collapse">
                    <asp:GridView ID="gvAccessLevels" runat="server" AutoGenerateColumns="False" CssClass="list" 
                        DataKeyNames="id" DataSourceID="ldsAccessLevels" AllowSorting="True" 
                        GridLines="None">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" ItemStyle-Width="110px" >

                            </asp:CommandField>
                            <asp:BoundField DataField="id" ReadOnly="True" SortExpression="id" />
                            <asp:BoundField DataField="security_level_name" HeaderText="Access Level Name" SortExpression="security_level_name" ControlStyle-Width="100%" />
                            <asp:BoundField DataField="security_level_description" HeaderText="Access Level Description" SortExpression="security_level_description" ControlStyle-Width="100%" />
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="ldsAccessLevels" runat="server" ContextTypeName="dbDataContext" TableName="security_levels"  Where="id &gt; 0" EnableUpdate="True" />
                    <br />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
    
        <ajax:CollapsiblePanelExtender ID="cpeImport" runat="server" CollapseControlID="lblImport" ExpandControlID="lblImport" TargetControlID="pnlImport" Collapsed="true" />
    
        <h4>
            <span class="title_header">
                <asp:Label CssClass="pointer" runat="server" ID="lblImport">
                    Import/Export Settings
                    <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Import/export settings such as your current theme sets and faqs.</q></span></a>
                </asp:Label>
            </span>
            <span class="clear"></span>
            <asp:Label ID="lblImportReport" style="text-align:left;" runat="server" />
        </h4>
        <asp:Panel ID="pnlImport" runat="server" class="collapse">
            <h3 style="text-align:left;">Import</h3>
            <asp:Button ID="btnStyleImport" runat="server" CssClass="button" Text="Import Themes" 
                onclick="btnImport_Click" ValidationGroup="import" />
            <asp:Button ID="btnFaqImport" runat="server" CssClass="button" Text="Import Faqs" 
                onclick="btnImport_Click" ValidationGroup="import" />
            <asp:FileUpload ID="fuImport" runat="server" />
            <asp:RequiredFieldValidator ID="rfvImport" CssClass="bold" ValidationGroup="import" 
                runat="server" ErrorMessage="Required" ControlToValidate="fuImport" />
            <h3 style="text-align:left;">Export</h3>
            <asp:Button CssClass="button" ID="btnExportStyles" runat="server" OnClick="btnExport_Click" Text="Export Themes" />
            <asp:Button CssClass="button" ID="btnExportFaqs" runat="server" OnClick="btnExport_Click" Text="Export Faqs" />
            <div><br /></div>
        </asp:Panel>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <ajax:CollapsiblePanelExtender ID="colAppearance" runat="server" TargetControlID="pnlAppearance" Collapsed="true" CollapseControlID="lblAppearance" ExpandControlID="lblAppearance" />
        <h4>
            <span class="title_header">
                <asp:Label CssClass="pointer" runat="server" ID="lblAppearance">Appearance</asp:Label>
                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Change the appearance of the entire system.</q></span></a>
            </span>
            <span class="smaller">
                <asp:Button ID="btnApplyAppearance" CssClass="button smaller" ValidationGroup="style" runat="server" Text="Apply Changes" onclick="btnApplyAppearance_Click" />&nbsp;
                <asp:Button ID="btnCssReset" runat="server" CssClass="button smaller"  Text="Reset to Default" onclick="btnCssReset_Click" />
            </span>
            <asp:Label ID="lblCAppearance" runat="server" />
        </h4>
        <asp:Panel ID="pnlAppearance" runat="server"  class="collapse">
          <h3 style="text-align:left;">
            Sidebar on 
            <asp:RadioButton ID="left" runat="server" Text="left" GroupName="radSidebar" CssClass="checkbox" />
            <asp:RadioButton ID="right" Text="right" runat="server" GroupName="radSidebar" CssClass="checkbox" />
          </h3>
            <h3>
                <span class="title_header">
                    <asp:DropDownList ID="ddlThemes" runat="server" DataSourceID="ldsThemes" AutoPostBack="true" AppendDataBoundItems="true" 
                        DataTextField="style_name" DataValueField="id"  onselectedindexchanged="ddlThemes_SelectedIndexChanged" >
                        <asp:ListItem Value="0">Pre-Set Themes</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button CssClass="button smaller" OnClientClick="confirm('This theme can not be recovered. Continue?')" runat="server"
                        Text="Delete" onclick="btnThemeDelete_Click" ID="btnThemeDelete" Visible="false" />
                    <asp:Label ID="lblThemeDelete" runat="server" />
                </span>    
                <asp:LinqDataSource ID="ldsThemes" runat="server" 
                    ContextTypeName="dbDataContext" OrderBy="style_name" TableName="styles" 
                    Where="id &gt; @id" >
                    <WhereParameters>
                        <asp:Parameter DefaultValue="2" Name="id" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </h3>
            
            <ajax:CollapsiblePanelExtender ID="cpeCustomize" runat="server" TargetControlID="pnlCustomize" CollapseControlID="lblCustomize" ExpandControlID="lblCustomize" Collapsed="true" />
            
            <h3 style="text-align:left;" class="clear">
                <asp:Label ID="lblCustomize" runat="server" CssClass="link pointer"><a href="javascript:void();">Customize</a></asp:Label>
            </h3>
            <asp:Panel ID="pnlCustomize" runat="server" class="collapse">
              <div id="picker" style="float: right;"></div>
              <asp:ValidationSummary ID="valStyle" runat="server" CssClass="error_area border_color header" ValidationGroup="style" style="width:370px;" />
              
              <asp:RegularExpressionValidator ID="regText" ControlToValidate="styleText" runat="server" ErrorMessage="Invalid color code for Text" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="rfvText" ControlToValidate="styleText" runat="server" ErrorMessage="Text value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="styleBorders" runat="server" ErrorMessage="Invalid color code for Borders" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="styleBorders" runat="server" ErrorMessage="Borders value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="styleBody" runat="server" ErrorMessage="Invalid color code for Body" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="styleBody" runat="server" ErrorMessage="Body value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="styleLink" runat="server" ErrorMessage="Invalid color code for Links" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="styleLink" runat="server" ErrorMessage="Links value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="styleLinkHover" runat="server" ErrorMessage="Invalid color code for Link Hover" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="styleLinkHover" runat="server" ErrorMessage="Link Hover value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="styleButtonText" runat="server" ErrorMessage="Invalid color code for Button Text" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="styleButtonText" runat="server" ErrorMessage="Button Text value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="styleHeader" runat="server" ErrorMessage="Invalid color code for Headers" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="styleHeader" runat="server" ErrorMessage="Headers value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="styleAlternatingRows" runat="server" ErrorMessage="Invalid color code for Alt Rows" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="styleAlternatingRows" runat="server" ErrorMessage="Alt Rows value required" ValidationGroup="style" Display="None" />
              <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="styleBg" runat="server" ErrorMessage="Invalid color code for Background" ValidationExpression="^#([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$" ValidationGroup="style"  Display="None" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="styleBg" runat="server" ErrorMessage="Background value required" ValidationGroup="style" Display="None" />
              
              <table style="width:370px;">
                <tr>
                    <td style="width:150px;">
                        <h3>
                            <span class="title_header">
                                Text
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Text throughout the system</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleText" cssclass="colorwell" text="#444444" />
                    </td>
                    <td style="width:150px;">
                        <h3>
                            <span class="title_header">
                                Borders
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Borders and header color</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleBorders" cssclass="colorwell" text="#c3d9ff" />
                    </td>
                    <td style="width:150px;">
                        <h3>
                            <span class="title_header">
                                Body
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Inner body of all elements</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleBody" cssclass="colorwell" text="#e0ecff" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3>
                            <span class="title_header">
                                Links
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>All hyperlinks and buttons</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleLink" cssclass="colorwell" text="#ffa500" />
                    </td>
                    <td>
                        <h3>
                            <span class="title_header">
                                Link Hover
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Color link or button changes to when a user hovers over it</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleLinkHover" cssclass="colorwell" text="#ff7700" />
                    </td>
                    <td>
                        <h3>
                            <span class="title_header">
                                Button Text
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Text in buttons (should contrast with 'Links' color)</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleButtonText" cssclass="colorwell" text="#ffffff" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <h3>
                            <span class="title_header">
                                Headers
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Main headers (should contrast with 'Borders' color)</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleHeader" name="color4" cssclass="colorwell" text="#eff5ff" />
                    </td>
                    <td>
                        <h3>
                            <span class="title_header">
                                Alt Rows
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>The color of every-other row in tables; alternates with 'Body' color</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleAlternatingRows" name="color4" cssclass="colorwell" text="#eff5ff" />
                    </td>
                    <td>
                        <h3>
                            <span class="title_header">
                                Background
                                <a href='javascript:void();' class='tooltip limited'><img src='../images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>Background and selected tab</q></span></a>
                            </span>
                        </h3>
                        <asp:TextBox runat="server" ID="styleBg" name="color4" cssclass="colorwell" text="#ffffff" />
                    </td>
                </tr>
              </table>
              <br />
              <br />
                <h3 style="clear:both; text-align:left;">
                    Save current as new theme template named:
                    <asp:TextBox ID="txtNewTheme" runat="server" />
                    <asp:Button ID="btnNewTheme" runat="server" Text="Save" ValidationGroup="newTheme"  CssClass="button smaller" onclick="btnNewTheme_Click"  /><br />
                    <asp:RequiredFieldValidator ID="rfvTheme" runat="server" ControlToValidate="txtNewTheme" ValidationGroup="newTheme" CssClass="error_area error" ErrorMessage="<span style='padding-left:320px;'>Name Required</span>"
                        display="Dynamic" /> 
                    <asp:Label ID="lblNewTheme" runat="server" />
                </h3>
                <div class="clear"></div><br />
            </asp:Panel>
        </asp:Panel>
    </fieldset>
</asp:Content>

