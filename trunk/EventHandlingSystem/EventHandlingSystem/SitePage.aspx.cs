using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetOpenAuth.OpenId;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class SitePage : System.Web.UI.Page
    {

        private int _wId;

        private string _type;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Hämtar WebPageId från URL.
            var stId = Request.QueryString["Id"];

            var stType = Request.QueryString["Type"];

            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out _wId) && !string.IsNullOrWhiteSpace(stType))
            {
                webpages webPage = WebPageDB.GetWebPageById(_wId);
                if (webPage != null)
                {
                    if (webPage.CommunityId != null)
                    {
                        // Sets the title on the page
                        Page.Title =
                            CommunityDB.GetCommunityById((int)webPage.CommunityId).Name;
                    }
                    else if (webPage.AssociationId != null)
                    {
                        // Sets the title on the page
                        Page.Title = AssociationDB.GetAssociationById((int)webPage.AssociationId).Name;
                    }
                    else
                    {
                        // Sets the title on the page
                        Page.Title = "No title";
                    }
                }
                else
                {
                    Page.Title = "Uknown page";
                }
            }
        }



        protected void Page_Init(object sender, EventArgs e)
        {

            ////MyControl is the Custom User Control with a code behind file
            //About myControl = (About)Page.LoadControl("~/About.ascx");

            //Calendar calendarControl = (Calendar) Page.LoadControl("~/Calendar.ascx");

            //var eventListControl = Page.LoadControl("~/EventList.ascx");

            ////ControlHolder is a place holder on the aspx page where I want to load the
            ////user control to.
            //ControlHolder.Controls.Add(myControl);
            //ControlHolder.Controls.Add(calendarControl);
            //ControlHolder.Controls.Add(eventListControl);

            if (_wId > 0)
            {
                if (WebPageDB.GetWebPageById(_wId) != null)
                {
                    foreach (var component in ComponentDB.GetComponentsByWebPageId(_wId).OrderBy(c => c.OrderingNumber).ThenBy(c => c.controls.Name))
                    {
                        controls c = ControlDB.GetControlsById(component.controls_Id);
                        //var filterTypeNameList = new List<string>();
                        object[] filterDataList = new object[component.filterdata.Count];
                        if (c != null)
                        {
                            //EventHandlingSystem.Components.
                            Type cls = Type.GetType("EventHandlingSystem.Components." + c.Name);
                            if (cls != null)
                            {
                                //foreach (var prop in cls.GetProperties())
                                //{
                                //    filterTypeNameList.Add(prop.Name);
                                //}

                                for (int i = 0; i < component.filterdata.Count; i++)
                                {
                                    filterDataList[i] = component.filterdata.ElementAt(i).Data;
                                }

                                //Type ComponentType = Type.GetType("EventHandlingSystem." + c.Name);
                                //if (ComponentType != null)
                                //{

                                    //var constructor =
                                    //    cls.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(string) });
                                if (filterDataList.Any())
                                {
                                    string filepath = "~/" + ControlDB.GetControlsById(component.controls_Id).FilePath;
                                    if (File.Exists(Server.MapPath(filepath)))
                                    {
                                        UserControl loadControl = LoadControl(filepath,filterDataList);
                                        ControlHolder.Controls.Add(loadControl);
                                    }
                                }
                                else
                                {
                                    string filepath = "~/" + ControlDB.GetControlsById(component.controls_Id).FilePath;
                                    if (File.Exists(Server.MapPath(filepath)))
                                    {
                                        UserControl loadControl = (UserControl)Page.LoadControl(filepath);
                                        ControlHolder.Controls.Add(loadControl);
                                    }
                                }
                                    //UserControl newcontrol = (UserControl)Page.LoadControl(cls,
                                    //    new object[] { "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });

                                    ////Activator.c
                                    //ControlHolder.Controls.Add(newcontrol);
                                    //constructor.Invoke(newcontrol, filterDataList);


                                    // This is the original code
                                    //ControlHolder.Controls.Add(Page.LoadControl("~/" + ControlDB.GetControlsById(component.controls_Id).FilePath));
                                //}
                            }
                            else
                            {
                                string filepath = "~/" + ControlDB.GetControlsById(component.controls_Id).FilePath;
                                if (File.Exists(Server.MapPath(filepath)))
                                {
                                UserControl loadControl = (UserControl) Page.LoadControl(filepath);
                                ControlHolder.Controls.Add(loadControl);
                                }
                            }
                        }
                    }

                    // REMOVE THIS CODE
                    if (ComponentDB.GetComponentsByWebPageId(_wId).Count == 0)
                    {
                        ControlHolder.Controls.Add(Page.LoadControl("~/About.ascx"));
                    }
                }
            }
        }

        private UserControl LoadControl(string UserControlPath, params object[] constructorParameters)
        {
            List<Type> constParamTypes = new List<Type>();
            foreach (object constParam in constructorParameters)
            {
                constParamTypes.Add(constParam.GetType());
            }

            UserControl ctl = Page.LoadControl(UserControlPath) as UserControl;

            // Find the relevant constructor
            ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

            //And then call the relevant constructor
            if (constructor == null)
            {
                throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
            }
            else
            {
                constructor.Invoke(ctl, constructorParameters);
            }

            // Finally return the fully initialized UC
            return ctl;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Hämtar EventId från URL.
            var stId = Request.QueryString["Id"];

            var stType = Request.QueryString["Type"];

            webpages webPage = null;

            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id) && !string.IsNullOrWhiteSpace(stType))
            {
                webPage = WebPageDB.GetWebPageById(id);
            }

            if (webPage != null)
            {
                LabelTitle.Text = webPage.Title;
            }
            else
            {
                LabelTitle.Text = "No title";
                LabelTitle.CssClass = "ribbon-title-small";
            }

            
            // Bättre att göra det i page_int
            //UserControl loadControl = LoadControl("~/Calendar.ascx", new object[] { "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            //ControlHolder.Controls.Add(loadControl);

        }
    }
}