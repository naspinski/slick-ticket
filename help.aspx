<%@ Page Title="Help/FAQ" Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="info" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="JavaScript" type="text/javascript" src="js/wysiwyg/scripts/wysiwyg.js" ></script>
    <script language="JavaScript" type="text/javascript" src="js/wysiwyg/scripts/wysiwyg-settings.js"></script>
     <script type="text/javascript">
         var mysettings = new WYSIWYG.Settings();
         WYSIWYG.attach('ctl00_body_txtA', mysettings);

         function showHide(showDivId, hideDivId) {
             document.getElementById(showDivId).style.display = 'block';
             document.getElementById(hideDivId).style.display = 'none';
         } 
    </script> 
</asp:Content>
<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <fieldset class="inner_color">
                <a id="home"></a>
                <span class="larger bold">
                    <asp:Label ID="lblReport" runat="server" />
                </span>
                <h2>
                    <asp:Button CssClass="smaller button right" runat="server" ID="btnNew" meta:resourcekey="btnNewResource1" />
                    <asp:literal runat="server" ID="litIndexTitle" meta:resourcekey="litIndexTitleResource1"></asp:literal>
                </h2>
                <ul class="bold">
                    <asp:Repeater ID="rptIndex" runat="server" DataSourceID="lds">
                        <HeaderTemplate>
                            <li><a href="#permissions">
                                <asp:literal ID="litPermissionSystemLink" runat="server" meta:resourcekey="litPermissionSystemLinkResource1" />
                            </a></li>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <asp:Button ID="btnEdit" style="font-size:.7em;padding:1px"  CssClass="button"  runat="server" CommandArgument='<%# Eval("id") %>' 
                                    OnClick="btnEdit_Click" meta:resourcekey="btnEditResource1" /> 
                                <asp:Button style="font-size:.7em;padding:1px"  CssClass="button"  ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnDelete_Click"
                                     meta:resourcekey="btnDeleteResource1"  /> 
                                <a href="#<%# trimJunk(Eval("title").ToString()) %>"><%# Eval("title").ToString() %></a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            <li><a href="#another_question"><asp:literal ID="litNewQuestion" runat="server" meta:resourcekey="litNewQuestionResource1" />
                            </a></li>
                        </FooterTemplate>
                    </asp:Repeater>
                </ul>
            </fieldset>
            
            <div class="divider"></div>
                       
            <asp:Repeater ID="rpt" runat="server" DataSourceID="lds">
                <HeaderTemplate>    
                    <fieldset class="inner_color">
                        <h2>
                            <span class="faq"><a id="permissions"></a>
                                <asp:literal ID="litPermissionsSystemExplainedTitle" runat="server" meta:resourcekey="litPermissionSystemLinkResource1" />
                            </span>
                            <span class="smaller right">
                                <a href="#home"><%= Resources.Common.BackToTop %></a>
                            </span>
                            <span class="clear"></span>
                        </h2>
                        <br />
                        <div>
                            <div><asp:literal ID="litPermissionsSystemExplainedPanel1" runat="server"  meta:resourcekey="litPermissionsSystemExplainedPanel1Resource1" /></div>
                            <br />
                            <div style="text-align:center;"><img src="images/permissions_1.png" alt="permissions image 1" /></div>
                            <br />
                            <div><asp:literal ID="litPermissionsSystemExplainedPanel2" runat="server" meta:resourcekey="litPermissionsSystemExplainedPanel2Resource1" /></div>
                            <br />
                            <div style="text-align:center;"><img src="images/permissions_2.png" alt="permissions image 2" /></div>
                            <br />
                            <div><asp:literal ID="litPermissionsSystemExplainedPanel3" runat="server"  meta:resourcekey="litPermissionsSystemExplainedPanel3Resource1" /></div>
                            <br />
                            <div style="text-align:center;"><img src="images/permissions_3.png" alt="permissions image 3" /></div>
                            <br />       
                            <div><asp:literal ID="litPermissionsSystemExplainedPanel4" runat="server"  meta:resourcekey="litPermissionsSystemExplainedPanel4Resource1" /></div> 
                            <br />
                            <a class="bold" href="#home"><%= Resources.Common.BackToTop %></a>
                        </div>
                    </fieldset>
                    <div class="divider"></div>
                </HeaderTemplate>
                <ItemTemplate>
                    <fieldset class="inner_color">
                        <h2>
                            <span class="faq">
                                <% if (isAdmin) { %>
                                   <span class="smaller">
                                        <asp:Button ID="btnEdit" style="padding:3px;"  CssClass="smaller button" runat="server"  CommandArgument='<%# Eval("id") %>' 
                                        OnClick="btnEdit_Click" meta:resourcekey="btnEditResource1" />
                                        <asp:Button style="padding:3px;" CssClass="smaller button" ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' 
                                        OnClick="btnDelete_Click" meta:resourcekey="btnDeleteResource1"  /> 
                                   </span>
                                <% } %>
                                <a id="<%# trimJunk(Eval("title").ToString()) %>"><%# Eval("title").ToString() %></a>
                            </span>
                                    <span class="smaller right">
                                        <a href="#home"><%= Resources.Common.BackToTop %></a>
                                    </span>
                                    <span class="clear"></span>
                                </h2>
                                <div class="faq_body">
                            <%# Eval("body") %>
                        </div>
                        <br />
                        <a class="bold" href="#home"><%= Resources.Common.BackToTop %></a>
                    </fieldset>
                    <div class="divider"></div>
                </ItemTemplate>
                <FooterTemplate>    
                    <fieldset class="inner_color">
                        <h2>
                            <span class="faq">
                                <a id="another_question"></a>
                                <asp:literal runat="server" ID="litQuestoinNotCoveredTitle" meta:resourcekey="litNewQuestionResource1" />
                            </span>
                            <span class="smaller right">
                                <a href="#home"><%= Resources.Common.BackToTop %></a>
                            </span>
                            <span class="clear"></span>
                        </h2>
                        <div>
                                <a href="contact.aspx">
                                    <asp:literal runat="server" ID="litQuestionNotCovered" Text="Contact your administrator" />
                                </a> 
                        </div>
                        <br />
                        <a class="bold" href="#home"><%= Resources.Common.BackToTop %></a>
                    </fieldset>
                </FooterTemplate>
            </asp:Repeater>   
            <asp:LinqDataSource ID="lds" runat="server" ContextTypeName="dbDataContext" 
                OrderBy="title" TableName="faqs" />
            
            <ajax:ModalPopupExtender ID="mpe" runat="server" TargetControlID="btnNew" 
                PopupControlID="pnlNew" BackgroundCssClass="modal_background" 
                DynamicServicePath="" Enabled="True" />
            
            <asp:Panel ID="pnlNew" runat="server" style="display:none;" >
                <div class="large_container border_color">
                    <fieldset class="inner_color">
                        <h3>
                            <asp:Label ID="lblQuestion" runat="server" CssClass="title_header" meta:resourcekey="lblQuestionResource1" />
                            <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtQ" ValidationGroup="new" meta:resourcekey="rfvSubjectResource1" />
                        </h3>
                        <h2><asp:TextBox ID="txtQ" runat="server" CssClass="full_window" /></h2>
                        <h3>
                            <asp:Label ID="lblAnswer" cssclass="title_header" runat="server" meta:resourcekey="lblAnswerResource1">Answer</asp:Label>&nbsp;
                        </h3>
                        <asp:TextBox ID="txtA" runat="server" CssClass="full_window"  TextMode="MultiLine"  Height="100px" Width="100%"  />
                        <br />
                        <div style="text-align:center;">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="button" CommandArgument="0" ValidationGroup="new" onclick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1"  />
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sidebar" Runat="Server">
</asp:Content>

