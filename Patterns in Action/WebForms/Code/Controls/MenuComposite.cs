using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace WebForms.Controls
{
    
    // menu control that renders HTML menu controls including 
    // selected menu items, menu item indentation, as well as the proper HTML 
    // and CSS (cascading style sheet) tags.
    
    // GoF Design patterns: Composite.
    // EnterPrise Design Pattern: Transform View.

    [DefaultProperty("Selected")]
    [ToolboxData("<{0}:MenuComposite runat=server></{0}:MenuComposite>")]
    public class MenuComposite : WebControl
    {
        
        // gets and sets the selected menu item.
        
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string SelectedItem
        {
            get { return (string)ViewState["SelectedItem"] ?? string.Empty; }
            set { ViewState["SelectedItem"] = value; }
        }

        
        // gets and sets the entire menu tree using the ASP.NET Viewstate.
        
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        public MenuCompositeItem MenuItems
        {
            get { return ViewState["MenuItems"] as MenuCompositeItem; }
            set { ViewState["MenuItems"] = value; }
        }

        
        // renders the entire menu structure.
        
        protected override void RenderContents(HtmlTextWriter output)
        {
            var root = this.MenuItems;

            output.Write(@"<div id=""navcontainer"">");
            output.Write(@"	<ul id=""navlist"">");

            RecursiveRender(output, root, 0);

            output.Write(@"	</ul>");
            output.Write(@"</div>");
        }

        
        // helper that renders a menu item at the correct indentation level.  
      
        void RecursiveRender(HtmlTextWriter output, MenuCompositeItem item, int depth)
        {
            if (depth > 0) // skip root node 
            {
                if (depth == 1)
                    output.Write("<li>");  // main menu
                else
                    output.Write(@"<li class=""indented"">");  // sub menu

                output.Write(@"<a href=""" + item.Link + @""">");

                if (item.Item == SelectedItem)  // selected item
                    output.Write(@"<span class=""selected"">" + item.Item + "</span>");
                else
                    output.Write(item.Item);  // unselected item.

                output.Write("</a>");
            }

            // recursively iterate over its children

            for (int i = 0; i < item.Children.Count; i++)
            {
                RecursiveRender(output, item.Children[i], depth + 1);
            }
        }
    }
}
