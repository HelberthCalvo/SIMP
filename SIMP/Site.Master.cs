﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMP
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkBtnCliente_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Pages/Cliente/Index.aspx");
            Response.Redirect("~/Cliente/Index.aspx");
        }

        protected void linkBtnUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuario/Index.aspx");
        }
    }
}