<%@ Page Title="Help/FAQ" Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="info" %>

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
<%--    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
            <fieldset class="inner_color">
                <span class="larger bold"><asp:Label ID="lblReport" runat="server" /></span>
                <h2>
                    <% if(isAdmin) { %>
                        <asp:Button CssClass="smaller button right" Text="New Question/Answer" runat="server" ID="btnNew" />
                    <% } %>
                    Help/FAQ Index
                </h2>
                <ul class="bold">
                    <asp:Repeater ID="rptIndex" runat="server" DataSourceID="lds">
                        <HeaderTemplate>
                            <li><a href="#permissions">Permission system explained</a></li>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <% if(isAdmin) { %>
                                <asp:Button ID="btnEdit" style="font-size:.7em;padding:1px"  CssClass="button" runat="server" Text="Edit" CommandArgument='<%# Eval("id") %>' OnClick="btnEdit_Click" /> 
                                <asp:Button style="font-size:.7em;padding:1px"  CssClass="button" ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnDelete_Click"
                                     OnClientClick="return confirm('Are you sure?');"  /> 
                                <% } %>
                                <a href="#<%# trimJunk(Eval("title").ToString()) %>"><%# Eval("title").ToString() %></a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            <li><a href="#another_question">I have a question that is not covered here...</a></li>
                        </FooterTemplate>
                    </asp:Repeater>
                </ul>
            </fieldset>
            
            <div class="divider"></div>
            
            
            <asp:Repeater ID="rpt" runat="server" DataSourceID="lds">
                <HeaderTemplate>    
                    <fieldset class="inner_color">
                        <h2>
                            <span class="faq"><a id="permissions"></a>Permission system explained</span>
                            <span class="smaller right">
                                <a href="#home">Back to top</a>
                            </span>
                            <span class="clear"></span>
                        </h2>
                        <br />
                        <div>
                            <div>
                                First thing the system does is to log the user in with your Windows credentials, then it immediately pulls all the groups you are a member of in your domain: </div>        <br />        <div style="text-align:center;">            <img src="images/permissions_1.png" alt="log the user in with your Windows credentials, then it immediately pulls all the groups you are a member of in your domain" />        </div>        <br />        <div>            Next the system compares your AD memberships with the permission groups that are set by the administrator.              Each permission group is related to a single AD group and has an 'Access Level' associated with it.            You are then assigned the highest access level available to you.        </div>        <br />        <div style="text-align:center;">            <img src="images/permissions_2.png" alt="log the user in with your Windows credentials, then it immediately pulls all the groups you are a member of in your domain" />        </div>        <br />        <div>            Now that the system has your access level, it can decide which groups you are able to join and assign tickets to.        </div>        <br />        <div style="text-align:center;">            <img src="images/permissions_3.png" alt="log the user in with your Windows credentials, then it immediately pulls all the groups you are a member of in your domain" />        </div>        <br />        <div>            Also, you will notice that you can view and comment any ticket, but only change status, priority, attachments and assignment of tickets if it is at or under your access level.        </div>        <br />        <div>            Basically your Active Directory groups decide your access level which then determines what you can do; think of access levels as the highest level that a particular AD group can assign tickets to, that should simplify things.        </div>        
                        </div>
                        <br />
                        <a class="bold" href="#home">Back to top</a>
                    </fieldset>
                    <div class="divider"></div>
                </HeaderTemplate>
                <ItemTemplate>
                    <fieldset class="inner_color">
                        <h2>
                            <span class="faq">
                                <% if (isAdmin)  { %>
                                   <span class="smaller">
                                        <asp:Button ID="btnEdit" style="padding:3px;" CssClass="smaller button" runat="server" Text="Edit" CommandArgument='<%# Eval("id") %>' OnClick="btnEdit_Click" />
                                        <asp:Button style="padding:3px;" CssClass="smaller button" ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnDelete_Click"
                                            OnClientClick="return confirm('Are you sure?');"  /> 
                                   </span>
                                <% } %>
                                <a id="<%# trimJunk(Eval("title").ToString()) %>"></a><%# Eval("title").ToString() %>
                            </span>
                            <span class="smaller right">
                                <a href="#home">Back to top</a>
                            </span>
                            <span class="clear"></span>
                        </h2>
                        <div class="faq_body">
                            <%# Eval("body") %>
                        </div>
                        <br />
                        <a class="bold" href="#home">Back to top</a>
                    </fieldset>
                    <div class="divider"></div>
                </ItemTemplate>
                <FooterTemplate>    
                    <fieldset class="inner_color">
                        <h2>
                            <span class="faq"><a id="another_question"></a>I have a question that is not covered here...</span>
                            <span class="smaller right">
                                <a href="#home">Back to top</a>
                            </span>
                            <span class="clear"></span>
                        </h2>
                        <br />
                        <div>
                            Please 
                            <a href="contact.aspx">contact your administrator</a> 
                            and they will be able to help you out.
                        </div>
                        <br />
                        <a class="bold" href="#home">Back to top</a>
                    </fieldset>
                </FooterTemplate>
            </asp:Repeater>   
            <asp:LinqDataSource ID="lds" runat="server" ContextTypeName="dbDataContext" 
                OrderBy="title" TableName="faqs" />
            
            <ajax:ModalPopupExtender ID="mpe" runat="server" TargetControlID="btnNew" PopupControlID="pnlNew" BackgroundCssClass="modal_background" />
            
            <asp:Panel ID="pnlNew" runat="server" style="display:none;">
                <div class="large_container border_color">
                    <fieldset class="inner_color">
                        <h3>
                            <span class="title_header">Question</span>
                            <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Required" ControlToValidate="txtQ" ValidationGroup="new" />
                        </h3>
                        <h2><asp:TextBox ID="txtQ" runat="server" CssClass="full_window" /></h2>
                        <h3>
                            <span class="title_header">Answer</span>&nbsp;
                        </h3>
                        <asp:TextBox ID="txtA" runat="server" CssClass="full_window" TextMode="MultiLine"  Height="100px" Width="100%"/>
                        <br />
                        <div style="text-align:center;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="button" CommandArgument="0" 
                                ValidationGroup="new" onclick="btnSubmit_Click"  />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sidebar" Runat="Server">
</asp:Content>

