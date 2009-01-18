<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="New Ticket " Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="new_ticket.aspx.cs" Inherits="new_ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="JavaScript" type="text/javascript" src="js/wysiwyg/scripts/wysiwyg.js" ></script>
    <script language="JavaScript" type="text/javascript" src="js/wysiwyg/scripts/wysiwyg-settings.js"></script>
     <script type="text/javascript">
         var mysettings = new WYSIWYG.Settings();
         WYSIWYG.attach('ctl00_body_txtDetails', mysettings);

         function showHide(showDivId, hideDivId) {
             document.getElementById(showDivId).style.display = 'block';
             document.getElementById(hideDivId).style.display = 'none';
         } 
    </script> 
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    
    <fieldset class="inner_color">
        
            <asp:Panel ID="pnlInput" runat="server" DefaultButton="btnSubmit" >
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <h3>
                            <span class="title_header larger">Assign To</span>&nbsp
                        </h3>
                        <table class="by2">
                            <tr>
                                <td style="width:50%;">
                                    <h3>
                                        <span class="title_header">Group</span>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvUnit" ControlToValidate="ddlUnit" InitialValue="0" ErrorMessage="Must Select Group"  ForeColor="" CssClass="error" />
                                    </h3>
                                    <asp:DropDownList ID="ddlUnit" runat="server"  cssclass="half_table"  onselectedindexchanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true"  />
                                </td>
                                <td style="width:50%;">
                                    <h3>
                                        <span class="title_header">Sub-Group</span>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvSubUnit" ControlToValidate="ddlSubUnit" InitialValue="0" ErrorMessage="Must Select Sub-Group"  ForeColor="" CssClass="error" />
                                    </h3>
                                    <asp:DropDownList ID="ddlSubUnit" runat="server"  cssclass="half_table"  />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <h3>
                                        <span class="title_header">Priority</span>
                                    </h3>
                                    <asp:DropDownList ID="ddlPriority" cssclass="full_window" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <h3>
                                        <span class="title_header">Topic</span>
                                        <asp:RequiredFieldValidator ID="rfvTopic" runat="server" ControlToValidate="txtTopic" ErrorMessage="Topic Required" />
                                    </h3>
                                    <h2>
                                        <asp:TextBox ID="txtTopic" runat="server" cssclass="full_window"/>
                                    </h2>
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <h3>
            <span class="title_header">Details</span>&nbsp;
            </h3>
            <div>
                <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" Height="100px" Width="100%"/>
            </div>
            
            <h3>
                <span class="title_header">Attachments</span>
                <span class="clear"></span>
            </h3>
            <div>
                <div><asp:FileUpload CssClass="attachment" ID="FileUpload1" runat="server" /></div>
                <div id="add1">
                    <a href="javascript:void();" onclick="showHide('att2', 'add1')">Additional Attachment</a>
                </div>
            </div>
            <div id="att2" style="display:none;">
                <div><asp:FileUpload CssClass="attachment" ID="FileUpload2" runat="server" /></div>
                <div id="add2">
                    <a href="javascript:void();" onclick="showHide('att3', 'add2')">Additional Attachment</a>
                </div>
            </div>
            <div id="att3" style="display:none;">
                <div><asp:FileUpload CssClass="attachment" ID="FileUpload3" runat="server" /></div>
                <div id="add3">
                    <a href="javascript:void();" onclick="showHide('att4', 'add3')">Additional Attachment</a>
                </div>
            </div>
            <div id="att4" style="display:none;">
                <div><asp:FileUpload CssClass="attachment" ID="FileUpload4" runat="server" /></div>
                <div id="add4">
                    <a href="javascript:void();" onclick="showHide('att5', 'add4')">Additional Attachment</a>
                </div>
            </div>
            <div id="att5" style="display:none;">
                <div><asp:FileUpload CssClass="attachment" ID="FileUpload5" runat="server" /></div>
                <div>
                    You can add additional attachments after the ticket is submitted
                </div>
            </div>
            <br />
            <div style="text-align:center;">
                <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="button" 
                    onclick="btnSubmit_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlOutput" runat="server" Visible="false">
            <h2 class="bold success larger">Ticket Submitted</h2>
            Your ticket has been successfully submitted; <asp:Label ID="lblSentTo" runat="server" /> 
            <br /><br />
            Your ticket number is <asp:Label ID="lblTicketNumber" CssClass="bold" runat="server" />; it will also be accessible  through <a class="bold" href="my_issues.aspx">my issues</a>.
        </asp:Panel>
        <asp:Panel ID="pnlError" runat="server" Visible="false">
            <h2 class="header">
                <span class="smaller"><asp:Label ID="lblReport" runat="server" /></span>
            </h2>
        </asp:Panel>
        
    </fieldset>

</asp:Content>

