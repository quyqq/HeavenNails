using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeavenNails.Database;


namespace HeavenNails.Codes
{
     public class DataAccess
    {
        public bool TestConnection(string ServerIn,string AccountIn,string PassIn, string DatabaseIn)
        {
            try
            {
                HeavenNailsModelDataContext Connect;
                Connect = new HeavenNailsModelDataContext("Server=" + ServerIn +
                    ";Database=" + DatabaseIn +
                    ";User Id=" + AccountIn +
                    ";Password = " + PassIn + " ;");
                Connect.Connection.Open();
                Connect.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Can not connect to server. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
        }
        private HeavenNailsModelDataContext Connection()
        {
            HeavenNailsModelDataContext Connect;
            try
            {
                Connect = new HeavenNailsModelDataContext("Server=" + Properties.Settings.Default.Server +
                    ";Database=" + Properties.Settings.Default.Database +
                    ";User Id=" + Properties.Settings.Default.AccountLOG +
                    ";Password = " + Properties.Settings.Default.Password + " ;");
                Connect.Connection.Open();
                return Connect;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Can not connect to server. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HeavenNails.SystemControl.Setting frm = new SystemControl.Setting();
                while (true)
                {
                    try
                    {
                        frm.ShowDialog();
                    }
                    catch (Exception)
                    {

                        
                    }

                }
            }


        }
        public List<Object> SelectAllServices()
        {
            List<Object> ListDataOut = new List<Object>();
            HeavenNailsModelDataContext connectionPort = Connection();
            if (connectionPort != null)
            {
                var Dataout = from rw in connectionPort.tb_Sevices
                              select new
                              {
                                  ID = rw.SIndex,
                                  SIndex = "SVC0"+rw.SIndex,
                                  NameService = rw.NameService,
                                  Cost = rw.Cost,
                                  Active = rw.Active
                              };
                ListDataOut.Add(Dataout);
                ListDataOut.Add(connectionPort);

                connectionPort.Connection.Close();
                return ListDataOut;
            }
            else return null;

        }
        public List<Object> SelectAllShownServices()
        {
            List<Object> ListDataOut = new List<Object>();
            HeavenNailsModelDataContext connectionPort = Connection();
            if (connectionPort != null)
            {
                var Dataout = from rw in connectionPort.tb_Sevices
                              where rw.Active==true
                              select new
                              {
                                  rw.SIndex,                                  
                                  NameService = rw.NameService,
                                  Cost = rw.Cost
                              };
                ListDataOut.Add(Dataout);
                ListDataOut.Add(connectionPort);

                connectionPort.Connection.Close();
                return ListDataOut;
            }
            else return null;

        }

        public Boolean InsertService(tb_Sevice ServiceData)
        {

            try
            {
                HeavenNailsModelDataContext connectionPort = Connection();
                connectionPort.tb_Sevices.InsertOnSubmit(ServiceData);
                connectionPort.SubmitChanges();
                connectionPort.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Can not add data to database. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public Boolean UpdateService(tb_Sevice ServiceData)
        {
            try
            {
                HeavenNailsModelDataContext connectionPort = Connection();
                tb_Sevice OldService = (from tmp in connectionPort.tb_Sevices
                                        where tmp.SIndex == ServiceData.SIndex
                                        select tmp).First();
                OldService.NameService = ServiceData.NameService;
                OldService.Cost = ServiceData.Cost;
                OldService.Active = ServiceData.Active;


                connectionPort.SubmitChanges();
                connectionPort.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Can not edit database. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public Boolean DeleteService(decimal SIndex)
        {
            try
            {
                HeavenNailsModelDataContext connectionPort = Connection();
                tb_Sevice DeletedRow = (from tmp in connectionPort.tb_Sevices
                                        where tmp.SIndex == SIndex
                                        select tmp).First();


                connectionPort.tb_Sevices.DeleteOnSubmit(DeletedRow);
                connectionPort.SubmitChanges();
                connectionPort.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Can not edit database. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<Object> SelectItemToSell(string BarCode)
        {
            /*
            ListColums.Add(new DataColumn("Type", typeof(string)));
            ListColums.Add(new DataColumn("QIIndex", typeof(decimal)));
            ListColums.Add(new DataColumn("SIndex", typeof(decimal)));
            ListColums.Add(new DataColumn("Name", typeof(string)));
            ListColums.Add(new DataColumn("ImportPrice", typeof(decimal)));
            ListColums.Add(new DataColumn("CostOrPrice", typeof(decimal)));
             */
            List<Object> ListDataOut = new List<Object>();
            HeavenNailsModelDataContext connectionPort = Connection();
            if (connectionPort != null)
            {
                var Dataout = from rw in connectionPort.tb_QuantityItems
                              select new
                              {
                                  Type = "Items",
                                  rw.QIIndex,
                                  SIndex = -1,
                                  Name = "",//Select from other tables ......,
                                  ImportPrice = 0,// Select from other tables.....
                                  CostOrPrice  = 0 // select from other tables....
                              };
                ListDataOut.Add(Dataout);
                ListDataOut.Add(connectionPort);

                connectionPort.Connection.Close();
                return ListDataOut;
            }
            else return null;
        }
    }
}
