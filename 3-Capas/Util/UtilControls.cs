using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Reflection;
using System.ComponentModel;

namespace Util.Library
{
    public class UtilControls
    {
        public static void FillDropDownList(DropDownList ddl, string valueField, string textField, object datasource)
        {
            ddl.DataValueField = valueField;
            ddl.DataTextField = textField;
            ddl.DataSource = datasource;
            ddl.DataBind();
        }

        public static void FillDropDownList(DropDownList ddlObj, string sValue, string sTexto, Object dsDatos, string sValuePre, string sTextoPre)
        {
            FillDropDownList(ddlObj, sValue, sTexto, dsDatos);
            ListItem liAux = new ListItem();
            liAux.Value = sValuePre;
            liAux.Text = sTextoPre;
            FillDropDownList(ddlObj, sValue, sTexto, dsDatos);
            ddlObj.Items.Insert(0, liAux);
            //if (!(ddlObj.Items.FindByValue(sPreValue) == null))
            //    ddlObj.Items.FindByValue(sPreValue).Selected = true;
        }

        public static void PreSelectDDL(object valueToSelect, DropDownList ddlObj)
        {
            int valueEnum = 0;
            string valToSelect = valueToSelect.ToString();
            if (ddlObj.SelectedItem != null)
                ddlObj.SelectedItem.Selected = false;
            if (valueToSelect.GetType().IsEnum)
            {
                valueEnum = (int)valueToSelect;
                valueToSelect = valueEnum.ToString();
            }

            if (!(ddlObj.Items.FindByValue(valueToSelect.ToString()) == null))
                ddlObj.Items.FindByValue(valueToSelect.ToString()).Selected = true;
        }

        public static void FillDropDownListDT(DropDownList ddl, string valueField, string textField, DataTable DT)
        {
            int r = DT.Rows.Count;

            for (int i = 0; i < r; i++)
            {
                string Text = DT.Rows[i][textField].ToString();
                string Value = DT.Rows[i][valueField].ToString();

                //ddl.Items.Insert(0,"i");                
            }            
        }

        public static void FillCheckBoxList(CheckBoxList chklst, string valueField, string textField, object datasource)
        {
            chklst.DataValueField = valueField;
            chklst.DataTextField = textField;
            chklst.DataSource = datasource;
            chklst.DataBind();
        }

        public static void FillCheckBoxList(CheckBoxList chklstObj, string sValue, string sTexto, Object dsDatos, string sValuePre, string sTextoPre)
        {
            FillCheckBoxList(chklstObj, sValue, sTexto, dsDatos);
            ListItem liAux = new ListItem();
            liAux.Value = sValuePre;
            liAux.Text = sTextoPre;
            FillCheckBoxList(chklstObj, sValue, sTexto, dsDatos);
            chklstObj.Items.Insert(0, liAux);
        }

        public static void SelectCHKLST(DataSet dsSelect, CheckBoxList chklstObj, string valueField)
        {
            
            if (dsSelect.Tables[0].Rows.Count != 0) {
                for (int i = 0; i < dsSelect.Tables[0].Rows.Count; i++) {
                        
                        chklstObj.Items.FindByValue(dsSelect.Tables[0].Rows[i][valueField].ToString()).Selected = true; 
                    
                }
                
            }

        }

        public static void FillListBox(ListBox lst, string valueField, string textField, object datasource)
        {
            lst.DataValueField = valueField;
            lst.DataTextField = textField;
            lst.DataSource = datasource;
            lst.DataBind();
        }

        public static void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        public static void SweetBox(String ex, String sub,String type, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>swal('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "','" + sub.Replace("\r\n", "\\n").Replace("'", "") + "','"+ type +"'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        public static void SweetBoxConfirm(String ex, String sub, String type, string url, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>";
            s += "swal({title: '" + ex + "',text: '" + sub + "',type: '" + type + "',showCancelButton: false,confirmButtonColor: '#DD6B55', confirmButtonText: 'OK',closeOnConfirm: true},function(){document.location.href = '"+ url + "';});</SCRIPT>";
           
            //string s = "<SCRIPT language='javascript'>swal({title:'" + ex.Replace("\r\n", "\\n").Replace("',text:", "") + "','" + sub.Replace("\r\n", "\\n").Replace("'", "") + "','" + type + "',false,'#DD6B55','OK',true); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        public static void EnumToListBox(Type EnumType, ListControl TheListBox,bool ValorNumerico)
        {
            //Array Values = Enum.GetValues(EnumType);
           //Recorremos el arreglo con todos los valores del enumerador
            string Display = String.Empty;
            DescriptionAttribute da;  //Es el tipo de variable que declaramos para hacer referencia a la descripcion del enumerador
            int posicion = 1; //como cambiamos la variable de entero a enumerador, hay que llevar un "posicionador" que me de el valor del enumerador en caso de ser requerido
            foreach (var value in Enum.GetValues(EnumType))
            {
                ListItem Item;
                FieldInfo fi = value.GetType().
                        GetField(value.ToString());
                da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi,
                            typeof(DescriptionAttribute));
                if (da != null)
                    Display = da.Description;
                else
                    Display = value.ToString();
                
                if (ValorNumerico) //El valor del ListBox será el Valor del Enumerador
                {Item = new ListItem(Display, posicion.ToString());}
                else  //El Valor del ListBox será el mismo que se despliegue
                {Item = new ListItem(Display, Display);}
                TheListBox.Items.Add(Item);
                posicion++;
            }
          
        }

        public static string GetDescription( Enum currentEnum)
        {
            string description = String.Empty;
            DescriptionAttribute da;
           
            FieldInfo fi = currentEnum.GetType().
                        GetField(currentEnum.ToString());
            da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi,
                        typeof(DescriptionAttribute));
            if (da != null)
                description = da.Description;
            else
                description = currentEnum.ToString();

            return description;
        }
    }
}
