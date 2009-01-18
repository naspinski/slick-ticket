<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Administration Information" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="info.aspx.cs" Inherits="admin_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="adminHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="adminBody" Runat="Server">   
    <h4 class="header">
        <span class="title_header">Information</span>
        <span class="clear"></span>
        <span class="smaller"><asp:Label ID="lblReport" runat="server" /></span>
    </h4>
    <div style="text-align:left;" class="header">
        Here you can find some guidance for setup and exactly what each administration area does.
    </div>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>Index</h2>
        <ul class="bold">
            <li><a href="#setup">Initial Setup</a></li>
            <li><a href="#permissions">Permissions</a></li>
            <li><a href="#groups">Groups</a></li>
            <li><a href="#users">Users</a></li>
            <li><a href="#settings">Settings</a></li>
            <li><a href="#info">Information</a></li>
            <li><a href="#stats">Statistics</a></li>
        </ul>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>
            <a href="#home" class="right smaller">Back to Top</a>
            <a id="setup"></a>
            Initial Setup
        </h2>
        <ol>
            <li>
                <div class="bold">Set up AD relational permissions on the <a href="permissions.aspx">permissions page</a></div>
                Without permissions set, all users will have the lowest access level of 1
            </li>
            <li>
                <div class="bold">Set the groups in your system on the <a href="groups.aspx">groups page</a></div>
                No tickets can be assigned if there are no groups to assign to
            </li>
            <li>
                <div class="bold">Adjust system settings on the <a href="settings.aspx">settings</a> page</div>
                For the system to work correctly, the following settings must be set correctly (others are optional):
                <ul class="sublist">
                    <li>Admin Contact Email Address Required Invalid Email Address</li>
                    <li>SMTP Server</li>
                    <li>Attachment Storage Directory</li>
                </ul>
                 <span class="bold">*You may need to set your &#39;Attachment Storage Directory' permissions to allow authenticated users to write</span>
            </li>
        </ol>
        <br />
        <a href="#home" class="bold">Back to Top</a>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>
            <a href="#home" class="right smaller">Back to Top</a>
            <a id="permissions"></a>
            Permissions
        </h2>
        <br />
        <div>
            This section ties user Active Directory membership to their access level in the system. The
            <a href="../help.aspx">permission system is explained here</a>.
            <br /><br />
            <span class="bold">Add Group</span> - This button allows you to add a new AD group to the system. This group must already be part of Active Directory. 
            All users that are members of a specified group will be granted that corresponding access level with the highest level taking priority.
            <br /><br />
            <span class="bold">Update AD</span> - Due to the large size of many AD forests (this forest has <%= utils.settings.get("ad_groups") %>) the groups are cached in a local xml file instead of querying AD every log on. 
            This greatly increases the performance of the system, but will not recognize new/deleted groups in AD until this button is pushed and the cache is refreshed.
        </div>
        <br />
        <a href="#home" class="bold">Back to Top</a>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>
            <a href="#home" class="right smaller">Back to Top</a>
            <a id="groups"></a>
            Groups
        </h2>
        <div>
            Designate the groups and sub-groups your users will be able to join and assign tickts to.  
            The required access level of the sub-groups is the lowest access level that a user can have and be able to assign tickets to, or join that group. 
            View all sub-groups by selecting a different group in the drop-down menu.
            The
            <a href="../help.aspx">permission system is explained here</a>.
            <br /><br />
            <span class="bold">Add Group</span> - This button allows you to add a new group to the system.
            A group is nothing more than a upper-level classification, think of it as a &#39;folder&#39; to keep your sub-groups in.
            <br /><br />
            <span class="bold">Add Sub-Group</span> - Sub-groups are what groups users are members of.
            If a sub-group gets a ticket assigned to it, an email will be sent to the &#39;Mail to&#39; email address for that group.
            Sub-groups also decide what you see in <a href="../my_issues.aspx">my issues</a>.
        </div>
        <br />
        <a href="#home" class="bold">Back to Top</a>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>
            <a href="#home" class="right smaller">Back to Top</a>
            <a id="users"></a>
            Users
        </h2>
        <div>
            This is simply an area to view and delete users as well as assign/revoke administration privileges.
            In order to avoid a broken system, you can not remove your own administration privileges.
            <br /><br />
            In an effort to keep this as simple as possible.  There will be no adding of users by the administration.
            This is possible since permissions are handled via Active Directory and the <a href="permissions.aspx">permissions</a> you set.
            Adding of users is all done automatically when a users first logs on, not allowing them to do anything until they fill out their profile.
            Users are unable to use anything but their logon name, so identity checking is not a problem.
        </div>
        <br />
        <a href="#home" class="bold">Back to Top</a>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>
            <a href="#home" class="right smaller">Back to Top</a>
            <a id="settings"></a>
            Settings
        </h2>
        <div class="faq_body">
            <ul>
                <li>
                    <span class="bold">General Settings</span><br />
                    This is where you set your general settings to make the site work, like site title, SMTP server, domain controller, etc.
                </li>
                <li>
                    <span class="bold">User Email Settings</span><br />
                    Here you decide whether or not to send out automatic email notifications for things such as ticket assignment.  
                    You also set whether or not to restrict email address domains for users.
                </li>
                <li>
                    <span class="bold">Access Level Names and Descriptions</span><br />
                    Here you set the names and descriptions of the access levels used within the system.
                    These have no functional purpose, but should make it easier for the administrators and users to understand the levels. 
                </li>
                <li>
                    <span class="bold">Import/Export Settings</span><br />
                    Allows you to import/export your themes and faq contents via xml file.
                </li>
                <li>
                    <span class="bold">Appearance</span><br />
                    Allows you to change the appearance of the entire system.
                    Also allows you to save your current appearance as a theme.
                </li>
            </ul>
        </div>
        <br />
        <a href="#home" class="bold">Back to Top</a>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>
            <a href="#home" class="right smaller">Back to Top</a>
            <a id="info"></a>
            Information
        </h2>
        <div>
            You&#39;re looking at it!
            Information about the administration interface and setup of Slick Ticket.
        </div>
        <br />
        <a href="#home" class="bold">Back to Top</a>
    </fieldset>
    <div class="divider"></div>
    <fieldset class="inner_color">
        <h2>
            <a href="#home" class="right smaller">Back to Top</a>
            <a id="stats"></a>
            Statistics
        </h2>
        <div>
            Allows a quick overview of ticket status throughout your organization.
            Show statistics such as average ticket age, average closure time, etc.
        </div>
        <br />
        <a href="#home" class="bold">Back to Top</a>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="adminSidebar" Runat="Server">
</asp:Content>

